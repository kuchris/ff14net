"""Make one data/opcodes/global/<patch>_<build>.txt table the active Global table.

Updates data/opcodes/act_global_opcodes.txt, the Global entry of
data/opcodes/opcodes.json and the S2C values in
data/opcodes/xivalex_global_opcodes.json. Other regions are left unchanged.
"""
from __future__ import annotations

import argparse
import json
import re
from pathlib import Path

OPCODE_DIR = Path("data/opcodes")

# ACT name -> upstream FFXIVOpcodes name (same mapping as tools/update_opcode_data.bat).
ACT_TO_UPSTREAM = {
    "StatusEffectList": "StatusEffectList",
    "StatusEffectList2": "StatusEffectListBozja",
    "StatusEffectList3": "StatusEffectListPlayer",
    "BossStatusEffectList": "StatusEffectListDouble",
    "StatusEffectListForay3": "StatusEffectListOccult",
    "Ability1": "Effect",
    "Ability8": "AoeEffect8",
    "Ability16": "AoeEffect16",
    "Ability24": "AoeEffect24",
    "Ability32": "AoeEffect32",
    "ActorCast": "ActorCast",
    "EffectResult": "EffectResult",
    "EffectResultBasic": "EffectResultBasic",
    "ActorControl": "ActorControl",
    "ActorControlSelf": "ActorControlSelf",
    "ActorControlTarget": "ActorControlTarget",
    "UpdateHpMpTp": "UpdateHpMpTp",
    "PlayerSpawn": "PlayerSpawn",
    "NpcSpawn": "NpcSpawn",
    "NpcSpawn2": "NpcSpawn2",
    "ActorMove": "ActorMove",
    "ActorSetPos": "ActorSetPos",
    "ActorGauge": "ActorGauge",
    "PresetWaymark": "PlaceFieldMarkerPreset",
    "Waymark": "PlaceFieldMarker",
    "SystemLogMessage": "SystemLogMessage",
}

# XivAlexander key -> ACT name.
XIVALEX_FROM_ACT = {
    "S2C_ActionEffect01": "Ability1",
    "S2C_ActionEffect08": "Ability8",
    "S2C_ActionEffect16": "Ability16",
    "S2C_ActionEffect24": "Ability24",
    "S2C_ActionEffect32": "Ability32",
    "S2C_ActorCast": "ActorCast",
    "S2C_ActorControl": "ActorControl",
    "S2C_ActorControlSelf": "ActorControlSelf",
}


def read_table(path: Path) -> dict[str, int]:
    table: dict[str, int] = {}
    for line in path.read_text(encoding="utf-8").splitlines():
        line = line.strip()
        if not line or line.startswith("#"):
            continue
        match = re.fullmatch(r"([^|]+)\|([0-9a-fA-F]+)", line)
        if not match:
            raise ValueError(f"{path}: bad line {line!r}")
        table[match[1]] = int(match[2], 16)
    if list(table) != list(ACT_TO_UPSTREAM):
        raise ValueError(f"{path}: expected the 26 ACT names in the standard order")
    return table


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("table", type=Path, help="e.g. data/opcodes/global/7.58_2026.11.03.txt")
    args = parser.parse_args()

    table = read_table(args.table)
    version = args.table.stem.split("_", 1)[0]

    act_path = OPCODE_DIR / "act_global_opcodes.txt"
    act_path.write_text("".join(f"{k}|{v:X}\n" for k, v in table.items()), encoding="utf-8")

    json_path = OPCODE_DIR / "opcodes.json"
    regions = json.loads(json_path.read_text(encoding="utf-8"))
    global_entry = next(e for e in regions if e["region"] == "Global")
    global_entry["version"] = version
    global_entry["lists"]["ServerZoneIpcType"] = [
        {"name": ACT_TO_UPSTREAM[k], "opcode": v} for k, v in table.items()
    ]
    json_path.write_text(json.dumps(regions, indent=2) + "\n", encoding="utf-8")

    xivalex_path = OPCODE_DIR / "xivalex_global_opcodes.json"
    xivalex = json.loads(xivalex_path.read_text(encoding="utf-8"))
    for key, act_name in XIVALEX_FROM_ACT.items():
        xivalex[key] = f"0x{table[act_name]:04x}"
    xivalex_path.write_text(json.dumps(dict(sorted(xivalex.items())), indent=2) + "\n", encoding="utf-8")

    print(f"Active Global table: {args.table} (version {version}, {len(table)} opcodes)")
    return 0


if __name__ == "__main__":
    raise SystemExit(main())
