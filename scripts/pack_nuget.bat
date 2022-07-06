:: This script will generate a NuGet package according the the project's .nuspec file.

:: Requirements:
:: - NuGet is installed on the machine and is accessible everywhere (added to PATH)

@ECHO OFF

:: Generate a NuGet package (will fail if assemblies are missing)
@ECHO:
@ECHO Generating NuGet package...
nuget pack %projectName%.nuspec || GOTO :commandFailed

:commandFailed
@ECHO Command failed.
GOTO :exitWithError

:exitWithError
@ECHO Exiting...
EXIT /B 1