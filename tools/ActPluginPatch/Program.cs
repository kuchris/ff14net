using System.IO.Compression;
using System.Text;
using System.Security.Cryptography;
using Mono.Cecil;
using Mono.Cecil.Cil;

if (args.Length >= 1 && string.Equals(args[0], "patch-deucalion-757", StringComparison.OrdinalIgnoreCase))
{
    return PatchDeucalion757(args.Skip(1).ToArray());
}

if (args.Length >= 1 && string.Equals(args[0], "dump", StringComparison.OrdinalIgnoreCase))
{
    return Dump(args.Skip(1).ToArray());
}

if (args.Length >= 1 && string.Equals(args[0], "patch-file", StringComparison.OrdinalIgnoreCase))
{
    return PatchFromFile(args.Skip(1).ToArray());
}

if (args.Length < 4)
{
    Console.WriteLine("usage:");
    Console.WriteLine("  ActPluginPatch <input-plugin-dll> <output-plugin-dll> <region> Name=hex [Name=hex...]");
    Console.WriteLine(@"  example: ActPluginPatch FFXIV_ACT_Plugin.dll FFXIV_ACT_Plugin.patched.dll Global ActorCast=2af");
    Console.WriteLine("  ActPluginPatch patch-file <input-plugin-dll> <output-plugin-dll> <region> <opcode-file.txt>");
    Console.WriteLine(@"  opcode file format: Name|hex, e.g. ActorCast|2af");
    Console.WriteLine("  ActPluginPatch dump <plugin-dll> <region> [Name ...]");
    Console.WriteLine("  ActPluginPatch patch-deucalion-757 <input-plugin> <output-plugin> <original-deucalion> <output-deucalion>");
    return 1;
}

var replacements = args
    .Skip(3)
    .Select(ParseReplacement)
    .ToDictionary(static x => x.Name, static x => x.Value, StringComparer.Ordinal);
return PatchPlugin(args[0], args[1], args[2], replacements);

static int Dump(string[] args)
{
    if (args.Length < 2)
    {
        throw new InvalidOperationException("dump usage: ActPluginPatch dump <plugin-dll> <region> [Name ...]");
    }

    var pluginPath = Path.GetFullPath(args[0]);
    var region = args[1];
    var names = args.Skip(2).ToHashSet(StringComparer.Ordinal);
    var regionResource = GetRegionResource(region);

    var outer = ModuleDefinition.ReadModule(pluginPath);
    var outerResource = outer.Resources
        .OfType<EmbeddedResource>()
        .FirstOrDefault(static r => r.Name == "costura.machina.ffxiv.dll.compressed")
        ?? throw new InvalidOperationException("Outer plugin does not contain costura.machina.ffxiv.dll.compressed.");

    var innerDllBytes = Decompress(ReadAllBytes(outerResource.GetResourceStream()));
    using var innerStream = new MemoryStream(innerDllBytes);
    var inner = ModuleDefinition.ReadModule(innerStream);

    var opcodeResource = inner.Resources
        .OfType<EmbeddedResource>()
        .FirstOrDefault(r => r.Name == regionResource)
        ?? throw new InvalidOperationException($"Inner Machina.FFXIV does not contain {regionResource}.");

    var opcodeText = ReadAllText(opcodeResource.GetResourceStream());
    foreach (var line in opcodeText.Split(["\r\n", "\n"], StringSplitOptions.RemoveEmptyEntries))
    {
        var separator = line.IndexOf('|');
        if (separator < 0)
        {
            continue;
        }

        var name = line[..separator].Trim();
        if (names.Count == 0 || names.Contains(name))
        {
            Console.WriteLine(line.Trim());
        }
    }

    return 0;
}

static int PatchFromFile(string[] args)
{
    if (args.Length != 4)
    {
        throw new InvalidOperationException("patch-file usage: ActPluginPatch patch-file <input-plugin-dll> <output-plugin-dll> <region> <opcode-file.txt>");
    }

    var replacements = ParseOpcodeFile(args[3]);
    return PatchPlugin(args[0], args[1], args[2], replacements);
}

