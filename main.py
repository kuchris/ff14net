from __future__ import annotations

import argparse
import ctypes
import datetime as dt
import json
import os
import queue
import socket
import struct
import sys
import threading
import time
from collections import Counter
from dataclasses import dataclass
from pathlib import Path

import psutil
import pydivert
from pydivert import consts


DEFAULT_PROCESS_NAMES = ("ffxiv_dx11.exe", "ffxiv.exe")


@dataclass(frozen=True)
class TcpEndpoint:
    local_addr: str
    local_port: int
    remote_addr: str
    remote_port: int


@dataclass(frozen=True)
class TcpPacketInfo:
    src_addr: str
    src_port: str
    dst_addr: str
    dst_port: str
    flags: str
    seq_num: int | None
    ack_num: int | None
    raw_len: int
    raw_prefix_hex: str
    payload: bytes


def find_game_pid(process_name: str | None) -> int:
    names = (process_name.lower(),) if process_name else DEFAULT_PROCESS_NAMES
    names = tuple(name.lower() for name in names)

    matches: list[psutil.Process] = []
    for proc in psutil.process_iter(["pid", "name"]):
        try:
            name = (proc.info["name"] or "").lower()
        except psutil.Error:
            continue
        if name in names:
            matches.append(proc)

    if not matches:
        expected = ", ".join(names)
        raise RuntimeError(f"Could not find a running FF14 process matching: {expected}")
    if len(matches) > 1:
        pids = ", ".join(str(proc.pid) for proc in matches)
        raise RuntimeError(f"Multiple FF14 processes found ({pids}); pass --pid explicitly.")
    return matches[0].pid


def established_ipv4_connections(pid: int) -> list[TcpEndpoint]:
    endpoints: list[TcpEndpoint] = []
    proc = psutil.Process(pid)
    for conn in proc.net_connections(kind="tcp"):
        if conn.status != psutil.CONN_ESTABLISHED or not conn.laddr or not conn.raddr:
            continue
        if ip_family(conn.laddr.ip) != socket.AF_INET or ip_family(conn.raddr.ip) != socket.AF_INET:
            continue
        endpoints.append(
            TcpEndpoint(
                local_addr=conn.laddr.ip,
                local_port=conn.laddr.port,
                remote_addr=conn.raddr.ip,
                remote_port=conn.raddr.port,
            )
        )
    return endpoints


def filter_endpoints(endpoints: list[TcpEndpoint], remote_ports: set[int]) -> list[TcpEndpoint]:
    if not remote_ports:
        return endpoints
    return [ep for ep in endpoints if ep.remote_port in remote_ports]


def ip_family(address: str) -> socket.AddressFamily | None:
    for family in (socket.AF_INET, socket.AF_INET6):
        try:
            socket.inet_pton(family, address)
            return family
        except OSError:
            pass
    return None


def build_filter(endpoints: list[TcpEndpoint]) -> str:
    parts: list[str] = []
    for ep in endpoints:
        inbound = (
            f"(ip.SrcAddr == {ep.remote_addr} and tcp.SrcPort == {ep.remote_port} "
            f"and ip.DstAddr == {ep.local_addr} and tcp.DstPort == {ep.local_port})"
        )
        outbound = (
            f"(ip.SrcAddr == {ep.local_addr} and tcp.SrcPort == {ep.local_port} "
            f"and ip.DstAddr == {ep.remote_addr} and tcp.DstPort == {ep.remote_port})"
        )
        parts.append(f"({inbound} or {outbound})")
    return "tcp and (" + " or ".join(parts) + ")"


def is_windows_admin() -> bool:
    try:
        return bool(ctypes.windll.shell32.IsUserAnAdmin())
    except Exception:
        return False


def packet_raw(packet: object) -> bytes:
    raw = getattr(packet, "raw", b"")
    if raw is None:
        return b""
    return bytes(raw)


def packet_addrs(packet: object) -> tuple[str, str, str, str]:
    src_addr = getattr(packet, "src_addr", "")
    dst_addr = getattr(packet, "dst_addr", "")
    src_port = getattr(packet, "src_port", None)
    dst_port = getattr(packet, "dst_port", None)
    return value_text(src_addr), value_text(src_port), value_text(dst_addr), value_text(dst_port)


def value_text(value: object) -> str:
    return "?" if value is None else str(value)


def tcp_flags(packet: object) -> str:
    tcp = getattr(packet, "tcp", None)
    if tcp is None:
        return ""
    names = []
    for attr, label in (
        ("syn", "SYN"),
        ("ack", "ACK"),
        ("psh", "PSH"),
        ("fin", "FIN"),
        ("rst", "RST"),
        ("urg", "URG"),
    ):
        if getattr(tcp, attr, False):
            names.append(label)
    return ",".join(names)


