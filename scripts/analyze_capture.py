from __future__ import annotations

import argparse
import ctypes
import json
import struct
import zlib
from collections import Counter
from dataclasses import dataclass
from pathlib import Path


MAGIC = bytes.fromhex("5252a041ff5d46e27f2a644d7b99c475")
BUNDLE_HEADER_SIZE = 40
COMPRESSION_NAMES = {
    0: "none",
    1: "zlib",
    2: "oodle",
}


@dataclass(frozen=True)
class BundleHeader:
    epoch: int
    length: int
    channel: int
    message_count: int
    version: int
    compression: int
    unknown1: int
    uncompressed_length: int


class OodleDecoder:
    HASH_TABLE_BITS = 17
    WINDOW_SIZE = 0x100000

    def __init__(self, dll_path: Path) -> None:
        self.dll_path = dll_path
        self.dll = ctypes.CDLL(str(dll_path))
        self.tcp_state_size = self.dll.OodleNetwork1TCP_State_Size
        self.tcp_state_size.argtypes = []
        self.tcp_state_size.restype = ctypes.c_int

        self.shared_size = self.dll.OodleNetwork1_Shared_Size
        self.shared_size.argtypes = [ctypes.c_int]
        self.shared_size.restype = ctypes.c_int

        self.set_window = self.dll.OodleNetwork1_Shared_SetWindow
        self.set_window.argtypes = [ctypes.c_void_p, ctypes.c_int, ctypes.c_void_p, ctypes.c_int]
        self.set_window.restype = None

        self.train = self.dll.OodleNetwork1TCP_Train
        self.train.argtypes = [ctypes.c_void_p, ctypes.c_void_p, ctypes.c_void_p, ctypes.c_void_p, ctypes.c_int]
        self.train.restype = None

        self.decode = self.dll.OodleNetwork1TCP_Decode
        self.decode.argtypes = [
            ctypes.c_void_p,
            ctypes.c_void_p,
            ctypes.c_void_p,
            ctypes.c_int,
            ctypes.c_void_p,
            ctypes.c_int,
        ]
        self.decode.restype = ctypes.c_bool

        state_size = self.tcp_state_size()
        shared_size = self.shared_size(self.HASH_TABLE_BITS)
        if state_size <= 0 or shared_size <= 0:
            raise RuntimeError(f"Invalid Oodle state/shared sizes: {state_size}/{shared_size}")

        self.state = ctypes.create_string_buffer(state_size)
        self.shared = ctypes.create_string_buffer(shared_size)
        self.window = ctypes.create_string_buffer(self.WINDOW_SIZE)
        self.set_window(self.shared, self.HASH_TABLE_BITS, self.window, self.WINDOW_SIZE)
        self.train(self.state, self.shared, None, None, 0)

    def decompress(self, data: bytes, raw_len: int) -> bytes:
        comp = ctypes.create_string_buffer(data)
        raw = ctypes.create_string_buffer(raw_len)
        ok = self.decode(self.state, self.shared, comp, len(data), raw, raw_len)
        if not ok:
            raise RuntimeError("OodleNetwork1TCP_Decode failed")
        return raw.raw[:raw_len]


def parse_bundle_header(payload: bytes) -> BundleHeader | None:
    if len(payload) < BUNDLE_HEADER_SIZE or not payload.startswith(MAGIC):
        return None

    # Machina stores epoch in network byte order inside an otherwise little-endian header.
    epoch = struct.unpack_from(">Q", payload, 16)[0]
    length = struct.unpack_from("<I", payload, 24)[0]
    channel = struct.unpack_from("<H", payload, 28)[0]
    message_count = struct.unpack_from("<H", payload, 30)[0]
    version = payload[32]
    compression = payload[33]
    unknown1 = struct.unpack_from("<H", payload, 34)[0]
    uncompressed_length = struct.unpack_from("<I", payload, 36)[0]
    return BundleHeader(epoch, length, channel, message_count, version, compression, unknown1, uncompressed_length)


def try_decompress(
    header: BundleHeader, payload: bytes, oodle: OodleDecoder | None
) -> tuple[str, bytes | None]:
    body = payload[BUNDLE_HEADER_SIZE : header.length]
    if header.compression == 0:
        return "ok", body
    if header.compression == 1:
        try:
            # Machina skips two zlib header bytes and inflates the remaining deflate stream.
            return "ok", zlib.decompress(payload[BUNDLE_HEADER_SIZE + 2 : header.length], -zlib.MAX_WBITS)
        except zlib.error as exc:
            return f"zlib_error:{exc}", None
    if header.compression == 2:
        if oodle is None:
            return "needs_oodle", None
        try:
            return "ok", oodle.decompress(body, header.uncompressed_length)
        except Exception as exc:
            return f"oodle_error:{exc}", None
    return "unknown_compression", None


def iter_message_lengths(decoded: bytes, count: int) -> list[int]:
    lengths: list[int] = []
    offset = 0
    for _ in range(count):
        if offset + 2 > len(decoded):
            break
        message_len = struct.unpack_from("<H", decoded, offset)[0]
        if message_len <= 0 or offset + message_len > len(decoded):
            break
        lengths.append(message_len)
        offset += message_len
    return lengths


def analyze(path: Path, limit: int, oodle_dll: Path | None) -> int:
    total = 0
    magic_count = 0
    compression_counts: Counter[str] = Counter()
    channel_counts: Counter[int] = Counter()
    status_counts: Counter[str] = Counter()
    oodle = OodleDecoder(oodle_dll) if oodle_dll else None
    if oodle:
        print(f"Using Oodle DLL: {oodle.dll_path}")
        print()

    with path.open("r", encoding="utf-8") as file:
        for line in file:
            if limit and total >= limit:
                break
            if not line.strip():
                continue
            total += 1
            record = json.loads(line)
            payload = bytes.fromhex(record.get("payload_hex", ""))
            header = parse_bundle_header(payload)
            if header is None:
                status_counts["no_bundle_magic"] += 1
                continue

            magic_count += 1
            compression_name = COMPRESSION_NAMES.get(header.compression, f"unknown:{header.compression}")
            compression_counts[compression_name] += 1
            channel_counts[header.channel] += 1

            status, decoded = try_decompress(header, payload, oodle)
            status_counts[status] += 1
            decoded_note = ""
            if decoded is not None:
                lengths = iter_message_lengths(decoded, header.message_count)
                decoded_note = f" decoded_len={len(decoded)} message_lengths={lengths}"

            print(
                f"{total:04d} {record.get('src')} -> {record.get('dst')} "
                f"bundle_len={header.length} channel={header.channel} messages={header.message_count} "
                f"version={header.version} compression={compression_name} "
                f"uncompressed_len={header.uncompressed_length} status={status}{decoded_note}"
            )

    print()
    print(f"records: {total}")
    print(f"bundle_magic: {magic_count}")
    print(f"compression: {dict(compression_counts)}")
    print(f"channels: {dict(channel_counts)}")
    print(f"status: {dict(status_counts)}")
    return 0


def parse_args() -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Analyze captured FF14 TCP payload bundle headers.")
    parser.add_argument("path", nargs="?", default="captures/raw_tcp_payloads.jsonl")
    parser.add_argument("--limit", type=int, default=25)
    parser.add_argument("--oodle-dll", type=Path, help="Path to oo2core_*_win64.dll for Oodle decompression.")
    return parser.parse_args()


def main() -> int:
    args = parse_args()
    return analyze(Path(args.path), args.limit, args.oodle_dll)


if __name__ == "__main__":
    raise SystemExit(main())
