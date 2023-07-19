#!/bin/bash

# This script will decode a base64 string to a file.

INPUT_FILE=$1
OUTPUT_FILE=$2

# Decode the file contents from base64 in the output file
base64 -d -i "$INPUT_FILE" -o "$OUTPUT_FILE"

# Exit with success
exit 0