def tcp_numbers(packet: object) -> tuple[int | None, int | None]:
    tcp = getattr(packet, "tcp", None)
    if tcp is None:
        return None, None
    seq_num = getattr(tcp, "seq_num", None)
    ack_num = getattr(tcp, "ack_num", None)
    return seq_num if isinstance(seq_num, int) else None, ack_num if isinstance(ack_num, int) else None


def parse_raw_ipv4_tcp(raw: bytes, max_prefix_bytes: int) -> TcpPacketInfo | None:
    if len(raw) < 40:
        return None

    version = raw[0] >> 4
    ihl = (raw[0] & 0x0F) * 4
    if version != 4 or ihl < 20 or len(raw) < ihl + 20:
        return None

    protocol = raw[9]
    if protocol != 6:
        return None

    total_len = struct.unpack_from("!H", raw, 2)[0]
    packet_len = min(total_len, len(raw)) if total_len else len(raw)
    if packet_len < ihl + 20:
        return None

    src_addr = socket.inet_ntoa(raw[12:16])
    dst_addr = socket.inet_ntoa(raw[16:20])
    tcp_start = ihl
    src_port, dst_port, seq_num, ack_num = struct.unpack_from("!HHII", raw, tcp_start)
    data_offset = (raw[tcp_start + 12] >> 4) * 4
    if data_offset < 20 or packet_len < tcp_start + data_offset:
        return None

    flags_value = raw[tcp_start + 13]
    flags = ",".join(
        label
        for bit, label in (
            (0x02, "SYN"),
            (0x10, "ACK"),
            (0x08, "PSH"),
            (0x01, "FIN"),
            (0x04, "RST"),
            (0x20, "URG"),
        )
        if flags_value & bit
    )
    payload_start = tcp_start + data_offset
    payload = raw[payload_start:packet_len]
    return TcpPacketInfo(
        src_addr=src_addr,
        src_port=str(src_port),
        dst_addr=dst_addr,
        dst_port=str(dst_port),
        flags=flags,
        seq_num=seq_num,
        ack_num=ack_num,
        raw_len=len(raw),
        raw_prefix_hex=raw[:max_prefix_bytes].hex(),
        payload=payload,
    )


def packet_info(packet: object, max_prefix_bytes: int) -> TcpPacketInfo:
    raw = packet_raw(packet)
    parsed = parse_raw_ipv4_tcp(raw, max_prefix_bytes)
    if parsed is not None:
        return parsed

    src_addr, src_port, dst_addr, dst_port = packet_addrs(packet)
    seq_num, ack_num = tcp_numbers(packet)
    payload = getattr(packet, "payload", b"")
    if payload is None:
        payload_bytes = b""
    elif isinstance(payload, bytes):
        payload_bytes = payload
    else:
        payload_bytes = bytes(payload)

    return TcpPacketInfo(
        src_addr=src_addr,
        src_port=src_port,
        dst_addr=dst_addr,
        dst_port=dst_port,
        flags=tcp_flags(packet),
        seq_num=seq_num,
        ack_num=ack_num,
        raw_len=len(raw),
        raw_prefix_hex=raw[:max_prefix_bytes].hex(),
        payload=payload_bytes,
    )


def close_handle(handle: pydivert.WinDivert) -> None:
    try:
        handle.close()
    except Exception:
        pass


def receive_packets(handle: pydivert.WinDivert, packets: queue.Queue[object], stop: threading.Event) -> None:
    while not stop.is_set():
        try:
            packets.put(handle.recv())
        except Exception as exc:
            if not stop.is_set():
                packets.put(exc)
            break


