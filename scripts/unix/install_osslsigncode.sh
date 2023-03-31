#!/bin/bash

# This script will install the osslsigncode dependency (https://github.com/mtrojnar/osslsigncode) required to release this project

PATH_STORAGE=/usr/local/bin

# Create a temporary folder
TEMP_FOLDER="temp"
mkdir -p "$TEMP_FOLDER"
cd "$TEMP_FOLDER" || exit

# Download the latest macOS release
# Courtesy: https://gist.github.com/steinwaywhw/a4cd19cda655b8249d908261a62687f8
REPO="mtrojnar/osslsigncode"
curl -s https://api.github.com/repos/$REPO/releases/latest \
| grep "browser_download_url.*macOS.zip" \
| cut -d : -f 2,3 \
| tr -d \" \
| wget -qi -

# Find the file name of the downloaded file
ZIP_FILE=$(find . -name "*macOS.zip")

# Unzip the file
unzip "$ZIP_FILE"

# Find the executable
OSSLSIGNCODE_EXE=$(find . -name "osslsigncode")

# Make the executable executable
chmod +x "$OSSLSIGNCODE_EXE"

# Move the executable to the PATH_STORAGE folder
mv "$OSSLSIGNCODE_EXE" "$PATH_STORAGE"

# Clean up
cd ..
rm -rf "$TEMP_FOLDER"
