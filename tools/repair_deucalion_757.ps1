param()

$ErrorActionPreference = 'Stop'
$taskRoot = Split-Path $PSScriptRoot -Parent
$taskSource = Join-Path $taskRoot 'tools\third_party\FFXIV_ACT_Plugin_3.0.3.1'
$taskOutput = Join-Path $taskRoot 'tools\third_party\patch'
$taskStage = Join-Path $taskRoot 'build\757-recv-fix'
$taskProject = Join-Path $taskRoot 'tools\ActPluginPatch\ActPluginPatch.csproj'
$taskGame = 'D:\Steam\steamapps\common\FINAL FANTASY XIV Online\game\ffxiv_dx11.exe'
$taskExpectedGame = 'B38840761E2677128B6BEC1CEAEE3F2870CD84E1D38AB7958216373058383780'
if ((Get-FileHash -LiteralPath $taskGame -Algorithm SHA256).Hash -ne $taskExpectedGame) {
    throw 'This repair is restricted to the validated Global 7.57 executable (2026.10.06).'
}

New-Item -ItemType Directory -Force -Path $taskStage, $taskOutput | Out-Null
& dotnet run --project $taskProject -- patch-file `
    (Join-Path $taskSource 'FFXIV_ACT_Plugin.dll') `
    (Join-Path $taskStage 'FFXIV_ACT_Plugin.opcodes.dll') Global `
    (Join-Path $taskRoot 'data\act_global_opcodes.txt')
if ($LASTEXITCODE -ne 0) { throw 'Opcode patch failed.' }

& dotnet run --project $taskProject -- patch-deucalion-757 `
    (Join-Path $taskStage 'FFXIV_ACT_Plugin.opcodes.dll') `
    (Join-Path $taskStage 'FFXIV_ACT_Plugin.dll') `
    (Join-Path $taskSource 'deucalion-1.5.0.distrib.dll') `
    (Join-Path $taskStage 'deucalion-1.5.0.dll')
if ($LASTEXITCODE -ne 0) { throw 'Deucalion repair failed.' }

Copy-Item -LiteralPath (Join-Path $taskStage 'FFXIV_ACT_Plugin.dll') -Destination $taskOutput -Force
Copy-Item -LiteralPath (Join-Path $taskStage 'deucalion-1.5.0.dll') -Destination $taskOutput -Force
Copy-Item -LiteralPath (Join-Path $taskStage 'deucalion-1.5.0.dll') `
    -Destination (Join-Path $taskOutput 'deucalion-1.5.0.distrib.dll') -Force
Write-Host "Prepared all three DLLs in $taskOutput. Close ACT before installing them together."
