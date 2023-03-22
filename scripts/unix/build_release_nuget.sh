#!/bin/bash

# This script will build a .NET project in Release mode, sign the generated DLLs with a provided PFX certificate,
# package the DLLs into a NuGet package, and sign the NuGet package with the provided PFX certificate.
# This script also handles pre-run cleanup (will delete old DLLs and NuGet package files)

# Requirements:
# - NuGet is installed on the machine and is accessible everywhere (added to PATH)
# - dotnet is installed on the machine and is accessible everywhere (added to PATH)
# - osslsigncode (https://github.com/mtrojnar/osslsigncode) is installed on the machine and is accessible everywhere (added to PATH)

# Parse command line arguments
PROJECT_NAME=$1
STRONG_NAME_CERT_FILE=$2
AUTH_CERT_FILE=$3
AUTH_CERT_PASSWORD=$4
BUILD_MODE=$5

# Delete old files
bash scripts/unix/delete_old_assemblies.sh

# Restore dependencies and build solution
bash scripts/unix/build_project.sh "$BUILD_MODE" || exit 1

# Strong-name sign the DLLs
bash scripts/unix/strong_name_dlls.sh "$STRONG_NAME_CERT_FILE" || exit 1

# Sign the DLLs for authenticity
bash scripts/unix/sign_dlls.sh "$AUTH_CERT_FILE" "$AUTH_CERT_PASSWORD" || exit 1

# Package the DLLs into a NuGet package (will fail if DLLs are missing)
bash scripts/unix/pack_nuget.sh "$PROJECT_NAME" || exit 1

# Sign the NuGet package for authenticity
bash scripts/unix/sign_nuget.sh "$AUTH_CERT_FILE" "$AUTH_CERT_PASSWORD" || exit 1

# Preset final information
NUGET_PACKAGE_FILE=$(find lib -name "*.nupkg")
echo "NuGet file $NUGET_PACKAGE_FILE is ready."
