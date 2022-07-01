:: This script will build a .NET project in Release mode, sign the generated DLLs with a provided PFX certificate,
:: package the DLLs into a NuGet package, and sign the NuGet package with the provided PFX certificate.
:: This script also handles pre-run cleanup (will delete old DLLs and NuGet package files)

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)
:: - SnInstallPfx (https://github.com/honzajscz/SnInstallPfx) is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Parse command line arguments
SET projectName=%1
SET certFile=%2
SET certPass=%3
SET containerName=%4
SET buildMode=%5
SET buildPlatform=%6

:: Delete old files
CALL delete_old_assemblies.bat

:: Install certificate (needed to automate signing later on)
CALL install_cert.bat "%certFile%" "%certPass%" "%containerName%"

:: Restore dependencies and build solution
CALL build_project.bat "%buildMode%" "%buildPlatform%"

:: Sign the DLLs
CALL sign_dlls.bat "%certFile%" "%certPass%" "%containerName%"

:: Package the DLLs in a NuGet package (will fail if DLLs are missing)
CALL pack_nuget.bat "%projectName%"

:: Sign the NuGet package
CALL sign_nuget.bat "%certFile%" "%certPass%"
SET nugetFileName=
FOR /R %%F IN (*.nupkg) DO (
    SET nugetFileName="%%F"
)
IF [%nugetFileName%]==[] (
    ECHO Could not find NuGet package.
    GOTO :exitWithError
)

:: Present final information
ECHO:
ECHO NuGet file %nugetFileName% is ready.

GOTO :eof


:usage
@ECHO:
@ECHO Usage: %0 <PROJECT_NAME> <VERSION_COUNT> <PATH_TO_CERTIFICATE> <CERTIFICATE_PASSWORD>
GOTO :exitWithError

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
