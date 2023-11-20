:: This script will find and sign any DLLs with a provided PFX certificate for authenticity

:: Requirements:
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)
:: - signtool is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x64)

@ECHO OFF

:: Parse command line arguments
SET certFingerprint=%1

:: Sign all DLLs found in the lib folder with our certificate to guarantee authenticity
@ECHO:
@ECHO Signing DLLs for authenticity...
FOR /R "lib" %%F IN (*.dll) DO (
    signtool sign /sha1 "%certFingerprint%" /tr http://timestamp.digicert.com /td SHA256 /fd SHA256 "%%F" || GOTO :commandFailed
    signtool verify /v /pa "%%F" || GOTO :commandFailed
)

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
