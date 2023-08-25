#!/bin/bash

# This script will encrypt a file using GPG.

# Usage: gpg_encrypt.sh <input_file> <password> <output_file>

INPUT_FILE=$1
PASSWORD=$2
OUTPUT_FILE=$3

gpg --symmetric --cipher-algo AES256 --passphrase "$PASSWORD" --batch --armor --yes --output "$OUTPUT_FILE" "$INPUT_FILE"

# Exit with success
exit 0
