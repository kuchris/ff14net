using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using Machina.FFXIV.Deucalion;
using Machina.FFXIV;
using Machina.FFXIV.Oodle;
using Machina.Infrastructure;

static int FindFfxivPid()
{
    var matches = Process.GetProcessesByName("ffxiv_dx11");
    if (matches.Length == 0)
    {
        throw new InvalidOperationException("Could not find ffxiv_dx11.");
    }

    if (matches.Length > 1)
    {
        throw new InvalidOperationException("Multiple ffxiv_dx11 processes found; pass --pid.");
    }

    return matches[0].Id;
}

static string ToHex(byte[] data, int maxBytes)
{
    var count = Math.Min(data.Length, maxBytes);
    return Convert.ToHexString(data.AsSpan(0, count)).ToLowerInvariant();
}

static int ReadIntArg(string[] args, string name, int defaultValue)
{
    var index = Array.IndexOf(args, name);
    if (index < 0 || index + 1 >= args.Length)
    {
        return defaultValue;
    }

    return int.TryParse(args[index + 1], out var value) ? value : defaultValue;
}

static bool HasFlag(string[] args, string name)
{
    return args.Contains(name, StringComparer.OrdinalIgnoreCase);
}

static string? ReadStringArg(string[] args, string name)
{
    var index = Array.IndexOf(args, name);
    if (index < 0 || index + 1 >= args.Length)
    {
        return null;
    }

    return args[index + 1];
}

var seconds = ReadIntArg(args, "--seconds", 30);
var maxMessages = ReadIntArg(args, "--max-messages", 100);
var maxBytes = ReadIntArg(args, "--max-bytes", 512);
var useDeucalion = HasFlag(args, "--deucalion");
var injectOnly = HasFlag(args, "--inject-only");
var output = ReadStringArg(args, "--output");
var deucalionPath = ReadStringArg(args, "--deucalion-path");
var deucalionDll = ReadStringArg(args, "--deucalion-dll");
var pid = ReadIntArg(args, "--pid", 0);
if (pid == 0)
{
    pid = FindFfxivPid();
}

var seen = 0;
using var writer = output is null ? null : new StreamWriter(output, append: false);
var monitor = new FFXIVNetworkMonitor();
monitor.ProcessID = (uint)pid;
monitor.MonitorType = NetworkMonitorType.RawSocket;
monitor.UseRemoteIpFilter = true;
monitor.OodleImplementation = OodleImplementation.FfxivTcp;
monitor.UseDeucalion = useDeucalion;
if (!string.IsNullOrWhiteSpace(deucalionPath))
{
    DeucalionInjector.DeucalionPath = Path.GetFullPath(deucalionPath);
}
var resolvedDeucalionDll = Path.GetFullPath(
    deucalionDll
    ?? Path.Combine(deucalionPath ?? DeucalionInjector.DeucalionPath, "deucalion-1.5.0.dll"));

if (injectOnly)
{
    Console.Error.WriteLine($"FF14 PID: {pid}");
    Console.Error.WriteLine($"Deucalion DLL: {resolvedDeucalionDll}");
    NativeInjector.InjectLoadLibrary(pid, resolvedDeucalionDll);

    Console.Error.WriteLine("Deucalion injection requested.");
    return 0;
}

monitor.MessageReceivedEventHandler += (connection, epoch, message) =>
{
    var current = Interlocked.Increment(ref seen);
    if (current > maxMessages)
    {
        return;
    }

    var record = new
    {
        time = DateTimeOffset.UtcNow.ToString("O"),
        direction = "recv",
        epoch,
        local = $"{connection.LocalIP}:{connection.LocalPort}",
        remote = $"{connection.RemoteIP}:{connection.RemotePort}",
        process_id = connection.ProcessId,
        connection_id = connection.ID,
        message_len = message.Length,
        message_hex = ToHex(message, maxBytes),
    };
    var line = JsonSerializer.Serialize(record);
    if (writer is null)
    {
        Console.WriteLine(line);
    }
    else
    {
        lock (writer)
        {
            writer.WriteLine(line);
            writer.Flush();
        }
    }
};

