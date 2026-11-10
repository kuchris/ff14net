param(
    [string]$Game = 'D:\Steam\steamapps\common\FINAL FANTASY XIV Online\game\ffxiv_dx11.exe'
)

$ErrorActionPreference = 'Stop'
$taskRoot = Split-Path $PSScriptRoot -Parent
$taskSource = Join-Path $taskRoot 'tools\third_party\FFXIV_ACT_Plugin_3.0.3.3'
$taskOutput = Join-Path $taskRoot 'tools\third_party\patch'
$taskStage = Join-Path $taskRoot 'build\20261020-recv-fix'
$taskProject = Join-Path $taskRoot 'tools\ActPluginPatch\ActPluginPatch.csproj'
$taskOpcodes = Join-Path $taskRoot 'data\act_global_opcodes.txt'

# These are observed local inputs, not a claim of official release provenance.
$taskInputs = @{
    $Game = '46A52AB1DBE80B9A8D6C4D4531BF66693904200C5135FACACD053EF28A5DE14A'
    (Join-Path $taskSource 'FFXIV_ACT_Plugin.dll') = 'AF1031CDBA8EEBA8F53DD9D618E58D62D4919BFA77DB9B31C568BD04CB359093'
    (Join-Path $taskSource 'deucalion-1.5.0.distrib.dll') = '326BE4DB261064EBB51B8C46D2940F55701D79887E2C65070AFF00DE4C8C65EF'
}
foreach ($taskInput in $taskInputs.GetEnumerator()) {
    if ((Get-FileHash -LiteralPath $taskInput.Key -Algorithm SHA256).Hash -ne $taskInput.Value) {
        throw "Input hash mismatch: $($taskInput.Key). This repair requires game build 2026.10.16 and the recorded ACT 3.0.3.3 input."
    }
}
$taskGameBytes = [IO.File]::ReadAllBytes($Game)
$taskGameText = [Text.Encoding]::Latin1.GetString($taskGameBytes)
$taskPattern = '\xE8[\s\S]{4}\x4C\x8B\x47\x10\x44\x8B\x4F\x1C'
if ([regex]::Matches($taskGameText, $taskPattern).Count -ne 3) {
    throw 'Expected exactly three receive signature matches.'
}

New-Item -ItemType Directory -Force -Path $taskStage, $taskOutput | Out-Null
& dotnet run --project $taskProject -- patch-file `
    (Join-Path $taskSource 'FFXIV_ACT_Plugin.dll') `
    (Join-Path $taskStage 'FFXIV_ACT_Plugin.opcodes.dll') Global $taskOpcodes
if ($LASTEXITCODE -ne 0) { throw 'Opcode patch failed.' }

# The existing mode name is historical. The same equal-length native transform
# is applicable here after the new game hash and signature-count checks above.
& dotnet run --project $taskProject -- patch-deucalion-757 `
    (Join-Path $taskStage 'FFXIV_ACT_Plugin.opcodes.dll') `
    (Join-Path $taskStage 'FFXIV_ACT_Plugin.dll') `
    (Join-Path $taskSource 'deucalion-1.5.0.distrib.dll') `
    (Join-Path $taskStage 'deucalion-1.5.0.dll')
if ($LASTEXITCODE -ne 0) { throw 'Deucalion repair failed.' }

$taskDump = & dotnet run --project $taskProject -- dump (Join-Path $taskStage 'FFXIV_ACT_Plugin.dll') Global
if ($LASTEXITCODE -ne 0) { throw 'Cannot read patched opcode resource.' }
$taskEmbedded = @{}
foreach ($taskLine in $taskDump) {
    if ($taskLine -match '^([^|]+)\|([0-9a-fA-F]+)$') {
        $taskEmbedded[$matches[1]] = [Convert]::ToInt32($matches[2], 16)
    }
}
$taskCount = 0
foreach ($taskLine in Get-Content -LiteralPath $taskOpcodes) {
    if ($taskLine -match '^([^|]+)\|([0-9a-fA-F]+)$') {
        if (!$taskEmbedded.ContainsKey($matches[1]) -or $taskEmbedded[$matches[1]] -ne [Convert]::ToInt32($matches[2], 16)) {
            throw "Embedded opcode mismatch: $taskLine"
        }
        $taskCount++
    }
}
if ($taskCount -ne 26) { throw 'Expected 26 supplied opcodes.' }
if ((Get-FileHash -LiteralPath (Join-Path $taskStage 'deucalion-1.5.0.dll')).Hash -ne 'FE84DED2594103A3319365306FA316BBAA9F80F7FC869803455CF9082529BAEB') {
    throw 'Patched native hash mismatch.'
}

$taskBackup = Join-Path $taskStage ('before-' + (Get-Date -Format 'yyyyMMdd-HHmmss-fff'))
New-Item -ItemType Directory -Path $taskBackup | Out-Null
foreach ($taskName in @('FFXIV_ACT_Plugin.dll', 'deucalion-1.5.0.dll', 'deucalion-1.5.0.distrib.dll')) {
    $taskExisting = Join-Path $taskOutput $taskName
    if (Test-Path -LiteralPath $taskExisting) {
        Copy-Item -LiteralPath $taskExisting -Destination $taskBackup
        if ((Get-FileHash -LiteralPath $taskExisting).Hash -ne (Get-FileHash -LiteralPath (Join-Path $taskBackup $taskName)).Hash) {
            throw "Backup verification failed: $taskName"
        }
    }
}
Copy-Item -LiteralPath (Join-Path $taskStage 'FFXIV_ACT_Plugin.dll') -Destination $taskOutput -Force
Copy-Item -LiteralPath (Join-Path $taskStage 'deucalion-1.5.0.dll') -Destination $taskOutput -Force
Copy-Item -LiteralPath (Join-Path $taskStage 'deucalion-1.5.0.dll') -Destination (Join-Path $taskOutput 'deucalion-1.5.0.distrib.dll') -Force
Write-Host "PASS: 26 embedded opcodes and native hash verified. Prepared ACT 3.0.3.3 bundle in $taskOutput."
Write-Host "Previous output backup: $taskBackup"
Write-Host 'This command only prepares files; restart ACT/game after installing the matching bundle to test native auto-initialization.'
