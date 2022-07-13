:: This script will find and publish a NuGet packages to Nuget.org

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Parse command line arguments
SET package=%1
SET apiKey=%2

nuget push %package% -src https://api.nuget.org/v3/index.json -ApiKey %apiKey%

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
