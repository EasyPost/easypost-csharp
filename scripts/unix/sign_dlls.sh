# Parse command line arguments
CERT_FILE=$1
CERT_PASSWORD=$2

# Set variables
AUTHOR_NAME="EasyPost"
AUTHOR_URL="http://www.easypost.com"
TIMESTAMP_SERVER="http://timestamp.digicert.com?alg=sha256"
FOLDER="lib"
SUFFIX=".signed"
FILE_PATTERN="*.dll"

# Sign all DLLs found with our certificate to guarantee authenticity
echo "Signing DLLs with $CERT_FILE for authenticity..."
for file in $(find "$FOLDER" -name "$FILE_PATTERN"); do
    echo "Signing $file..."
    # Sign the file to a new file with added suffix
    osslsigncode sign -pkcs12 "$CERT_FILE" -pass "$CERT_PASSWORD" -n "$AUTHOR_NAME" -i "$AUTHOR_URL" -ts "$TIMESTAMP_SERVER" -in "$file" -out "$file$SUFFIX"
    # Delete original file
    rm -f "$file"
    # Rename signed file to original name
    mv "$file$SUFFIX" "$file"
done
