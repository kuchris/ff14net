@echo off
setlocal

set "ROOT=%~dp0.."
for %%I in ("%ROOT%") do set "ROOT=%%~fI"

set "PROJECT=%ROOT%\tools\ActPluginPatch\ActPluginPatch.csproj"
set "PLUGIN_ROOT=%ROOT%\tools\third_party"
set "OPCODE_FILE=%ROOT%\data\act_global_opcodes.txt"
set "REGION=Global"

for /f "usebackq delims=" %%I in (`powershell -NoProfile -ExecutionPolicy Bypass -Command "$root = $env:PLUGIN_ROOT; if (!(Test-Path -LiteralPath $root)) { exit 0 }; Get-ChildItem -LiteralPath $root -Directory -Filter 'FFXIV_ACT_Plugin_*' | ForEach-Object { $versionText = $_.Name.Substring('FFXIV_ACT_Plugin_'.Length); try { [pscustomobject]@{ Version = [version]$versionText; Path = $_.FullName } } catch {} } | Where-Object { Test-Path -LiteralPath (Join-Path $_.Path 'FFXIV_ACT_Plugin.dll') } | Sort-Object Version -Descending | Select-Object -First 1 -ExpandProperty Path"`) do set "PLUGIN_DIR=%%I"

if not "%PLUGIN_DIR%"=="" (
    set "INPUT_DLL=%PLUGIN_DIR%\FFXIV_ACT_Plugin.dll"
    set "OUTPUT_DLL=%PLUGIN_ROOT%\patch\FFXIV_ACT_Plugin.dll"
)

if not "%~1"=="" set "OPCODE_FILE=%~1"
if not "%~2"=="" set "OUTPUT_DLL=%~2"
if not "%~3"=="" set "REGION=%~3"

if not exist "%PROJECT%" (
    echo Missing patch project:
    echo   %PROJECT%
    exit /b 1
)

if not exist "%INPUT_DLL%" (
    echo Missing input plugin:
    echo   %PLUGIN_ROOT%\FFXIV_ACT_Plugin_*\FFXIV_ACT_Plugin.dll
    echo.
    echo Put the latest release DLL in a versioned folder, for example:
    echo   %PLUGIN_ROOT%\FFXIV_ACT_Plugin_3.0.2.1\FFXIV_ACT_Plugin.dll
    exit /b 1
)

if not exist "%OPCODE_FILE%" (
    echo Missing opcode file:
    echo   %OPCODE_FILE%
    exit /b 1
)

echo Patching ACT plugin...
echo   Input : %INPUT_DLL%
echo   Output: %OUTPUT_DLL%
echo   Region: %REGION%
echo   Table : %OPCODE_FILE%
echo.

dotnet run --project "%PROJECT%" -- patch-file "%INPUT_DLL%" "%OUTPUT_DLL%" "%REGION%" "%OPCODE_FILE%"
set "EXITCODE=%ERRORLEVEL%"

if not "%EXITCODE%"=="0" (
    echo.
    echo Patch failed with exit code %EXITCODE%.
    exit /b %EXITCODE%
)

echo.
echo Patch complete:
echo   %OUTPUT_DLL%
exit /b 0
