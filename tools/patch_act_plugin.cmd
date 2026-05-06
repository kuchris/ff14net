@echo off
setlocal

set "ROOT=%~dp0.."
for %%I in ("%ROOT%") do set "ROOT=%%~fI"

set "PROJECT=%ROOT%\tools\ActPluginPatch\ActPluginPatch.csproj"
set "INPUT_DLL=%ROOT%\tools\third_party\FFXIV_ACT_Plugin_3.0.1.8\FFXIV_ACT_Plugin.dll"
set "OPCODE_FILE=%ROOT%\data\act_global_opcodes.txt"
set "OUTPUT_DLL=%ROOT%\tools\third_party\FFXIV_ACT_Plugin_3.0.1.8\FFXIV_ACT_Plugin.patched.dll"
set "REGION=Global"

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
    echo   %INPUT_DLL%
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
