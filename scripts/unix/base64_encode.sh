#!/bin/bash

# This script will encode the contents of a file to a base64 string.

INPUT_FILE=$1
OUTPUT_FILE="$INPUT_FILE.base64"

# Encode the file contents to base64 in the output file
base64 -i "$INPUT_FILE" -o "$OUTPUT_FILE"

# Exit with success
exit 0

