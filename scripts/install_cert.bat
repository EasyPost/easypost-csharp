:: This script will install a provided PFX certificate to the system.

:: Requirements:
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)
:: - SnInstallPfx (https://github.com/honzajscz/SnInstallPfx) is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Parse command line arguments
SET certFile=%1
SET certPass=%2
SET containerName=%3

:: Install certificate
@ECHO:
@ECHO (Re-)Installing certificate to system...
sn -d %containerName%
SnInstallPfx %certFile% %certPass% %containerName% || GOTO :commandFailed

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
