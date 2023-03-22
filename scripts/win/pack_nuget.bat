:: This script will generate a NuGet package according the the project's .nuspec file.

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Parse command line arguments
SET projectName=%1

:: Generate a NuGet package (will fail if assemblies are missing)
@ECHO:
@ECHO Generating NuGet package...
nuget pack %projectName%.nuspec || GOTO :commandFailed

EXIT /B 0

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1
