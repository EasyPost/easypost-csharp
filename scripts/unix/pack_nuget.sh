#!/bin/bash

# This script will generate a NuGet package according the the project's .nuspec file.

# Requirements:
# - NuGet is installed on the machine and is accessible everywhere (added to PATH)

# Parse command line arguments
PROJECT_NAME=$1

# Generate the NuGet package (will fail if assemblies are missing)
echo "Generating NuGet package..."
nuget pack "$PROJECT_NAME.nuspec" || exit 1
