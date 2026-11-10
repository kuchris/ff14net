# Opcode data

## Active tables

These are the files the tools read by default.

| File | Used by | Contents |
| --- | --- | --- |
| `opcodes.json` | `scripts/analyze_deucalion.py`, `scripts/watch_casts.py` | Upstream [FFXIVOpcodes](https://github.com/karashiiro/FFXIVOpcodes) format, all regions |
| `act_global_opcodes.txt` | `tools/patch_act_plugin.cmd` | Global table with ACT names |
| `xivalex_global_opcodes.json` | XivAlexander | Global server values |

Currently active Global table: **7.57, build `2026.10.16`**.

`tools/update_opcode_data.bat` overwrites all three active files from upstream.

## Global history (`global/`)

One file per game build, in ACT `Name|hex` format. Lines starting with `#`
are comments, and `ActPluginPatch patch-file` accepts these files directly.

| File | Game build | Source | Status |
| --- | --- | --- | --- |
| `7.57_2026.10.06.txt` | `2026.10.06.0000.0000` | User, 2026-10-13 | Superseded |
| `7.57_2026.10.16.txt` | `2026.10.16.0000.0000` | User, 2026-10-20 | Active; ACT damage events confirmed live |
| `7.58_2026.11.03.txt` | `2026.11.03.0000.0000` | Community, 2026-11-10 | Not yet tested |

When a new table arrives, add `global/<patch>_<build>.txt` with a source
comment, then copy its values into the active tables once it is in use.
