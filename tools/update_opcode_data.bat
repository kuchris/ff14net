@echo off
setlocal

set "ROOT=%~dp0.."
for %%I in ("%ROOT%") do set "ROOT=%%~fI"

powershell -NoProfile -ExecutionPolicy Bypass -Command ^
  "$ErrorActionPreference = 'Stop';" ^
  "$root = $env:ROOT;" ^
  "$dataDir = Join-Path $root 'data';" ^
  "$opcodesPath = Join-Path $dataDir 'opcodes.json';" ^
  "$actPath = Join-Path $dataDir 'act_global_opcodes.txt';" ^
  "$xivalexPath = Join-Path $dataDir 'xivalex_global_opcodes.json';" ^
  "$url = 'https://raw.githubusercontent.com/karashiiro/FFXIVOpcodes/master/opcodes.json';" ^
  "$tmp = Join-Path ([IO.Path]::GetTempPath()) 'ff14net-opcodes-current.json';" ^
  "Write-Host 'Downloading current opcodes...';" ^
  "curl.exe -fsSL $url -o $tmp;" ^
  "if ($LASTEXITCODE -ne 0) { throw 'Failed to download upstream opcodes.' }" ^
  "$json = Get-Content $tmp -Raw | ConvertFrom-Json;" ^
  "$global = $json | Where-Object { $_.region -eq 'Global' } | Select-Object -First 1;" ^
  "if (-not $global) { throw 'Global opcode entry not found.' }" ^
  "$existingAct = @{}; if (Test-Path -LiteralPath $actPath) { foreach ($line in Get-Content -LiteralPath $actPath) { if ($line -match '^([^|]+)\|([0-9a-fA-F]+)$') { $existingAct[$matches[1]] = [Convert]::ToInt32($matches[2], 16) } } }" ^
  "Copy-Item -LiteralPath $tmp -Destination $opcodesPath -Force;" ^
  "$server = @{}; foreach ($entry in $global.lists.ServerZoneIpcType) { $server[$entry.name] = [int]$entry.opcode }" ^
  "$client = @{}; foreach ($entry in $global.lists.ClientZoneIpcType) { $client[$entry.name] = [int]$entry.opcode }" ^
  "$actMap = [ordered]@{ StatusEffectList='StatusEffectList'; StatusEffectList2='StatusEffectListBozja'; StatusEffectList3='StatusEffectListPlayer'; BossStatusEffectList='StatusEffectListDouble'; StatusEffectListForay3='StatusEffectListOccult'; Ability1='Effect'; Ability8='AoeEffect8'; Ability16='AoeEffect16'; Ability24='AoeEffect24'; Ability32='AoeEffect32'; ActorCast='ActorCast'; EffectResult='EffectResult'; EffectResultBasic='EffectResultBasic'; ActorControl='ActorControl'; ActorControlSelf='ActorControlSelf'; ActorControlTarget='ActorControlTarget'; UpdateHpMpTp='UpdateHpMpTp'; PlayerSpawn='PlayerSpawn'; NpcSpawn='NpcSpawn'; NpcSpawn2='NpcSpawn2'; ActorMove='ActorMove'; ActorSetPos='ActorSetPos'; ActorGauge='ActorGauge'; PresetWaymark='PlaceFieldMarkerPreset'; Waymark='PlaceFieldMarker'; SystemLogMessage='SystemLogMessage' };" ^
  "$actLines = foreach ($kv in $actMap.GetEnumerator()) { if ($server.ContainsKey($kv.Value)) { '{0}|{1:X}' -f $kv.Key, $server[$kv.Value] } elseif ($existingAct.ContainsKey($kv.Key)) { Write-Warning ('Keeping existing ACT opcode for ' + $kv.Key + '; upstream name not found: ' + $kv.Value); '{0}|{1:X}' -f $kv.Key, $existingAct[$kv.Key] } else { throw ('Missing upstream opcode name: ' + $kv.Value) } };" ^
  "[IO.File]::WriteAllText($actPath, ($actLines -join [Environment]::NewLine), [Text.UTF8Encoding]::new($false));" ^
  "$act = @{}; foreach ($line in $actLines) { if ($line -match '^([^|]+)\|([0-9a-fA-F]+)$') { $act[$matches[1]] = [Convert]::ToInt32($matches[2], 16) } }" ^
  "$requiredClient = @('ActionRequest', 'ActionRequestGroundTargeted'); foreach ($name in $requiredClient) { if (-not $client.ContainsKey($name)) { throw ('Missing upstream client opcode name: ' + $name) } }" ^
  "$xivalexLines = @('{', ('  \"C2S_ActionRequest\": \"0x{0:x4}\",' -f $client['ActionRequest']), ('  \"C2S_ActionRequestGroundTargeted\": \"0x{0:x4}\",' -f $client['ActionRequestGroundTargeted']), '  \"Common_UseOodleTcp\": true,', '  \"PatchCode\": [],', ('  \"S2C_ActionEffect01\": \"0x{0:x4}\",' -f $act['Ability1']), ('  \"S2C_ActionEffect08\": \"0x{0:x4}\",' -f $act['Ability8']), ('  \"S2C_ActionEffect16\": \"0x{0:x4}\",' -f $act['Ability16']), ('  \"S2C_ActionEffect24\": \"0x{0:x4}\",' -f $act['Ability24']), ('  \"S2C_ActionEffect32\": \"0x{0:x4}\",' -f $act['Ability32']), ('  \"S2C_ActorCast\": \"0x{0:x4}\",' -f $act['ActorCast']), ('  \"S2C_ActorControl\": \"0x{0:x4}\",' -f $act['ActorControl']), ('  \"S2C_ActorControlSelf\": \"0x{0:x4}\",' -f $act['ActorControlSelf']), '  \"Server_IpRange\": \"0.0.0.0/0\",', '  \"Server_PortRange\": \"1-65535\"', '}');" ^
  "[IO.File]::WriteAllText($xivalexPath, ($xivalexLines -join [Environment]::NewLine), [Text.UTF8Encoding]::new($false));" ^
  "Get-Content $opcodesPath -Raw | ConvertFrom-Json | Out-Null;" ^
  "Get-Content $xivalexPath -Raw | ConvertFrom-Json | Out-Null;" ^
  "Write-Host ('Updated opcodes.json: Global ' + $global.version);" ^
  "Write-Host ('Updated ' + $actPath);" ^
  "Write-Host ('Updated ' + $xivalexPath);"

if errorlevel 1 exit /b %ERRORLEVEL%
exit /b 0
