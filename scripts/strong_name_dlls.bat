:: This script will find and finish strong-naming any DLLs with a provided PFX certificate

:: Requirements:
:: - dotnet is installed on the machine and is accessible everywhere (added to PATH) (might be in C:\Program Files\dotnet)
:: - sn.exe is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Parse command line arguments
SET certFile=%1

:: Strong-name all DLLs found in the lib folder
@ECHO:
@ECHO Strong-naming (finishing delayed signing) DLLs with %certFile%...
FOR /R "lib" %%F IN (*.dll) DO (
    REM sn erroneously triggers command failed if we put a fallback on this
    sn -Ra "%%F" %certFile%
)

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
