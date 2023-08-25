#!/bin/bash

# This script will decrypt a GPG encrypted file.

# Usage: gpg_decrypt.sh <input_file> <password> <output_file>

INPUT_FILE=$1
PASSWORD=$2
OUTPUT_FILE=$3

gpg --decrypt --passphrase "$PASSWORD" --batch --output "$OUTPUT_FILE" "$INPUT_FILE"

# Exit with success
exit 0
