:: This script will build a .NET project in Release mode, sign the generated DLLs with a provided PFX certificate,
:: package the DLLs into a NuGet package, and sign the NuGet package with the provided PFX certificate.
:: This script also handles pre-run cleanup (will delete old DLLs and NuGet package files)

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)

@ECHO OFF

:: Parse command line arguments
SET projectName=%1
SET certFile=%2
SET buildMode=%5

:: Delete old files
CALL "scripts\delete_old_assemblies.bat"

:: Restore dependencies and build solution
CALL "scripts\build_project.bat" %buildMode% || GOTO :commandFailed

:: Sign the DLLs
CALL "scripts\sign_dlls.bat" %certFile% || GOTO :commandFailed

:: Package the DLLs in a NuGet package (will fail if DLLs are missing)
CALL "scripts\pack_nuget.bat" %projectName% || GOTO :commandFailed

:: Sign the NuGet package
CALL "scripts\sign_nuget.bat" %certFile% || GOTO :commandFailed

:: Gather the NuGet package
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
@ECHO Usage: %0 <PROJECT_NAME> <PATH_TO_CERTIFICATE> <BUILD_MODE>
GOTO :exitWithError

:commandFailed
@ECHO Step failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
