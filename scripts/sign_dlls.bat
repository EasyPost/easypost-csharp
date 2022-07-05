:: This script will find and sign any DLLs with a provided PFX certificate

:: Requirements:
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)

@ECHO OFF

:: Parse command line arguments
SET certFile=%1
SET certPass=%2
SET containerName=%3

:: Sign all DLLs found in the lib folder
@ECHO:
@ECHO Signing DLLs with certificate...
FOR /R "lib" %%F IN (*.dll) DO (
    REM We need to run the DLLs through both sn.exe and signtool to get complete the signing process
    sn -Rca "%%F" %containerName% || GOTO :commandFailed
    signtool sign /f %certFile% /p %certPass% /v /tr http://timestamp.digicert.com?alg=sha256 /td SHA256 /fd SHA256 "%%F" || GOTO :commandFailed
)

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
