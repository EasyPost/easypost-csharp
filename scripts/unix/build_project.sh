#!/bin/bash

# This script will restore and build a .NET project in a specified mode and platform.

# Requirements:
# - dotnet is installed on the machine and is accessible everywhere (added to PATH)

# Parse command line arguments
BUILD_MODE=$1

# Restore dependencies and build solution
echo "Restoring and building project..."
dotnet msbuild -property:Configuration="$BUILD_MODE" -target:Rebuild -restore || exit 1
