"""Bounded Deucalion health check and validated Global 7.57 recv initialization."""
from __future__ import annotations

import argparse
import ctypes
from ctypes import wintypes
import hashlib
import json
import msvcrt
from pathlib import Path
import re
import struct
import time
from collections import Counter


SIGNATURE = "E8 $ { ' } 4C 8B 47 10 44 8B 4F 1C"
GAME_SHA256 = "b38840761e2677128b6bec1ceaee3f2870cd84e1d38ab7958216373058383780"


def validate_game(path: Path) -> None:
    data = path.read_bytes()
    if hashlib.sha256(data).hexdigest() != GAME_SHA256:
        raise RuntimeError("Game executable differs from the validated 7.57 build; refusing hook initialization.")
    matches = list(re.finditer(rb"\xe8....\x4c\x8b\x47\x10\x44\x8b\x4f\x1c", data, re.DOTALL))
    if len(matches) != 3:
        raise RuntimeError(f"Expected 3 recv call sites; found {len(matches)}.")
    print("Validated game SHA256 and 3 recv signature matches.", flush=True)


def probe(pid: int | None, seconds: float, initialize: bool, game: Path) -> dict:
    if initialize:
        validate_game(game)
    if pid is None:
        pipes = [p.name for p in Path("//./pipe/").iterdir() if re.fullmatch(r"deucalion-\d+", p.name)]
        if len(pipes) != 1:
            raise RuntimeError(f"Expected one Deucalion pipe, found {len(pipes)}; pass --pid.")
        pid = int(pipes[0].split("-")[1])
    peek = ctypes.WinDLL("kernel32", use_last_error=True).PeekNamedPipe
    peek.argtypes = [wintypes.HANDLE, wintypes.LPVOID, wintypes.DWORD,
                     ctypes.POINTER(wintypes.DWORD), ctypes.POINTER(wintypes.DWORD), ctypes.POINTER(wintypes.DWORD)]
    peek.restype = wintypes.BOOL
    counts: Counter[str] = Counter()
    opcodes: Counter[str] = Counter()
    debug: list[str] = []
    buffer = b""
    requested = False
    with open(rf"\\.\pipe\deucalion-{pid}", "r+b", buffering=0) as pipe:
        handle = msvcrt.get_osfhandle(pipe.fileno())

        def send(op: int, channel: int, data: bytes = b"") -> None:
            pipe.write(struct.pack("<IBI", 9 + len(data), op, channel) + data)

        send(0, 9000, b"ff14net_diagnostic")
        send(5, 6)
        deadline = time.monotonic() + seconds
        while time.monotonic() < deadline:
            available = wintypes.DWORD()
            if not peek(handle, None, 0, None, ctypes.byref(available), None):
                raise ctypes.WinError(ctypes.get_last_error())
            if not available.value:
                time.sleep(0.05)
                continue
            buffer += pipe.read(available.value)
            while len(buffer) >= 9:
                length, op, channel = struct.unpack_from("<IBI", buffer)
                if not 9 <= length <= 1048576:
                    raise RuntimeError(f"Invalid frame length: {length}")
                if len(buffer) < length:
                    break
                data, buffer = buffer[9:length], buffer[length:]
                counts[f"{op}:{channel}"] += 1
                if op == 0:
                    message = data.decode("utf-8", errors="replace")
                    debug.append(message)
                    print("DEBUG:", message, flush=True)
                    if initialize and not requested and message.startswith("SERVER HELLO."):
                        if "VERSION: 1.5.0." not in message:
                            raise RuntimeError("Expected Deucalion 1.5.0; refusing initialization.")
                        if "RECV OFF." in message:
                            send(3, 1, SIGNATURE.encode("ascii"))
                            requested = True
                if op == 3 and channel == 1 and len(data) >= 32:
                    opcodes[f"0x{struct.unpack_from('<H', data, 18)[0]:04x}"] += 1
    return {"pid": pid, "seconds": seconds, "recv_initialization_requested": requested,
            "debug": debug, "frame_counts": dict(counts), "zone_opcodes": dict(opcodes),
            "recv_zone_packets": counts["3:1"]}


def main() -> int:
    parser = argparse.ArgumentParser(description=__doc__)
    parser.add_argument("--pid", type=int)
    parser.add_argument("--seconds", type=float, default=8)
    parser.add_argument("--initialize-recv-757", action="store_true")
    parser.add_argument("--game", type=Path, default=Path(r"D:\Steam\steamapps\common\FINAL FANTASY XIV Online\game\ffxiv_dx11.exe"))
    parser.add_argument("--output", type=Path, default=Path("captures/deucalion_health.json"))
    args = parser.parse_args()
    if not 0 < args.seconds <= 60:
        parser.error("--seconds must be greater than 0 and at most 60")
    result = probe(args.pid, args.seconds, args.initialize_recv_757, args.game)
    args.output.parent.mkdir(parents=True, exist_ok=True)
    args.output.write_text(json.dumps(result, indent=2) + "\n", encoding="utf-8")
    print(json.dumps(result, indent=2))
    return 0 if result["recv_zone_packets"] else 2


if __name__ == "__main__":
    raise SystemExit(main())
