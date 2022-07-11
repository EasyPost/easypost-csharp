:: This script will delete any DLLs and NuGet packages

@ECHO OFF

:: Delete old files
@ECHO:
@ECHO Cleaning old files...
@RD /S /Q lib
DEL /S /Q /F *.nupkg

EXIT /B 0
