# ff14net

Small experiments for reading FF14 network data, plus tools that keep the ACT
FFXIV plugin working on new game builds by patching opcodes and Deucalion.

## Layout

```text
scripts/            Python tools (run from the repo root with uv)
  capture_tcp.py      raw TCP payload capture through WinDivert
  analyze_capture.py  inspect raw TCP captures
  deucalion_client.py read the Deucalion named pipe into JSONL
  analyze_deucalion.py decode captured Zone IPC with data/opcodes/opcodes.json
  watch_casts.py      live cast-start viewer
  deucalion_probe.py  short Deucalion health check (RECV ON/OFF, packet count)
  activate_opcodes.py make one data/opcodes/global/ table the active one
tools/
  ActPluginPatch/     .NET: patch opcodes / Deucalion checksum in FFXIV_ACT_Plugin.dll
  MachinaBridge/      .NET: Machina decoder bridge and Deucalion injector
  ExportTriggeventActions.java   export data/actions.csv from Triggevent
  build_act_bundle_20261103.ps1  build the current ACT 3.0.3.4 bundle (7.58)
  repair_deucalion_20261020.ps1  previous ACT 3.0.3.3 + patched Deucalion bundle (7.57)
  patch_act_plugin.cmd           opcode-only ACT patch
  update_opcode_data.bat         download upstream opcode tables
  third_party/        ACT plugin inputs and the prepared bundle in patch/
data/
  opcodes/          active opcode tables; global/ keeps one file per game build
  actions.csv       action ID -> name
  reference/        decompiled ACT plugin reference
docs/               dated repair notes and the 7.57 technical report
build/, captures/   local outputs (git-ignored)
```

## Current status (Global)

| Item | Value |
| --- | --- |
| Game build | `2026.11.03.0000.0000` (7.58) |
| ACT FFXIV plugin input | 3.0.3.4, stored in `tools/third_party/FFXIV_ACT_Plugin_3.0.3.4/` |
| Deucalion | official 1.5.1, unchanged (already matches this build) |
| Opcodes | community 7.58 table, active since 2026-11-10 |
| Prepared bundle | `tools/third_party/patch/` (opcode-patched plugin + Deucalion 1.5.1) |
| Last live check | **not tested on 7.58 yet**; previous build passed on 2026-10-20 |

## Opcode data

All opcode files live in `data/opcodes/`; see
[data/opcodes/README.md](data/opcodes/README.md) for the full table list.

| File | Contents |
| --- | --- |
| `opcodes.json` | Upstream-format table for the Python readers |
| `act_global_opcodes.txt` | Active Global table with ACT names, `Name\|hex` |
| `xivalex_global_opcodes.json` | Global XivAlexander server values |
| `global/<patch>_<build>.txt` | Every Global table received, one per game build |

- **Global 7.58** (`2026.11.03`, active): the 26 server Zone opcodes from the
  community table. Other Global messages are unknown. Not yet checked against
  live packets.
- **Global 7.57**: earlier tables are kept in `global/` for reference.
- **XivAlexander**: server values only. The config stays incomplete until the
  `C2S_ActionRequest` and `C2S_ActionRequestGroundTargeted` values are known.

To switch the active Global table to another file in `global/`:

```powershell
uv run python scripts\activate_opcodes.py data\opcodes\global\7.58_2026.11.03.txt
```
- **CN / KR / TW**: taken from the upstream
  [FFXIVOpcodes](https://github.com/karashiiro/FFXIVOpcodes) download
  (CN `7.55a`, KR `7.55h`, TW `7.2`); not checked locally.

`tools/update_opcode_data.bat` downloads the upstream file and **overwrites all
three files**, including the hand-entered Global table. Check the upstream
Global version first.

## ACT / Deucalion repair

For the current build (`2026.11.03`), official Deucalion 1.5.1 already hooks
receive correctly, so only the ACT plugin's opcodes need patching:

```powershell
powershell -NoProfile -ExecutionPolicy Bypass -File tools\build_act_bundle_20261103.ps1
```

It checks the game, plugin and Deucalion input hashes and the checksum
pairing, patches the 7.58 opcodes into ACT 3.0.3.4, and writes the bundle to
`tools/third_party/patch/`. Only `FFXIV_ACT_Plugin.dll` needs to be installed;
see [docs/2026-11-10_act-3.0.3.4-global-7.58.md](docs/2026-11-10_act-3.0.3.4-global-7.58.md).
`ActPluginPatch deucalion-checksum <plugin>` prints the Deucalion SHA256 a
plugin accepts.

History:

- 2026-10-20, build `2026.10.16`, ACT 3.0.3.3: Deucalion 1.5.0 needed a
  patched receive signature and a matching plugin checksum
  (`tools\repair_deucalion_20261020.ps1`,
  [notes](docs/2026-10-20_deucalion-recv-repair.md)). Do not run
  `tools\patch_act_plugin.cmd` on that bundle; it breaks the checksum pairing.
- 2026-10-13, build `2026.10.06`, ACT 3.0.3.1: first fix
  ([notes](docs/2026-10-13_deucalion-recv-repair.md),
  [full report](docs/2026-10-13_global-7.57-tech-report.md)).

Check the pipe after installing:

```powershell
uv run python scripts\deucalion_probe.py --seconds 8
```

## Reading packets

All commands run from the repo root. Packet capture and DLL injection need an
elevated terminal.

### Raw TCP capture

Captures raw TCP payload bytes for `ffxiv_dx11.exe`; it does not decode FF14
packets. Output is appended to `captures/raw_tcp_payloads.jsonl`.

```powershell
uv run python scripts\capture_tcp.py --seconds 30 --max-payloads 100 --remote-port 55027
```

Useful flags: `--print-every 25` (quieter console), `--overwrite` (fresh file),
`--include-empty` (include empty TCP packets when no payloads show up).

### Direct Deucalion pipe (tested path)

```powershell
dotnet run --project tools\MachinaBridge -- --inject-only --deucalion-path tools\third_party\patch
uv run python scripts\deucalion_client.py --seconds 30 --max-payloads 200 --overwrite
```

Expected first payload:

```text
SERVER HELLO. VERSION: 1.5.0. HOOK STATUS: RECV ON. SEND ON. SEND_LOBBY ON. CREATE_TARGET ON.
```

Analyze the capture, optionally filtering by message name:

```powershell
uv run python scripts\analyze_deucalion.py --limit 50
uv run python scripts\analyze_deucalion.py --name ActorCast --limit 50
```

Watch cast-start packets live (after the injection command above):

```powershell
uv run python scripts\watch_casts.py --max-events 1
```

### Machina bridge

Emits Machina.FFXIV-decoded messages as JSONL:

```powershell
dotnet run --project tools\MachinaBridge -- --seconds 30 --max-messages 100 --output captures\decoded_messages.jsonl
```

Add `--deucalion --deucalion-path tools\third_party\patch` for Machina's
Deucalion mode. Machina's own injector may check the Deucalion checksum and
reject the patched DLL; this has not been tested on the current build.

### Refreshing action names

`data/actions.csv` is exported from Triggevent's `xivdata` library:

```powershell
$triggevent = "$env:USERPROFILE\Desktop\triggevent"
New-Item -ItemType Directory -Force tools\third_party\triggevent | Out-Null
Copy-Item "$triggevent\deps\xivdata-1.0-SNAPSHOT.jar" tools\third_party\triggevent\xivdata-1.0-SNAPSHOT.jar -Force
$cp = "tools\third_party\triggevent\xivdata-1.0-SNAPSHOT.jar;$triggevent\deps\slf4j-api-2.0.9.jar"
& "$triggevent\jre\bin\java.exe" -cp $cp tools\ExportTriggeventActions.java data\actions.csv
```

## Upstream references

This repo is glue code around several upstream projects, not a clean-room
implementation.

- `tools/MachinaBridge` depends on `Machina` and `Machina.FFXIV`.
- Deucalion: [ff14wed/deucalion](https://github.com/ff14wed/deucalion).
- `tools/ActPluginPatch` rewrites resources in a built `FFXIV_ACT_Plugin.dll`;
  it does not rebuild the plugin from source.
- `data/reference/FFXIV_ACT_Plugin.dll_Decompiled` is reference material for
  the plugin bundle layout and embedded opcode resources.