Console.Error.WriteLine($"FF14 PID: {pid}");
Console.Error.WriteLine($"Capture mode: {(useDeucalion ? "Deucalion" : "RawSocket")}");
if (useDeucalion)
{
    Console.Error.WriteLine($"Deucalion path: {DeucalionInjector.DeucalionPath}");
}
Console.Error.WriteLine($"Capturing decoded messages for {seconds}s, max {maxMessages} messages...");
if (output is not null)
{
    Console.Error.WriteLine($"Output: {output}");
}
monitor.Start();
var deadline = DateTime.UtcNow.AddSeconds(seconds);
while (DateTime.UtcNow < deadline && Volatile.Read(ref seen) < maxMessages)
{
    Thread.Sleep(100);
}

monitor.Stop();
Console.Error.WriteLine($"Messages saved: {Math.Min(seen, maxMessages)}");
return 0;

static class NativeInjector
{
    private const uint ProcessCreateThread = 0x0002;
    private const uint ProcessQueryInformation = 0x0400;
    private const uint ProcessVmOperation = 0x0008;
    private const uint ProcessVmWrite = 0x0020;
    private const uint ProcessVmRead = 0x0010;
    private const uint MemCommit = 0x1000;
    private const uint MemReserve = 0x2000;
    private const uint PageReadwrite = 0x04;
    private const uint Infinite = 0xFFFFFFFF;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr OpenProcess(uint access, bool inheritHandle, int processId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr VirtualAllocEx(IntPtr process, IntPtr address, uint size, uint allocationType, uint protect);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool WriteProcessMemory(
        IntPtr process,
        IntPtr baseAddress,
        byte[] buffer,
        uint size,
        out UIntPtr bytesWritten);

    [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
    private static extern IntPtr GetModuleHandle(string moduleName);

    [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
    private static extern IntPtr GetProcAddress(IntPtr module, string procName);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr CreateRemoteThread(
        IntPtr process,
        IntPtr threadAttributes,
        uint stackSize,
        IntPtr startAddress,
        IntPtr parameter,
        uint creationFlags,
        IntPtr threadId);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern uint WaitForSingleObject(IntPtr handle, uint milliseconds);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool CloseHandle(IntPtr handle);

    public static void InjectLoadLibrary(int processId, string dllPath)
    {
        if (!File.Exists(dllPath))
        {
            throw new FileNotFoundException("DLL not found.", dllPath);
        }

        var access = ProcessCreateThread | ProcessQueryInformation | ProcessVmOperation | ProcessVmWrite | ProcessVmRead;
        var process = OpenProcess(access, false, processId);
        if (process == IntPtr.Zero)
        {
            throw new InvalidOperationException($"OpenProcess failed: {Marshal.GetLastWin32Error()}");
        }

        IntPtr thread = IntPtr.Zero;
        try
        {
            var loadLibrary = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryW");
            if (loadLibrary == IntPtr.Zero)
            {
                throw new InvalidOperationException($"GetProcAddress(LoadLibraryW) failed: {Marshal.GetLastWin32Error()}");
            }

            var bytes = System.Text.Encoding.Unicode.GetBytes(Path.GetFullPath(dllPath) + "\0");
            var remoteString = VirtualAllocEx(process, IntPtr.Zero, (uint)bytes.Length, MemCommit | MemReserve, PageReadwrite);
            if (remoteString == IntPtr.Zero)
            {
                throw new InvalidOperationException($"VirtualAllocEx failed: {Marshal.GetLastWin32Error()}");
            }

            if (!WriteProcessMemory(process, remoteString, bytes, (uint)bytes.Length, out var written) || written.ToUInt64() != (ulong)bytes.Length)
            {
                throw new InvalidOperationException($"WriteProcessMemory failed: {Marshal.GetLastWin32Error()}");
            }

            thread = CreateRemoteThread(process, IntPtr.Zero, 0, loadLibrary, remoteString, 0, IntPtr.Zero);
            if (thread == IntPtr.Zero)
            {
                throw new InvalidOperationException($"CreateRemoteThread failed: {Marshal.GetLastWin32Error()}");
            }

            _ = WaitForSingleObject(thread, Infinite);
        }
        finally
        {
            if (thread != IntPtr.Zero)
            {
                _ = CloseHandle(thread);
            }
            _ = CloseHandle(process);
        }
    }
}