static int PatchPlugin(
    string inputPluginArg,
    string outputPluginArg,
    string region,
    IReadOnlyDictionary<string, string> replacements)
{
    var inputPlugin = Path.GetFullPath(inputPluginArg);
    var outputPlugin = Path.GetFullPath(outputPluginArg);
    var regionResource = GetRegionResource(region);

    var outer = ModuleDefinition.ReadModule(inputPlugin);
    var outerResource = outer.Resources
        .OfType<EmbeddedResource>()
        .FirstOrDefault(static r => r.Name == "costura.machina.ffxiv.dll.compressed")
        ?? throw new InvalidOperationException("Outer plugin does not contain costura.machina.ffxiv.dll.compressed.");

    var innerDllBytes = Decompress(ReadAllBytes(outerResource.GetResourceStream()));
    using var innerStream = new MemoryStream(innerDllBytes);
    var inner = ModuleDefinition.ReadModule(innerStream);

    var opcodeResource = inner.Resources
        .OfType<EmbeddedResource>()
        .FirstOrDefault(r => r.Name == regionResource)
        ?? throw new InvalidOperationException($"Inner Machina.FFXIV does not contain {regionResource}.");

    var opcodeText = ReadAllText(opcodeResource.GetResourceStream());
    var lines = opcodeText
        .Split(["\r\n", "\n"], StringSplitOptions.None)
        .ToList();

    foreach (var replacement in replacements)
    {
        var index = lines.FindIndex(line =>
        {
            var separator = line.IndexOf('|');
            if (separator < 0)
            {
                return false;
            }

            var name = line[..separator].Trim();
            return string.Equals(name, replacement.Key, StringComparison.Ordinal);
        });

        if (index < 0)
        {
            throw new InvalidOperationException($"Opcode name not found in {regionResource}: {replacement.Key}");
        }

        lines[index] = $"{replacement.Key}|{replacement.Value}";
    }

    opcodeText = string.Join(Environment.NewLine, lines);

    inner.Resources.Remove(opcodeResource);
    inner.Resources.Add(new EmbeddedResource(
        opcodeResource.Name,
        opcodeResource.Attributes,
        Encoding.UTF8.GetBytes(opcodeText)));

    using var patchedInnerStream = new MemoryStream();
    inner.Write(patchedInnerStream);
    var patchedInnerCompressed = Compress(patchedInnerStream.ToArray());

    outer.Resources.Remove(outerResource);
    outer.Resources.Add(new EmbeddedResource(
        outerResource.Name,
        outerResource.Attributes,
        patchedInnerCompressed));

    Directory.CreateDirectory(Path.GetDirectoryName(outputPlugin)!);
    outer.Write(outputPlugin);

    Console.WriteLine($"Patched: {outputPlugin}");
    Console.WriteLine($"Region: {region}");
    foreach (var replacement in replacements)
    {
        Console.WriteLine($"{replacement.Key}={replacement.Value}");
    }

    return 0;
}

static int PatchDeucalion757(string[] args)
{
    if (args.Length != 4)
        throw new InvalidOperationException("usage: patch-deucalion-757 <input-plugin> <output-plugin> <original-deucalion> <output-deucalion>");

    const string originalHash = "326be4db261064ebb51b8c46d2940f55701d79887e2c65070aff00de4c8c65ef";
    var inputPlugin = Path.GetFullPath(args[0]);
    var outputPlugin = Path.GetFullPath(args[1]);
    var inputNative = Path.GetFullPath(args[2]);
    var outputNative = Path.GetFullPath(args[3]);
    if (string.Equals(inputPlugin, outputPlugin, StringComparison.OrdinalIgnoreCase)
        || string.Equals(inputNative, outputNative, StringComparison.OrdinalIgnoreCase))
        throw new InvalidOperationException("Use separate output paths to preserve the input DLLs.");

    var native = File.ReadAllBytes(inputNative);
    if (Convert.ToHexString(SHA256.HashData(native)).ToLowerInvariant() != originalHash)
        throw new InvalidOperationException("Expected the original Deucalion 1.5.0 DLL; its SHA256 did not match.");
    var oldPattern = Encoding.ASCII.GetBytes("E8 $ { ' } 4C 8B 4F 10 8B 47 1C 45");
    var newPattern = Encoding.ASCII.GetBytes("E8 $ { ' } 4C 8B 47 10 44 8B 4F 1C");
    if (oldPattern.Length != newPattern.Length)
        throw new InvalidOperationException("Native replacement must preserve byte length.");
    var offsets = new List<int>();
    for (var i = 0; i <= native.Length - oldPattern.Length; i++)
        if (native.AsSpan(i, oldPattern.Length).SequenceEqual(oldPattern))
            offsets.Add(i);
    if (offsets.Count != 3)
        throw new InvalidOperationException($"Expected three signature strings, found {offsets.Count}.");
    foreach (var offset in offsets)
        newPattern.CopyTo(native.AsSpan(offset, newPattern.Length));
    var newHash = SHA256.HashData(native);
    var oldChecksum = string.Join("-", Convert.FromHexString(originalHash).Select(b => b.ToString("x2")));
    var newChecksum = string.Join("-", newHash.Select(b => b.ToString("x2")));

    using var outer = ModuleDefinition.ReadModule(inputPlugin);
    var resource = outer.Resources.OfType<EmbeddedResource>()
        .Single(r => r.Name == "costura.machina.ffxiv.dll.compressed");
    using var innerStream = new MemoryStream(Decompress(ReadAllBytes(resource.GetResourceStream())));
    using var inner = ModuleDefinition.ReadModule(innerStream);
    var method = inner.GetTypes().Single(t => t.FullName == "Machina.FFXIV.Deucalion.DeucalionInjector")
        .Methods.Single(m => m.Name == "get__checksum");
    var checksums = method.Body.Instructions.Where(i => i.OpCode == OpCodes.Ldstr).ToList();
    if (checksums.Count != 1 || (string)checksums[0].Operand != oldChecksum)
        throw new InvalidOperationException("Embedded Machina checksum did not match the original Deucalion DLL.");
    checksums[0].Operand = newChecksum;
    using var patchedInner = new MemoryStream();
    inner.Write(patchedInner);
    outer.Resources.Remove(resource);
    outer.Resources.Add(new EmbeddedResource(resource.Name, resource.Attributes, Compress(patchedInner.ToArray())));
    Directory.CreateDirectory(Path.GetDirectoryName(outputPlugin)!);
    Directory.CreateDirectory(Path.GetDirectoryName(outputNative)!);
    outer.Write(outputPlugin);
    File.WriteAllBytes(outputNative, native);
    Console.WriteLine($"Patched plugin: {outputPlugin}");
    Console.WriteLine($"Patched Deucalion: {outputNative}");
    Console.WriteLine($"Native SHA256: {Convert.ToHexString(newHash).ToLowerInvariant()}");
    Console.WriteLine($"Signature offsets: {string.Join(", ", offsets.Select(i => $"0x{i:x}"))}");
    return 0;
}

