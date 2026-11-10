from __future__ import annotations

import argparse
import datetime as dt
import sys
from pathlib import Path

from analyze_deucalion import (
    CHANNEL_ZONE,
    OP_RECV,
    decode_actor_cast,
    load_action_names,
    load_opcode_maps,
    parse_deucalion_segment,
)
from deucalion_client import connect_pipe, find_game_pid, read_payload, write_payload


DEFAULT_FILTER = (1 << 1) | (1 << 2)


def watch(args: argparse.Namespace) -> int:
    pid = args.pid or find_game_pid()
    pipe_path = rf"\\.\pipe\deucalion-{pid}"
    server_opcodes, _ = load_opcode_maps(args.opcodes, args.region)
    action_names = load_action_names(args.actions)

    print(f"FF14 PID: {pid}")
    print(f"Pipe: {pipe_path}")
    print(f"Watching for Recv Zone {args.name}...")

    matched = 0
    seen_zone = 0
    with connect_pipe(pipe_path, args.connect_timeout) as pipe:
        if args.nickname:
            write_payload(pipe, 0, 9000, args.nickname.encode("utf-8"))
        write_payload(pipe, 5, args.filter, b"")

        while args.max_events <= 0 or matched < args.max_events:
            payload = read_payload(pipe)
            if payload.op != OP_RECV or payload.channel != CHANNEL_ZONE:
                continue

            seen_zone += 1
            segment = parse_deucalion_segment(payload.data)
            if segment is None:
                continue

            opcode = int(segment["opcode"])
            opcode_name = server_opcodes.get(opcode, "Unknown")
            if opcode_name.lower() != args.name.lower():
                if args.verbose and seen_zone % args.verbose_every == 0:
                    print(f"seen_zone={seen_zone} latest=0x{opcode:04x} {opcode_name}")
                continue

            matched += 1
            timestamp = dt.datetime.now().strftime("%H:%M:%S")
            extra = decode_actor_cast(segment["body"], action_names) if opcode_name == "ActorCast" else ""
            print(
                f"[{timestamp}] {opcode_name} "
                f"src={segment['source_actor']} dst={segment['target_actor']} "
                f"opcode={segment['opcode_hex']} body_len={segment['body_len']} {extra}"
            )

    return 0


def parse_args(argv: list[str]) -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Watch direct Deucalion stream for cast-start packets.")
    parser.add_argument("--pid", type=int, help="ffxiv_dx11.exe PID. Auto-detected when omitted.")
    parser.add_argument("--opcodes", default="data/opcodes.json", type=Path)
    parser.add_argument("--actions", default="data/actions.csv", type=Path)
    parser.add_argument("--region", default="Global")
    parser.add_argument("--name", default="ActorCast", help="Server Zone opcode name to print.")
    parser.add_argument("--max-events", type=int, default=0, help="Stop after N matches. 0 means forever.")
    parser.add_argument("--connect-timeout", type=float, default=10.0)
    parser.add_argument("--nickname", default="ff14net-cast-watch")
    parser.add_argument("--filter", type=int, default=DEFAULT_FILTER, help="Deucalion filter flags. Default enables Recv Zone/Chat.")
    parser.add_argument("--verbose", action="store_true", help="Print occasional non-matching Zone opcode progress.")
    parser.add_argument("--verbose-every", type=int, default=100)
    return parser.parse_args(argv)


def main(argv: list[str] | None = None) -> int:
    try:
        return watch(parse_args(argv or sys.argv[1:]))
    except KeyboardInterrupt:
        print("\nStopped.")
        return 130
    except Exception as exc:
        print(f"ERROR: {exc}", file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
