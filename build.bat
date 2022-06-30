:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)
:: - SnInstallPfx (https://github.com/honzajscz/SnInstallPfx) is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Parse command line arguments
SET projectName=%1
SET /A versionCount=%2
SET certFile=%3
SET certPass=%4
SET containerName=EasyPost

:: Delete old files
@ECHO:
@ECHO Cleaning old files...
@RD /S /Q lib
DEL /S /Q /F *.nupkg

:: Install certificate (needed to automate signing later on)
@ECHO:
@ECHO (Re-)Installing certificate to system...
sn -d "%containerName%"
SnInstallPfx "%certFile%" "%certPass%" "%containerName%" || GOTO :commandFailed

:: Restore NuGet packages and dependencies
@ECHO:
@ECHO Restoring dependencies...
:: Runs nuget restore under the hood
:: Not really necessary, since restore is run as part of build below
dotnet restore || GOTO :commandFailed

:: Build solution in Release mode
:: Configuration details are hard-coded
@ECHO:
@ECHO Building project for release...
dotnet msbuild -property:Configuration="Release" -property:Platform="Any CPU" -target:Rebuild -restore || GOTO :commandFailed

:: Verify lib folder and DLL count
@ECHO:
@ECHO Verifying DLLs...
SET /A dllCount=0
FOR /R lib %%a IN (*.dll) DO (
    SET /A dllCount+=1
)
:: Might find additional ref DLLs made by the build, that's fine
@ECHO DLL Count: %dllCount%
@ECHO Expected DLL Count: %versionCount%
IF %dllCount% LSS %versionCount% (
    @ECHO Could not find expected number of DLLs.
    GOTO :exitWithError
)

:: Sign the DLLs
@ECHO:
@ECHO Signing DLLs with certificate...
FOR /R lib %%F IN (*.dll) DO (
    :: Don't ask why we need to do both, I don't know, we just do
    sn -Rca "%%F" "%containerName%" || GOTO :commandFailed
    signtool sign /f "%certFile%" /p "%certPass%" /v /tr http://timestamp.digicert.com?alg=sha256 /td SHA256 /fd SHA256 "%%F" || GOTO :commandFailed
)

:: Package the DLLs in a NuGet package
@ECHO:
@ECHO Generating NuGet package...
nuget pack %projectName%.nuspec || GOTO :commandFailed

:: Sign the NuGet package
@ECHO:
@ECHO Signing NuGet package with certificate...
:: Should only be one .nupkg file at this point, since we deleted the old ones
SET nugetFileName=
FOR /R %%F IN (*.nupkg) DO (
    SET nugetFileName="%%F"
    nuget sign "%%F" -Timestamper http://timestamp.digicert.com -CertificatePath "%certFile%" -CertificatePassword "%certPass%" || GOTO :commandFailed
)
IF [%nugetFileName%]==[] (
    ECHO Could not find NuGet package to sign.
    GOTO :exitWithError
)

:: Present final information
ECHO:
ECHO NuGet file %nugetFileName% is ready.

GOTO :eof


:usage
@ECHO:
@ECHO Usage: %0 "<PROJECT_NAME>" "<VERSION_COUNT>" "<PATH_TO_CERTIFICATE>" "<CERTIFICATE_PASSWORD>"
GOTO :exitWithError

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1