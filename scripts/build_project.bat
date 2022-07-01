:: This script will restore and build a .NET project in a specified mode and platform.

:: Requirements:
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)

@ECHO OFF

:: Parse command line arguments
SET buildMode=%1
SET buildPlatform=%2

:: Restore dependencies and build solution
@ECHO:
@ECHO Restoring and building project...
dotnet msbuild -property:Configuration="%buildMode%" -property:Platform="%buildPlatform%" -target:Rebuild -restore || GOTO :commandFailed

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
