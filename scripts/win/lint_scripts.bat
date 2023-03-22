:: This script will install BatCodeCheck and run analysis on all Batch scripts in the "scripts" folder


:: Requirements:
:: - 7Zip is installed on the machine and is accessible everywhere (added to PATH)
:: - Might need to run with elevated privileges

@ECHO OFF

:: Download BatCodeCheck
curl https://www.robvanderwoude.com/files/batcodecheck.zip --output batcodecheck.zip || GOTO :commandFailed

:: Unzip BatCodeCheck
7z x batcodecheck.zip -y || GOTO :commandFailed

:: Analyze all Batch files found in the scripts folder
:: May complain about double-%, this is fine
:: However, don't want to falsely throw an error, so this does not fail on error
@ECHO:
@ECHO Verifying Batch files...
FOR /R scripts %%F IN (*.bat) DO (
    BatCodeCheck.exe "%%F" /LS
)

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
