from __future__ import annotations

import argparse
import csv
import json
import struct
from collections import Counter
from pathlib import Path


OP_RECV = 3
OP_SEND = 4
CHANNEL_ZONE = 1
DEUCALION_SEGMENT_HEADER_SIZE = 16
IPC_HEADER_SIZE = 16


def load_opcode_maps(path: Path, region: str) -> tuple[dict[int, str], dict[int, str]]:
    if not path.exists():
        return {}, {}

    records = json.loads(path.read_text(encoding="utf-8"))
    selected = next((item for item in records if item.get("region") == region), None)
    if selected is None:
        regions = ", ".join(str(item.get("region")) for item in records)
        raise RuntimeError(f"Region {region!r} not found in {path}. Available regions: {regions}")

    lists = selected.get("lists", {})
    server = {
        int(entry["opcode"]): str(entry["name"])
        for entry in lists.get("ServerZoneIpcType", [])
    }
    client = {
        int(entry["opcode"]): str(entry["name"])
        for entry in lists.get("ClientZoneIpcType", [])
    }
    return server, client


def load_action_names(path: Path) -> dict[int, str]:
    if not path.exists():
        return {}

    with path.open("r", encoding="utf-8", newline="") as file:
        return {
            int(row["id"]): row["name"]
            for row in csv.DictReader(file)
            if row.get("id") and row.get("name")
        }


def parse_deucalion_segment(data: bytes) -> dict[str, object] | None:
    if len(data) < DEUCALION_SEGMENT_HEADER_SIZE + IPC_HEADER_SIZE:
        return None

    source_actor, target_actor, timestamp_ms = struct.unpack_from("<IIQ", data, 0)
    ipc_offset = DEUCALION_SEGMENT_HEADER_SIZE
    reserved, opcode, padding, server_id, ipc_timestamp, padding1 = struct.unpack_from("<HHHHII", data, ipc_offset)
    body_offset = ipc_offset + IPC_HEADER_SIZE
    body = data[body_offset:]
    return {
        "source_actor": f"0x{source_actor:08x}",
        "target_actor": f"0x{target_actor:08x}",
        "timestamp_ms": timestamp_ms,
        "reserved": reserved,
        "opcode": opcode,
        "opcode_hex": f"0x{opcode:04x}",
        "padding": padding,
        "server_id": server_id,
        "ipc_timestamp": ipc_timestamp,
        "padding1": padding1,
        "body_len": len(body),
        "body_hex": body[:64].hex(),
        "body": body,
    }


def decode_actor_cast(body: bytes, action_names: dict[int, str] | None = None) -> str:
    if len(body) < 16:
        return "cast=short"

    # Keep this conservative: ActorCast fields can be scrambled/changed by
    # patch, so label raw candidates instead of pretending the body is solved.
    action_id, action_id_2, cast_time = struct.unpack_from("<IIf", body, 0)
    target_id = struct.unpack_from("<I", body, 12)[0] if len(body) >= 16 else 0
    action_name = (action_names or {}).get(action_id_2) or (action_names or {}).get(action_id)
    action_parts = (
        [f"action_id={action_id_2}", f"action_name={action_name!r}", f"raw_action_id_1={action_id}"]
        if action_name
        else [f"candidate_action_id={action_id}", f"candidate_action_id_2={action_id_2}"]
    )
    return " ".join(
        action_parts
        + [
            f"cast_time={cast_time:.3f}",
            f"body_target=0x{target_id:08x}",
        ]
    )


def parse_opcode(value: str) -> int:
    return int(value, 16) if value.lower().startswith("0x") else int(value)


def analyze(
    path: Path,
    limit: int,
    opcodes_path: Path,
    actions_path: Path,
    region: str,
    opcode_filter: int | None,
    name_filter: str | None,
) -> int:
    total = 0
    parsed = 0
    opcodes: Counter[int] = Counter()
    server_opcodes, client_opcodes = load_opcode_maps(opcodes_path, region)
    action_names = load_action_names(actions_path)

    with path.open("r", encoding="utf-8") as file:
        for line in file:
            if not line.strip():
                continue
            record = json.loads(line)
            if record.get("op") not in (OP_RECV, OP_SEND) or record.get("channel") != CHANNEL_ZONE:
                continue

            total += 1
            data = bytes.fromhex(record.get("data_hex", ""))
            segment = parse_deucalion_segment(data)
            if segment is None:
                print(f"{total:04d} {record.get('op_name')} Zone len={len(data)} parse=short")
                continue

            parsed += 1
            opcode = int(segment["opcode"])
            direction = str(record.get("op_name"))
            opcode_name = (
                server_opcodes.get(opcode)
                if record.get("op") == OP_RECV
                else client_opcodes.get(opcode)
            )
            opcode_label = opcode_name or "Unknown"

            if opcode_filter is not None and opcode != opcode_filter:
                continue
            if name_filter is not None and opcode_label.lower() != name_filter.lower():
                continue

            opcodes[opcode] += 1
            print(
                f"{total:04d} {direction} Zone "
                f"src={segment['source_actor']} dst={segment['target_actor']} "
                f"opcode={segment['opcode_hex']} {opcode_label} "
                f"body_len={segment['body_len']} body={segment['body_hex']}"
                + (f" {decode_actor_cast(segment['body'], action_names)}" if opcode_label == "ActorCast" else "")
            )

            if limit and parsed >= limit:
                break

    print()
    print(f"zone_payloads: {total}")
    print(f"parsed_segments: {parsed}")
    print("opcodes:")
    for opcode, count in opcodes.most_common(20):
        name = server_opcodes.get(opcode) or client_opcodes.get(opcode) or "Unknown"
        print(f"  0x{opcode:04x} {name}: {count}")
    return 0


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Analyze Deucalion IPC payload JSONL.")
    parser.add_argument("path", nargs="?", default="captures/deucalion_payloads.jsonl")
    parser.add_argument("--limit", type=int, default=25)
    parser.add_argument("--opcodes", default="data/opcodes.json", type=Path)
    parser.add_argument("--actions", default="data/actions.csv", type=Path)
    parser.add_argument("--region", default="Global")
    parser.add_argument("--opcode", type=parse_opcode, help="Only show one opcode, e.g. 0x0345.")
    parser.add_argument("--name", help="Only show one opcode name, e.g. ActorCast.")
    return parser.parse_args()


def main() -> int:
    args = parse_args()
    return analyze(Path(args.path), args.limit, args.opcodes, args.actions, args.region, args.opcode, args.name)


if __name__ == "__main__":
    raise SystemExit(main())
