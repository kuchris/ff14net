# ff14net

Small Python experiments for reading FF14 network data.

## Current status (Global)

| Item | Value |
| --- | --- |
| Game build | `2026.10.16.0000.0000` (patch label 7.57, not reconfirmed) |
| ACT FFXIV plugin input | 3.0.3.3, stored in `tools/third_party/FFXIV_ACT_Plugin_3.0.3.3/` |
| Deucalion | 1.5.0 with the patched receive signature |
| Prepared bundle | `tools/third_party/patch/` (plugin + both Deucalion copies) |
| Last live check | 2026-10-20: `RECV ON`, ACT type `21` damage events on a training dummy |

`tools/third_party/FFXIV_ACT_Plugin_3.0.3.4.zip` is a newer plugin download
that has not been extracted, patched or tested yet.

## Opcode data

| File | Contents |
| --- | --- |
| `data/opcodes.json` | Upstream-format table for the Python readers |
| `data/act_global_opcodes.txt` | Global table with ACT names, `Name\|hex` |
| `data/xivalex_global_opcodes.json` | Global XivAlexander server values |

- **Global 7.57**: only the 26 server Zone opcodes supplied by the user on
  2026-10-20 (replacing the 2026-10-13 mappings). Other Global messages are
  unknown. The values match the ACT DLL, but packet layouts have not been
  checked message by message.
- **XivAlexander**: server values only. The config stays incomplete until the
  7.57 `C2S_ActionRequest` and `C2S_ActionRequestGroundTargeted` values are
  known; the old client values were removed.
- **CN / KR / TW**: taken from the upstream
  [FFXIVOpcodes](https://github.com/karashiiro/FFXIVOpcodes) download
  (CN `7.55a`, KR `7.55h`, TW `7.2`); not checked locally.

`tools/update_opcode_data.bat` downloads the upstream file and **overwrites all
three files**, including the hand-entered Global table. Check the upstream
Global version first.

## ACT / Deucalion repair

Use `tools\repair_deucalion_20261020.ps1` for the current build:

```powershell
pwsh -NoProfile -File tools\repair_deucalion_20261020.ps1
```

It checks the game, plugin and Deucalion input hashes, applies the Global
opcodes and the receive-signature fix, then writes the matching bundle to
`tools/third_party/patch/`. Install all three files together; see
[the 2026-10-20 repair notes](tools/ActPluginPatch/DEUCALION_20261020.md).

- `tools\patch_act_plugin.cmd` only rewrites opcodes and expects the original
  Deucalion checksum. Running it on a repaired bundle breaks the
  plugin/Deucalion checksum pairing.
- History: `tools\repair_deucalion_757.ps1` and
  [DEUCALION_757.md](tools/ActPluginPatch/DEUCALION_757.md) cover the first
  fix for build `2026.10.06` with ACT 3.0.3.1. That ACT input is no longer
  stored here, so the script is kept for reference only. The full investigation
  is in [FFXIV_7.57_TECH_REPORT.md](tools/FFXIV_7.57_TECH_REPORT.md).

## Upstream References

This repo is glue code and local experiments around several upstream projects. It is not a clean-room implementation of every component used here.

- `tools/MachinaBridge` depends on `Machina` and `Machina.FFXIV` for FF14 network decoding.
- `tools/third_party/patch` holds the patched Deucalion DLL used for the direct pipe workflow. The unpatched 1.5.0 DLL reports `RECV OFF` on the current build.
- `tools/ActPluginPatch` patches a built `FFXIV_ACT_Plugin.dll` copy by rewriting its embedded opcode table; it does not rebuild the ACT plugin from source.
- `data/reference/FFXIV_ACT_Plugin.dll_Decompiled` is kept as reference material for investigating the plugin bundle layout and embedded opcode resources.

## Step 1: raw TCP payload capture

The first script only captures raw TCP payload bytes for the running
`ffxiv_dx11.exe` process. It does not decode FF14 packets yet.

Windows packet capture through WinDivert needs an elevated terminal.

```powershell
uv run python main.py --seconds 10 --max-payloads 100
```

To focus on the current game-server connection shown by the script, filter by
remote port:

```powershell
uv run python main.py --seconds 30 --max-payloads 100 --remote-port 55027
```

For quieter console output while still saving all records:

```powershell
uv run python main.py --seconds 30 --max-payloads 100 --remote-port 55027 --print-every 25
```

Use `--overwrite` when you want a fresh capture file:

```powershell
uv run python main.py --seconds 30 --max-payloads 100 --remote-port 55027 --print-every 0 --overwrite
```

If payload records are still zero, include empty TCP packets for diagnostics:

```powershell
uv run python main.py --seconds 10 --max-payloads 50 --remote-port 55027 --include-empty
```

Output is appended as JSONL:

```text
captures/raw_tcp_payloads.jsonl
```

Each record includes source, destination, payload length, and a hex preview of
the TCP payload.

## Step 2: decoded message bridge

The experimental .NET bridge uses Machina.FFXIV to emit decoded FF14 message
bytes as JSONL. Run from an elevated terminal.

```powershell
dotnet run --project tools\MachinaBridge -- --seconds 30 --max-messages 100 --output captures\decoded_messages.jsonl
```

To test Machina's Deucalion mode:

```powershell
dotnet run --project tools\MachinaBridge -- --seconds 30 --max-messages 100 --deucalion --deucalion-path tools\third_party\patch --output captures\decoded_messages.jsonl
```

Machina's own injector may check the Deucalion DLL checksum, so it may reject
the patched DLL in this mode. This has not been tested on the current build.
`--inject-only` below loads the DLL directly and is the tested path.

## Step 3: direct Deucalion pipe

The direct path gives more debug information than the Machina wrapper.

Inject the current Deucalion DLL:

```powershell
dotnet run --project tools\MachinaBridge -- --inject-only --deucalion-path tools\third_party\patch
```

Read Deucalion named-pipe payloads from Python:

```powershell
uv run python deucalion_client.py --seconds 30 --max-payloads 200 --overwrite
```

Expected first payload:

```text
SERVER HELLO. VERSION: 1.5.0. HOOK STATUS: RECV ON. SEND ON. SEND_LOBBY ON. CREATE_TARGET ON.
```

Analyze captured Zone IPC payloads:

```powershell
uv run python analyze_deucalion.py --limit 50
```

Filter for cast-start packets using the opcode table in `data/opcodes.json`:

```powershell
uv run python analyze_deucalion.py --name ActorCast --limit 50
```

`data/actions.csv` is exported from Triggevent's `xivdata` library and is used
to turn cast action IDs into names. To refresh it from the local Triggevent
install:

```powershell
$triggevent = "$env:USERPROFILE\Desktop\triggevent"
New-Item -ItemType Directory -Force tools\third_party\triggevent | Out-Null
Copy-Item "$triggevent\deps\xivdata-1.0-SNAPSHOT.jar" tools\third_party\triggevent\xivdata-1.0-SNAPSHOT.jar -Force
$cp = "tools\third_party\triggevent\xivdata-1.0-SNAPSHOT.jar;$triggevent\deps\slf4j-api-2.0.9.jar"
& "$triggevent\jre\bin\java.exe" -cp $cp tools\ExportTriggeventActions.java data\actions.csv
```

Watch cast-start packets in real time:

```powershell
dotnet run --project tools\MachinaBridge -- --inject-only --deucalion-path tools\third_party\patch
uv run python watch_casts.py
```

Stop after the first matching cast:

```powershell
uv run python watch_casts.py --max-events 1
```
