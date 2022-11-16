:: This script will find and sign any DLLs with a provided PFX certificate

:: Requirements:
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)
:: - signtool is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files (x86)\Windows Kits\10\bin\10.0.18362.0\x64)

@ECHO OFF

:: Parse command line arguments
SET certFile=%1

:: Sign all DLLs found in the lib folder
@ECHO:
@ECHO Signing DLLs with certificate...
FOR /R "lib" %%F IN (*.dll) DO (
    signtool sign /f %certFile% /v /tr http://timestamp.digicert.com?alg=sha256 /td SHA256 /fd SHA256 "%%F" || GOTO :commandFailed
)

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
