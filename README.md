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
tools/
  ActPluginPatch/     .NET: patch opcodes / Deucalion checksum in FFXIV_ACT_Plugin.dll
  MachinaBridge/      .NET: Machina decoder bridge and Deucalion injector
  ExportTriggeventActions.java   export data/actions.csv from Triggevent
  repair_deucalion_20261020.ps1  build the current ACT + Deucalion bundle
  patch_act_plugin.cmd           opcode-only ACT patch (see warning below)
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
| Game build | `2026.10.16.0000.0000` (patch label 7.57, not reconfirmed) |
| ACT FFXIV plugin input | 3.0.3.3, stored in `tools/third_party/FFXIV_ACT_Plugin_3.0.3.3/` |
| Deucalion | 1.5.0 with the patched receive signature |
| Prepared bundle | `tools/third_party/patch/` (plugin + both Deucalion copies) |
| Last live check | 2026-10-20: `RECV ON`, ACT type `21` damage events on a training dummy |

`tools/third_party/FFXIV_ACT_Plugin_3.0.3.4.zip` is a newer plugin download
that has not been extracted, patched or tested yet.

## Opcode data

All opcode files live in `data/opcodes/`; see
[data/opcodes/README.md](data/opcodes/README.md) for the full table list.

| File | Contents |
| --- | --- |
| `opcodes.json` | Upstream-format table for the Python readers |
| `act_global_opcodes.txt` | Active Global table with ACT names, `Name\|hex` |
| `xivalex_global_opcodes.json` | Global XivAlexander server values |
| `global/<patch>_<build>.txt` | Every Global table received, one per game build |

- **Global 7.58** (`2026.11.03`): community table stored in
  `global/7.58_2026.11.03.txt`; not active and not tested yet.
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
[docs/2026-10-20_deucalion-recv-repair.md](docs/2026-10-20_deucalion-recv-repair.md).

- `tools\patch_act_plugin.cmd` only rewrites opcodes and expects the original
  Deucalion checksum. Running it on a repaired bundle breaks the
  plugin/Deucalion checksum pairing. Details in
  [tools/ActPluginPatch/README.md](tools/ActPluginPatch/README.md).
- History: the first fix (build `2026.10.06`, ACT 3.0.3.1) is in
  [docs/2026-10-13_deucalion-recv-repair.md](docs/2026-10-13_deucalion-recv-repair.md)
  and the full investigation in
  [docs/2026-10-13_global-7.57-tech-report.md](docs/2026-10-13_global-7.57-tech-report.md).

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
