# Global 7.57 receive hook repair

> **Historical.** Superseded by [DEUCALION_20261020.md](DEUCALION_20261020.md)
> for game build `2026.10.16` and ACT 3.0.3.3. The ACT 3.0.3.1 input used
> below is no longer stored in this repo.

The local game executable is build `2026.10.06.0000.0000`, SHA256
`b38840761e2677128b6bec1ceaee3f2870cd84e1d38ab7958216373058383780`.
ACT 3.0.3.1 loads Deucalion 1.5.0. Its session log reports that the default
`RecvPacket` signature does not match. A direct pipe probe reports `RECV OFF`
and zero received Zone packets. The sender and create-target hooks initialize.

The original pattern is:

```text
E8 $ { ' } 4C 8B 4F 10 8B 47 1C 45
```

The corresponding 7.57 call sites use different registers. The replacement is:

```text
E8 $ { ' } 4C 8B 47 10 44 8B 4F 1C
```

It matches exactly three call sites in the executable. Their call targets, in
ascending order, are `0x141d21c60`, `0x141d29a10`, and `0x141d6ad10`.
Deucalion's source assigns the three matches to Chat, Lobby, and Zone. These
addresses and the register change are static observations; packet reception and
damage parsing must still be verified at runtime.

Runtime verification on 2026-10-13 after installing the bundle: the pipe
reported `RECV ON` and received four Zone packets during an eight-second probe.
The training-dummy test produced five ACT type `21` events, including four with
damage effect type `03` and nonzero values. The user confirmed that the ACT
Damage/DPS display showed numbers. The earlier native decompilation request
timed out; this result is supported by byte matching, source inspection, and
the live packet and ACT checks.

The repair preserves DLL byte length and replaces three copies of the pattern
at native file offsets `0x148fc0`, `0x26dbcd`, and `0x28b7c7`. It also sets
`Machina.FFXIV.Deucalion.DeucalionInjector.get__checksum` to the SHA256 of the
patched native DLL. Checksum validation remains enabled. The original native
DLL must have SHA256
`326be4db261064ebb51b8c46d2940f55701d79887e2c65070aff00de4c8c65ef`.
The repaired DLL has SHA256
`fe84ded2594103a3319365306fa316bbaa9f80f7fc869803455cf9082529baeb`.

Generate the matching bundle from the unmodified local ACT 3.0.3.1 release:

```powershell
tools\repair_deucalion_757.ps1
```

Close ACT, then install **all three** output files together in its plugin folder:
`FFXIV_ACT_Plugin.dll`, `deucalion-1.5.0.dll`, and
`deucalion-1.5.0.distrib.dll`. Keep a backup first. Start ACT and run:

```powershell
python tools\deucalion_probe.py --seconds 8
```

Exit code 0 means received Zone packets were observed; code 2 means none were
observed during the bounded check. Hit a training dummy and verify ACT log
types `21`/`22` and nonzero damage. Packet reception alone does not establish
that the damage packet layout is correct.

The probe also supports `--initialize-recv-757` to submit the validated signature
through Deucalion's existing protocol without replacing the running DLL.
It checks the game hash before submitting anything. The HELLO hook flags are
captured at server startup and can remain stale after manual initialization;
actual received packets are the verification signal.

For this installation, the plugin path is
`C:\Users\kyost\Desktop\FFXIV_ACT_Plugin.dll`. The previous desktop DLLs are
saved in `build/desktop-act-before-recv-fix/`. ACT consumed the `.distrib.dll`
file during startup; the loaded `deucalion-1.5.0.dll` remained identical to the
prepared patched file.

The ordinary `patch_act_plugin.cmd` updates opcodes only. Running it again
replaces the ACT DLL with a version expecting the original native checksum;
use the repair script to regenerate a matching bundle afterward.

Sources: [Deucalion 1.5.0 source](https://github.com/ff14wed/deucalion/tree/5cc714ad4cc66aced1519042660026edff68e850),
[receive hook setup](https://github.com/ff14wed/deucalion/blob/5cc714ad4cc66aced1519042660026edff68e850/deucalion/src/hook/recv.rs),
[manual receive initialization protocol](https://github.com/ff14wed/deucalion#recv-op).
