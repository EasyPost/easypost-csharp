#!/usr/bin/env bash

# This script is used to download and install the required .NET versions

# If running Apple Silicon, use brew instead
if [[ $(sysctl -n machdep.cpu.brand_string) =~ "Apple" ]]; then
  echo "Apple Silicon detected, using brew to install .NET..."
  brew install dotnet || exit 0
fi

# .NET versions we want to install
declare -a NetVersions=("6.0" "7.0" "8.0" "9.0")

# Download dotnet-install.sh
echo "Downloading dotnet-install.sh script..."
curl -sSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh

# Install each .NET version
echo "Installing .NET versions..."
for version in "${NetVersions[@]}"; do
  echo "Installing .NET $version..."
  sudo bash ./dotnet-install.sh -c "$version"
done

# Remove dotnet-install.sh
echo "Removing dotnet-install.sh script..."
rm dotnet-install.sh
