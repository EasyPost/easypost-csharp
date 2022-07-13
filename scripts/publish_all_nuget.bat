:: This script will find and publish any NuGet packages to Nuget.org

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Parse command line arguments
SET apiKey=%1

:: Find all NuGet packages
@ECHO:
@ECHO Finding NuGet package(s)...
FOR /R %%F IN (*.nupkg) DO (
    @ECHO Found %%F. Publishing...
    nuget push "%%F" -src https://api.nuget.org/v3/index.json -ApiKey %apiKey%
)

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
