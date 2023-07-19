#!/bin/bash

# This script will prepare a certificate needed for the release process on GitHub Actions, by decoding a base64 version of the certificate to a file.

CERT_BASE64_STRING=$1
CERT_FILE_NAME=$2

if [ -z "$CERT_BASE64_STRING" ]; then
  echo "No certificate provided"
  exit 1
fi

echo "Preparing $CERT_FILE_NAME certificate..."

TEMP_INPUT_FILE="$CERT_FILE_NAME.base64"

# Copy the base64 string to a temporary file
echo "$CERT_BASE64_STRING" > "$TEMP_INPUT_FILE"

# Decode the file contents from base64 in the output file
base64 -d -i "$TEMP_INPUT_FILE" -o "$CERT_FILE_NAME"

echo "Certificate $CERT_FILE_NAME prepared"

# Delete the temporary file
rm "$TEMP_INPUT_FILE"

# Exit with success
exit 0
