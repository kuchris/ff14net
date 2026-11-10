from __future__ import annotations

import argparse
import json
import os
import struct
import sys
import time
from dataclasses import dataclass
from pathlib import Path

import psutil


OP_NAMES = {
    0: "Debug",
    1: "Ping",
    2: "Exit",
    3: "Recv",
    4: "Send",
    5: "Option",
    6: "RecvOther",
    7: "SendOther",
}
CHANNEL_NAMES = {
    0: "Lobby",
    1: "Zone",
    2: "Chat",
    9000: "Hello",
}


@dataclass(frozen=True)
class DeucalionPayload:
    op: int
    channel: int
    data: bytes


def find_game_pid() -> int:
    matches = [proc for proc in psutil.process_iter(["pid", "name"]) if (proc.info["name"] or "").lower() == "ffxiv_dx11.exe"]
    if not matches:
        raise RuntimeError("Could not find ffxiv_dx11.exe.")
    if len(matches) > 1:
        pids = ", ".join(str(proc.pid) for proc in matches)
        raise RuntimeError(f"Multiple ffxiv_dx11.exe processes found ({pids}); pass --pid.")
    return int(matches[0].pid)


def make_payload(op: int, channel: int, data: bytes = b"") -> bytes:
    length = 4 + 1 + 4 + len(data)
    return struct.pack("<IBI", length, op, channel) + data


def read_exact(file: object, size: int) -> bytes:
    chunks: list[bytes] = []
    remaining = size
    while remaining > 0:
        chunk = file.read(remaining)
        if not chunk:
            raise EOFError("Named pipe closed.")
        chunks.append(chunk)
        remaining -= len(chunk)
    return b"".join(chunks)


def read_payload(file: object) -> DeucalionPayload:
    header = read_exact(file, 9)
    length, op, channel = struct.unpack("<IBI", header)
    if length < 9:
        raise RuntimeError(f"Invalid Deucalion payload length: {length}")
    data = read_exact(file, length - 9)
    return DeucalionPayload(op=op, channel=channel, data=data)


def write_payload(file: object, op: int, channel: int, data: bytes = b"") -> None:
    file.write(make_payload(op, channel, data))
    file.flush()


def decode_debug(data: bytes) -> str | None:
    try:
        return data.decode("utf-8", errors="replace")
    except Exception:
        return None


def payload_record(payload: DeucalionPayload, max_bytes: int) -> dict[str, object]:
    debug_text = decode_debug(payload.data) if payload.op == 0 else None
    return {
        "time": time.time(),
        "op": payload.op,
        "op_name": OP_NAMES.get(payload.op, f"Unknown:{payload.op}"),
        "channel": payload.channel,
        "channel_name": CHANNEL_NAMES.get(payload.channel, f"Unknown:{payload.channel}"),
        "data_len": len(payload.data),
        "debug_text": debug_text,
        "data_hex": payload.data[:max_bytes].hex(),
    }


def connect_pipe(path: str, timeout: float) -> object:
    deadline = time.monotonic() + timeout
    last_error: Exception | None = None
    while time.monotonic() < deadline:
        try:
            return open(path, "r+b", buffering=0)
        except OSError as exc:
            last_error = exc
            time.sleep(0.2)
    raise RuntimeError(f"Could not connect to {path}: {last_error}")


def capture(args: argparse.Namespace) -> int:
    pid = args.pid or find_game_pid()
    pipe_path = rf"\\.\pipe\deucalion-{pid}"
    output_path = Path(args.output)
    output_path.parent.mkdir(parents=True, exist_ok=True)
    if args.overwrite:
        output_path.write_text("", encoding="utf-8")

    print(f"FF14 PID: {pid}")
    print(f"Pipe: {pipe_path}")
    print(f"Output: {output_path}")

    count = 0
    deadline = time.monotonic() + args.seconds
    with connect_pipe(pipe_path, args.connect_timeout) as pipe, output_path.open("a", encoding="utf-8") as out:
        if args.nickname:
            write_payload(pipe, 0, 9000, args.nickname.encode("utf-8"))
        if args.filter is not None:
            write_payload(pipe, 5, args.filter, b"")

        while time.monotonic() < deadline and count < args.max_payloads:
            payload = read_payload(pipe)
            record = payload_record(payload, args.max_bytes)
            out.write(json.dumps(record, separators=(",", ":")) + "\n")
            out.flush()
            count += 1

            should_print = args.print_every > 0 and (count <= 5 or count % args.print_every == 0)
            if should_print:
                debug = f" debug={record['debug_text']!r}" if record["debug_text"] else ""
                print(
                    f"{count:04d} op={record['op_name']} channel={record['channel_name']} "
                    f"len={record['data_len']}{debug}"
                )

    print(f"Payloads saved: {count}")
    return 0


def parse_args(argv: list[str]) -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Read direct Deucalion named-pipe payloads.")
    parser.add_argument("--pid", type=int, help="ffxiv_dx11.exe PID. Auto-detected when omitted.")
    parser.add_argument("--seconds", type=float, default=30.0)
    parser.add_argument("--max-payloads", type=int, default=100)
    parser.add_argument("--max-bytes", type=int, default=1024)
    parser.add_argument("--print-every", type=int, default=10)
    parser.add_argument("--connect-timeout", type=float, default=10.0)
    parser.add_argument("--nickname", default="ff14net")
    parser.add_argument(
        "--filter",
        type=int,
        default=(1 << 1) | (1 << 2) | (1 << 4) | (1 << 5),
        help="Deucalion Option bitflags. Default enables recv/send Zone and Chat.",
    )
    parser.add_argument("--output", default=os.path.join("captures", "deucalion_payloads.jsonl"))
    parser.add_argument("--overwrite", action="store_true")
    return parser.parse_args(argv)


def main(argv: list[str] | None = None) -> int:
    try:
        return capture(parse_args(argv or sys.argv[1:]))
    except KeyboardInterrupt:
        print("\nStopped.")
        return 130
    except Exception as exc:
        print(f"ERROR: {exc}", file=sys.stderr)
        return 1


if __name__ == "__main__":
    raise SystemExit(main())
