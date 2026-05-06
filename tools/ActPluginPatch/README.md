# ActPluginPatch

Small helper for patching the embedded FF14 opcode table inside a built `FFXIV_ACT_Plugin.dll`.

This helper does **not** rebuild the ACT plugin from source. It rewrites the bundled `costura.machina.ffxiv.dll.compressed` payload inside the plugin, updates the embedded opcode text resource, and writes a new plugin DLL copy.

## Files

- [Program.cs](./Program.cs): patcher implementation
- [ActPluginPatch.csproj](./ActPluginPatch.csproj): helper project
- [../patch_act_plugin.cmd](../patch_act_plugin.cmd): wrapper command
- [../../data/act_global_opcodes.txt](../../data/act_global_opcodes.txt): default Global opcode table input

## What It Patches

The helper opens a built `FFXIV_ACT_Plugin.dll`, extracts the embedded `Machina.FFXIV.dll`, updates one of these opcode resources, then repacks the outer plugin:

- `Machina.FFXIV.Headers.Opcodes.Global.txt`
- `Machina.FFXIV.Headers.Opcodes.Chinese.txt`
- `Machina.FFXIV.Headers.Opcodes.Korean.txt`
- `Machina.FFXIV.Headers.Opcodes.TraditionalChinese.txt`

Region names accepted by the tool:

- `Global`
- `CN`
- `KR`
- `TW`

## Default Workflow

1. Edit [../../data/act_global_opcodes.txt](../../data/act_global_opcodes.txt).
2. Run the wrapper:

```cmd
tools\patch_act_plugin.cmd
```

Default output:

```text
tools\third_party\FFXIV_ACT_Plugin_3.0.1.8\FFXIV_ACT_Plugin.patched.dll
```

The original `FFXIV_ACT_Plugin.dll` is left unchanged.

## Opcode File Format

The wrapper expects a plain text file with one opcode per line:

```text
StatusEffectList|117
StatusEffectList2|3dc
ActorCast|345
EffectResult|ea
ActorControl|328
```

Rules:

- one entry per line
- format is `Name|hex`
- hex values should not include `0x`
- blank lines are allowed
- lines starting with `#` are ignored

## Wrapper Usage

Run with defaults:

```cmd
tools\patch_act_plugin.cmd
```

Optional arguments:

```cmd
tools\patch_act_plugin.cmd [opcode-file] [output-dll] [region]
```

Example:

```cmd
tools\patch_act_plugin.cmd data\act_global_opcodes.txt tools\third_party\FFXIV_ACT_Plugin_3.0.1.8\FFXIV_ACT_Plugin.test.dll Global
```

## Direct Tool Usage

Patch from inline values:

```powershell
dotnet run --project tools\ActPluginPatch\ActPluginPatch.csproj -- `
  "C:\path\to\FFXIV_ACT_Plugin.dll" `
  "C:\path\to\FFXIV_ACT_Plugin.patched.dll" `
  Global `
  ActorCast=2af `
  ActorControl=328
```

Patch from an opcode file:

```powershell
dotnet run --project tools\ActPluginPatch\ActPluginPatch.csproj -- patch-file `
  "C:\path\to\FFXIV_ACT_Plugin.dll" `
  "C:\path\to\FFXIV_ACT_Plugin.patched.dll" `
  Global `
  "C:\path\to\act_global_opcodes.txt"
```

Dump selected values from a plugin:

```powershell
dotnet run --project tools\ActPluginPatch\ActPluginPatch.csproj -- dump `
  "C:\path\to\FFXIV_ACT_Plugin.patched.dll" `
  Global `
  ActorCast ActorControl EffectResult
```

## Build

```powershell
dotnet build tools\ActPluginPatch\ActPluginPatch.csproj
```

The helper depends on `Mono.Cecil` to read and rewrite assembly resources.

## Limitations

- This helper patches an already-built plugin DLL. It does not produce a clean source rebuild of the ACT plugin.
- If a game patch changes packet structure, updating opcodes alone may not be enough.
- The wrapper defaults are currently aimed at the plugin copy under `tools\third_party\FFXIV_ACT_Plugin_3.0.1.8`.
