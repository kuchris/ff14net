# ff14net

Small Python experiments for reading FF14 network data.

## Global 7.57 opcode data

The Global 7.57 table contains the 26 server Zone opcodes supplied by the user
on 2026-10-20 (replacing the 2026-10-13 mappings). The version label is retained
until the new game patch version is confirmed. These values have not been verified against live game packets.
`data/act_global_opcodes.txt` uses ACT names; `data/opcodes.json` uses the
corresponding upstream names for the Python readers. Other Global messages are
unknown, and the other regions retain their existing tables.

`data/xivalex_global_opcodes.json` contains the updated server values only.
It is an incomplete configuration until the 7.57 `C2S_ActionRequest` and
`C2S_ActionRequestGroundTargeted` values are supplied. The old client values
were removed because their validity for 7.57 is unknown.

For game build `2026.10.16.0000.0000` and the local ACT 3.0.3.3 input, run
`pwsh -NoProfile -File tools\repair_deucalion_20261020.ps1` to generate the matching
ACT/Deucalion bundle in `tools/third_party/patch/`. See
[the 2026-10-20 repair notes](tools/ActPluginPatch/DEUCALION_20261020.md).
The ordinary `tools\patch_act_plugin.cmd` updates only opcodes and expects the
original native DLL checksum; it can undo the checksum pairing of a repaired
bundle. Updating opcodes does not verify packet layouts or live parsing.
`tools/update_opcode_data.bat` replaces the local tables with
upstream data; check the downloaded Global version before using that workflow.

For the Global 7.57 Deucalion `RECV OFF` issue, use
`tools\repair_deucalion_757.ps1` to generate the matching ACT and Deucalion DLLs.
See [the repair notes](tools/ActPluginPatch/DEUCALION_757.md) for the exact build,
backup/install steps, and runtime verification commands.

## Upstream References

This repo is glue code and local experiments around several upstream projects. It is not a clean-room implementation of every component used here.

- `tools/MachinaBridge` depends on `Machina` and `Machina.FFXIV` for FF14 network decoding.
- `tools/third_party/deucalion` holds the Deucalion DLL used for the direct pipe workflow.
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
dotnet run --project tools\MachinaBridge -- --seconds 30 --max-messages 100 --deucalion --deucalion-path tools\third_party\deucalion --output captures\decoded_messages.jsonl
```

## Step 3: direct Deucalion pipe

The direct path gives more debug information than the Machina wrapper.

Inject the current Deucalion DLL:

```powershell
dotnet run --project tools\MachinaBridge -- --inject-only --deucalion-path tools\third_party\deucalion
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
dotnet run --project tools\MachinaBridge -- --inject-only --deucalion-path tools\third_party\deucalion
uv run python watch_casts.py
```

Stop after the first matching cast:

```powershell
uv run python watch_casts.py --max-events 1
```
