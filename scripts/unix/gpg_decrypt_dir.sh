#!/bin/bash

# This script will encrypt all the files in a directory using GPG.

# Usage: gpg_encrypt_dir.sh <input_dir> <password> <suffix>

INPUT_DIR=$1
PASSWORD=$2
ENCRYPTED_SUFFIX=$3

# Loop through all the files in the input directory
for file in "$INPUT_DIR"/*
do
    # Output is file name minus the ENCRYPTED_SUFFIX
    output_file=${file%.$ENCRYPTED_SUFFIX}
    # Decrypt the file
    gpg --decrypt --passphrase "$PASSWORD" --batch --output "$output_file" "$file" 2>/dev/null # Ignore stderr
done