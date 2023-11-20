:: This script will build a .NET project in Release mode, sign the generated DLLs with a provided PFX certificate,
:: package the DLLs into a NuGet package, and sign the NuGet package with the provided PFX certificate.
:: This script also handles pre-run cleanup (will delete old DLLs and NuGet package files)

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)

@ECHO OFF

:: Parse command line arguments
SET projectName=%1
SET strongNameCertFile=%2
SET authCertFingerprint=%3
SET buildMode=%4

:: Delete old files
CALL "scripts\win\delete_old_assemblies.bat"

:: Restore dependencies and build solution
CALL "scripts\win\build_project.bat" %buildMode% || GOTO :commandFailed

:: Strong-name the DLLs
CALL "scripts\win\strong_name_dlls.bat" %strongNameCertFile% || GOTO :commandFailed

:: Sign the DLLs for authenticity
CALL "scripts\win\sign_dlls.bat" %authCertFingerprint% || GOTO :commandFailed

:: Package the DLLs in a NuGet package (will fail if DLLs are missing)
CALL "scripts\win\pack_nuget.bat" %projectName% || GOTO :commandFailed

:: Sign the NuGet package for authenticity
CALL "scripts\win\sign_nuget.bat" %authCertFingerprint% || GOTO :commandFailed
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
@ECHO Usage: %0 <PROJECT_NAME> <PATH_TO_STRONG_NAME_CERTIFICATE> <AUTH_CERTIFICATE_FINGERPRINT> <BUILD_MODE>
GOTO :exitWithError

:commandFailed
@ECHO Step failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
