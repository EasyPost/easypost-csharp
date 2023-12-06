:: This script will find and sign any NuGet packages with a provided PFX certificate for authenticity

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Parse command line arguments
SET certFingerprint=%1

:: Sign all NuGet packages found with our certificate to guarantee authenticity
@ECHO:
@ECHO Signing NuGet package for authenticity...
:: Should only be one .nupkg file at this point, since we deleted the old ones
FOR /R %%F IN (*.nupkg) DO (
    nuget sign "%%F" -Timestamper http://timestamp.digicert.com -CertificateFingerprint "%certFingerprint%" -HashAlgorithm SHA256 -Verbosity detailed -Overwrite || GOTO :commandFailed
    nuget verify -All "%%F" || GOTO :commandFailed
)

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
