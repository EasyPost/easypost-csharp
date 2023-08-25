#!/bin/bash

# This script will encrypt all the files in a directory using GPG.

# Usage: gpg_encrypt_dir.sh <input_dir> <password> <suffix>

INPUT_DIR=$1
PASSWORD=$2
OUTPUT_SUFFIX=$3

# Loop through all the files in the input directory
for file in "$INPUT_DIR"/*
do
    # Encrypt the file
    gpg --symmetric --passphrase "$PASSWORD" --batch --output "$file.$OUTPUT_SUFFIX" "$file"
done