#!/bin/bash

# This script will write a string to a file.

INPUT_STRING=$1
OUTPUT_FILE=$2

# Write the string to the file
echo "$INPUT_STRING" > "$OUTPUT_FILE"

# Exit with success
exit 0