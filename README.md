# ff14net

Small Python experiments for reading FF14 network data.

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