def capture_packets(
    args: argparse.Namespace, filter_text: str, output_path: Path
) -> tuple[int, int, Counter[str], Counter[str]]:
    deadline = time.monotonic() + args.seconds
    seen = 0
    saved = 0
    payload_by_flow: Counter[str] = Counter()
    empty_by_flow: Counter[str] = Counter()

    stop = threading.Event()
    packets: queue.Queue[object] = queue.Queue()
    handle = pydivert.WinDivert(filter_text, flags=consts.Flag.SNIFF)
    handle.open()
    receiver = threading.Thread(target=receive_packets, args=(handle, packets, stop), daemon=True)
    receiver.start()

    with output_path.open("a", encoding="utf-8") as out:
        try:
            while time.monotonic() < deadline and saved < args.max_payloads:
                remaining = max(0.001, deadline - time.monotonic())
                try:
                    item = packets.get(timeout=remaining)
                except queue.Empty:
                    break
                if isinstance(item, Exception):
                    raise item

                seen += 1
                info = packet_info(item, args.raw_prefix_bytes)
                payload = info.payload
                flow = f"{info.src_addr}:{info.src_port}->{info.dst_addr}:{info.dst_port}"
                if not payload and not args.include_empty:
                    empty_by_flow[flow] += 1
                    continue

                if payload:
                    payload_by_flow[flow] += 1
                else:
                    empty_by_flow[flow] += 1

                record = {
                    "time": dt.datetime.now(dt.UTC).isoformat(),
                    "src": f"{info.src_addr}:{info.src_port}",
                    "dst": f"{info.dst_addr}:{info.dst_port}",
                    "direction": str(getattr(item, "direction", "")),
                    "raw_len": info.raw_len,
                    "raw_prefix_hex": info.raw_prefix_hex,
                    "tcp_flags": info.flags,
                    "tcp_seq": info.seq_num,
                    "tcp_ack": info.ack_num,
                    "payload_len": len(payload),
                    "payload_hex": payload[: args.max_bytes].hex(),
                }
                out.write(json.dumps(record, separators=(",", ":")) + "\n")
                saved += 1

                should_print = args.print_every > 0 and (saved <= 3 or saved % args.print_every == 0)
                if should_print:
                    preview = record["payload_hex"][:96]
                    print(
                        f"{saved:04d} {record['src']} -> {record['dst']} "
                        f"flags={record['tcp_flags']} payload_len={record['payload_len']} hex={preview}"
                    )
        finally:
            stop.set()
            close_handle(handle)
            receiver.join(timeout=1.0)

    return seen, saved, payload_by_flow, empty_by_flow


def capture(args: argparse.Namespace) -> int:
    pid = args.pid if args.pid is not None else find_game_pid(args.process_name)
    endpoints = established_ipv4_connections(pid)
    remote_ports = set(args.remote_port or [])
    endpoints = filter_endpoints(endpoints, remote_ports)
    if not endpoints:
        port_note = f" matching remote ports {sorted(remote_ports)}" if remote_ports else ""
        raise RuntimeError(f"No established IPv4 TCP connections found for PID {pid}{port_note}.")

    print(f"FF14 PID: {pid}")
    print("Established TCP endpoints:")
    for ep in endpoints:
        print(f"  {ep.local_addr}:{ep.local_port} <-> {ep.remote_addr}:{ep.remote_port}")

    filter_text = build_filter(endpoints)
    print(f"\nWinDivert filter:\n  {filter_text}\n")

    if os.name == "nt" and not is_windows_admin():
        raise RuntimeError(
            "WinDivert capture needs an elevated terminal. "
            "Open PowerShell as Administrator in this folder and rerun the same uv command."
        )

    output_path = Path(args.output)
    output_path.parent.mkdir(parents=True, exist_ok=True)
    if args.overwrite:
        output_path.write_text("", encoding="utf-8")

    print(f"Capturing for {args.seconds:.1f}s in passive sniff mode...")
    seen, saved, payload_by_flow, empty_by_flow = capture_packets(args, filter_text, output_path)

    print(f"\nPackets seen: {seen}")
    print(f"Payload records saved: {saved}")
    if payload_by_flow:
        print("Payload packets by flow:")
        for flow, count in payload_by_flow.most_common():
            print(f"  {count:4d} {flow}")
    if empty_by_flow:
        print("Empty TCP packets by flow:")
        for flow, count in empty_by_flow.most_common():
            print(f"  {count:4d} {flow}")
    print(f"Output: {output_path}")
    return 0


def parse_args(argv: list[str]) -> argparse.Namespace:
    parser = argparse.ArgumentParser(description="Capture raw TCP payloads for the running FF14 process.")
    parser.add_argument("--pid", type=int, help="ffxiv_dx11.exe PID. Auto-detected when omitted.")
    parser.add_argument("--process-name", help="Process name to auto-detect. Default: ffxiv_dx11.exe.")
    parser.add_argument(
        "--remote-port",
        action="append",
        type=int,
        help="Only capture connections whose remote port matches. Repeat for multiple ports.",
    )
    parser.add_argument("--seconds", type=float, default=10.0, help="Capture duration.")
    parser.add_argument("--max-payloads", type=int, default=100, help="Stop after saving this many payload records.")
    parser.add_argument("--max-bytes", type=int, default=512, help="Maximum bytes saved per payload.")
    parser.add_argument("--raw-prefix-bytes", type=int, default=96, help="Raw packet prefix bytes saved for diagnostics.")
    parser.add_argument("--print-every", type=int, default=10, help="Print every N saved records. Use 0 for quiet.")
    parser.add_argument("--include-empty", action="store_true", help="Also save TCP packets without payload.")
    parser.add_argument("--overwrite", action="store_true", help="Clear the output file before capturing.")
    parser.add_argument(
        "--output",
        default=os.path.join("captures", "raw_tcp_payloads.jsonl"),
        help="JSONL output path.",
    )
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