static string GetRegionResource(string region) =>
    region switch
    {
        "Global" => "Machina.FFXIV.Headers.Opcodes.Global.txt",
        "CN" => "Machina.FFXIV.Headers.Opcodes.Chinese.txt",
        "KR" => "Machina.FFXIV.Headers.Opcodes.Korean.txt",
        "TW" => "Machina.FFXIV.Headers.Opcodes.TraditionalChinese.txt",
        _ => throw new InvalidOperationException($"Unknown region: {region}")
    };

static (string Name, string Value) ParseReplacement(string value)
{
    var parts = value.Split('=', 2);
    if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
    {
        throw new InvalidOperationException($"Invalid replacement: {value}");
    }

    var opcodeValue = parts[1].Trim().ToLowerInvariant();
    if (!opcodeValue.All(Uri.IsHexDigit))
    {
        throw new InvalidOperationException($"Opcode must be hex without 0x prefix: {value}");
    }

    return (parts[0].Trim(), opcodeValue);
}

static IReadOnlyDictionary<string, string> ParseOpcodeFile(string opcodeFileArg)
{
    var opcodeFile = Path.GetFullPath(opcodeFileArg);
    var replacements = new Dictionary<string, string>(StringComparer.Ordinal);

    foreach (var rawLine in File.ReadAllLines(opcodeFile))
    {
        var line = rawLine.Trim();
        if (line.Length == 0 || line.StartsWith('#'))
        {
            continue;
        }

        var parts = line.Split('|', 2);
        if (parts.Length != 2 || string.IsNullOrWhiteSpace(parts[0]) || string.IsNullOrWhiteSpace(parts[1]))
        {
            throw new InvalidOperationException($"Invalid opcode file line: {rawLine}");
        }

        var name = parts[0].Trim();
        var opcodeValue = parts[1].Trim().ToLowerInvariant();
        if (!opcodeValue.All(Uri.IsHexDigit))
        {
            throw new InvalidOperationException($"Opcode must be hex without 0x prefix: {rawLine}");
        }

        replacements[name] = opcodeValue;
    }

    if (replacements.Count == 0)
    {
        throw new InvalidOperationException($"Opcode file is empty: {opcodeFile}");
    }

    return replacements;
}

static byte[] ReadAllBytes(Stream stream)
{
    using var output = new MemoryStream();
    stream.CopyTo(output);
    return output.ToArray();
}

static string ReadAllText(Stream stream)
{
    using var reader = new StreamReader(stream, Encoding.UTF8, detectEncodingFromByteOrderMarks: true);
    return reader.ReadToEnd();
}

static byte[] Decompress(byte[] compressed)
{
    using var input = new MemoryStream(compressed);
    using var deflate = new DeflateStream(input, CompressionMode.Decompress);
    using var output = new MemoryStream();
    deflate.CopyTo(output);
    return output.ToArray();
}

static byte[] Compress(byte[] raw)
{
    using var output = new MemoryStream();
    using (var deflate = new DeflateStream(output, CompressionMode.Compress, leaveOpen: true))
    {
        deflate.Write(raw, 0, raw.Length);
    }

    return output.ToArray();
}
