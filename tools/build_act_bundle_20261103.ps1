param(
    [string]$Game = 'D:\Steam\steamapps\common\FINAL FANTASY XIV Online\game\ffxiv_dx11.exe'
)

# ACT 3.0.3.4 bundle for Global 7.58, game build 2026.11.03.0000.0000.
# Official Deucalion 1.5.1 already ships the receive signature that matches this
# build, so only the opcode table is patched; the native DLL is used unchanged.

$ErrorActionPreference = 'Stop'
$taskRoot = Split-Path $PSScriptRoot -Parent
$taskSource = Join-Path $taskRoot 'tools\third_party\FFXIV_ACT_Plugin_3.0.3.4'
$taskOutput = Join-Path $taskRoot 'tools\third_party\patch'
$taskStage = Join-Path $taskRoot 'build\20261103-bundle'
$taskProject = Join-Path $taskRoot 'tools\ActPluginPatch\ActPluginPatch.csproj'
$taskOpcodes = Join-Path $taskRoot 'data\opcodes\global\7.58_2026.11.03.txt'
$taskPlugin = Join-Path $taskSource 'FFXIV_ACT_Plugin.dll'
$taskNative = Join-Path $taskSource 'deucalion-1.5.1.distrib.dll'
$taskNativeHash = '4AC5D10790BAA319027108CB1AECFA74C4F8D0423464A31642B186FE507CA9B8'

# Observed local inputs: the 3.0.3.4 release zip and the installed game.
$taskInputs = @{
    $Game = 'CEB4260B2574476ACF72E7B6105D0A4F8D1BD78B4E7CAB5D8115C453F2F39339'
    $taskPlugin = 'F7F0C8894BA1A152B6A453427773496F5948CF412171DA773578D8671F643A81'
    $taskNative = $taskNativeHash
}
foreach ($taskInput in $taskInputs.GetEnumerator()) {
    if ((Get-FileHash -LiteralPath $taskInput.Key -Algorithm SHA256).Hash -ne $taskInput.Value) {
        throw "Input hash mismatch: $($taskInput.Key). This bundle requires game build 2026.11.03 and the ACT 3.0.3.4 release files."
    }
}

$taskGameText = [Text.Encoding]::GetEncoding(28591).GetString([IO.File]::ReadAllBytes($Game))
if ([regex]::Matches($taskGameText, '\xE8[\s\S]{4}\x4C\x8B\x47\x10\x44\x8B\x4F\x1C').Count -ne 3) {
    throw 'Expected exactly three receive signature matches for Deucalion 1.5.1.'
}

function Get-TaskChecksum([string]$PluginPath) {
    $value = & dotnet run --project $taskProject -- deucalion-checksum $PluginPath | Select-Object -Last 1
    if ($LASTEXITCODE -ne 0) { throw "Cannot read the Deucalion checksum from $PluginPath." }
    return $value.Trim().ToUpperInvariant()
}

if ((Get-TaskChecksum $taskPlugin) -ne $taskNativeHash) {
    throw 'The input plugin does not expect the bundled Deucalion 1.5.1 DLL.'
}

New-Item -ItemType Directory -Force -Path $taskStage, $taskOutput | Out-Null
$taskStagePlugin = Join-Path $taskStage 'FFXIV_ACT_Plugin.dll'
& dotnet run --project $taskProject -- patch-file $taskPlugin $taskStagePlugin Global $taskOpcodes
if ($LASTEXITCODE -ne 0) { throw 'Opcode patch failed.' }

$taskDump = & dotnet run --project $taskProject -- dump $taskStagePlugin Global
if ($LASTEXITCODE -ne 0) { throw 'Cannot read patched opcode resource.' }
$taskEmbedded = @{}
foreach ($taskLine in $taskDump) {
    if ($taskLine -match '^([^|]+)\|([0-9a-fA-F]+)$') {
        $taskEmbedded[$matches[1]] = [Convert]::ToInt32($matches[2], 16)
    }
}
$taskCount = 0
foreach ($taskLine in Get-Content -LiteralPath $taskOpcodes) {
    if ($taskLine -match '^([^|#]+)\|([0-9a-fA-F]+)$') {
        if (!$taskEmbedded.ContainsKey($matches[1]) -or $taskEmbedded[$matches[1]] -ne [Convert]::ToInt32($matches[2], 16)) {
            throw "Embedded opcode mismatch: $taskLine"
        }
        $taskCount++
    }
}
if ($taskCount -ne 26) { throw 'Expected 26 opcodes.' }
if ((Get-TaskChecksum $taskStagePlugin) -ne $taskNativeHash) {
    throw 'Patched plugin no longer expects the Deucalion 1.5.1 DLL.'
}

# Back up the previous bundle, then replace it with plugin + Deucalion 1.5.1.
$taskBackup = Join-Path $taskStage ('before-' + (Get-Date -Format 'yyyyMMdd-HHmmss-fff'))
New-Item -ItemType Directory -Path $taskBackup | Out-Null
foreach ($taskExisting in Get-ChildItem -LiteralPath $taskOutput -File) {
    Copy-Item -LiteralPath $taskExisting.FullName -Destination $taskBackup
    if ((Get-FileHash -LiteralPath $taskExisting.FullName).Hash -ne (Get-FileHash -LiteralPath (Join-Path $taskBackup $taskExisting.Name)).Hash) {
        throw "Backup verification failed: $($taskExisting.Name)"
    }
    Remove-Item -LiteralPath $taskExisting.FullName
}
Copy-Item -LiteralPath $taskStagePlugin -Destination $taskOutput
Copy-Item -LiteralPath $taskNative -Destination $taskOutput
Copy-Item -LiteralPath $taskNative -Destination (Join-Path $taskOutput 'deucalion-1.5.1.dll')

Write-Host "PASS: 26 embedded opcodes verified; plugin expects Deucalion 1.5.1 ($taskNativeHash)."
Write-Host "Prepared ACT 3.0.3.4 bundle in $taskOutput"
Write-Host "Previous bundle backup: $taskBackup"
Write-Host 'Install: close ACT, replace FFXIV_ACT_Plugin.dll, then restart ACT. Deucalion 1.5.1 is the official file.'
