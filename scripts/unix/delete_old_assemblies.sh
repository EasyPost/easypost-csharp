#!/bin/bash

# This script will delete any DLLs and NuGet packages

# Delete old DLLs
echo "Cleaning old files..."
rm -rf lib
rm -rf "*.nupkg"
