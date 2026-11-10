# 2026-10-20 ACT / Deucalion bundle

Observed game build: `2026.10.16.0000.0000`, executable SHA256
`46a52ab1dbe80b9a8d6c4d4531bf66693904200c5135facacd053ef28a5de14a`.
The installed plugin is ACT FFXIV plugin 3.0.3.3. REA managed inspection
`ev_3b7e7a284c26210228d44ce08cce5c53115313a4999ead567a03817ae596fb30`
confirms its version and input SHA256
`af1031cdba8eeba8f53dd9d618e58d62d4919bfa77db9b31c568bd04cb359093`.
The source copy was taken from the local desktop installation; it was not
downloaded or authenticated as an official release during this repair.

## Observed failures

The first eight-second probe received two Zone payloads even though HELLO said
`RECV OFF`. ACT logged a successful manual initialization (`RECV OK`). HELLO
records startup flags, so it does not establish the current hook state after
manual initialization. Evidence: `captures/deucalion_health_20261020_before.json`.

The installed ACT plugin still contained the October 13 mappings:
`StatusEffectListForay3|22b`, `Ability1|bd`, `ActorCast|301`,
`ActorControlTarget|c8`. The user's October 20 table assigns `0x22b` to
`ActorControlTarget`. ACT's current log reported packet length 64 versus the
416 bytes required by `StatusEffectListForay3`, demonstrating wrong dispatch.

During investigation the desktop native DLL changed to the previously patched
version while the ACT plugin stayed unchanged. A subsequent ACT restart logged
`File checksum is invalid, cannot use dll`. This is a mismatched pair: the
plugin expects the original native hash, but the native DLL has the patched hash.

## Repair

The previous replacement signature still matches exactly three call sites in
this new executable, at file offsets `0x1d1d07b`, `0x1d27634`, `0x1d686a4`:

```text
E8 $ { ' } 4C 8B 47 10 44 8B 4F 1C
```

No new signature was needed. The rebuilt bundle uses the installed **3.0.3.3**
plugin, applies all 26 supplied opcode values, and applies the same native
signature replacement plus embedded checksum update. All other outer bundled
resources and other-region opcode resources were compared and preserved.

Inputs are stored in `tools/third_party/FFXIV_ACT_Plugin_3.0.3.3/`.
The original native input comes from the preserved 3.0.3.1 bundle and has SHA256
`326be4db261064ebb51b8c46d2940f55701d79887e2c65070aff00de4c8c65ef`.
The patched native hash remains
`fe84ded2594103a3319365306fa316bbaa9f80f7fc869803455cf9082529baeb`.
Checksum validation remains enabled.

```powershell
pwsh -NoProfile -File tools\repair_deucalion_20261020.ps1
```

The script requires the recorded input hashes and three signature matches before
generating outputs. Its internal `patch-deucalion-757` command name is historical;
the new wrapper validates the October 16 executable separately. The old
`repair_deucalion_757.ps1` intentionally remains restricted to its older build.
Previous output files are hash-verified in a timestamped backup under
`build/20261020-recv-fix/` before replacement.

## Installation and live verification

Close ACT, back up the existing plugin/native files, and install all three files
from `tools/third_party/patch/` into the actual plugin folder:

- `FFXIV_ACT_Plugin.dll`
- `deucalion-1.5.0.dll`
- `deucalion-1.5.0.distrib.dll`

Start ACT and run the probe below. Replacing files alone does not replace a DLL
already loaded in the game; if the old native instance remains active, restart
the game as well. A fresh-game-process test specifically requires a game restart.

```powershell
python tools\deucalion_probe.py --seconds 8 --output captures\deucalion_health_20261020_after.json
```

Require actual Zone payloads, no checksum error, and correct ACT damage events
(`21`/`22`) during a training-dummy test. Static bundle checks and the earlier
successful manual hook do not prove fresh native auto-initialization or damage
parsing with this bundle.

Installation completed on 2026-10-20 at approximately 19:04 JST into
`C:\Users\kyost\Desktop\`. All three installed hashes match the prepared bundle.
The installed ACT 3.0.3.3 SHA256 is
`aa2e7b9e8ba4479d73899d675d7895cf72e9db8dcde7dead06c03c627d4f122f`.
The embedded native checksum was read back and matches both native copies.
Installed opcode readback confirms `ActorControlTarget|22b` and
`StatusEffectListForay3|2d0`, resolving the observed dispatch mismatch on disk.
The wrapper also rejected a wrong game input without changing its output.

Desktop backups and a manifest recording which files previously existed are in
`build/desktop-act-20261020-before-install-20261020-190413-509/`.

After the user restarted ACT and attacked a training dummy, the 19:04:41 JST
session successfully injected Deucalion and reported `RECV ON` without a manual
receive signature request. The eight-second probe received two Zone packets.
ACT log lines 1033 onward contain type `21` action-effect events for attacks,
Fast Blade, Riot Blade, and Royal Authority, with damage effect type `03` and
nonzero effect values. No checksum or packet-processing errors were found after
the successful injection in the checked log segment. Full counts and the exact
checked boundary are recorded in `captures/act_20261020_verification.json`;
the pipe result is in `captures/deucalion_health_20261020_after.json`.

The game PID stayed 7816; ACT restart was sufficient for this observed session.
An independent fresh-game-process launch was not needed for this verification
and was not tested. The ACT UI's displayed DPS value was not inspected.
