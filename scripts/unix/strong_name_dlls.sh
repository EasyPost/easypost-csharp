# This script will find and finish strong-naming any DLLs with a provided PFX certificate

# Requirements:
# - dotnet is installed on the machine and is accessible everywhere (added to PATH)
# - sn is installed on the machine and is accessible everywhere (added to PATH)

# Parse command line arguments
CERT_FILE=$1

# Set variables
FOLDER="lib"
FILE_PATTERN="*.dll"

# Strong-name all DLLs found in the lib folder
echo "Strong-naming (finishing delayed signing) DLLs with $CERT_FILE..."
for file in $(find "$FOLDER" -name "$FILE_PATTERN"); do
    echo "Strong-naming $file..."
    # Strong-name the file to a new file with added suffix
    sn -R "$file" "$CERT_FILE"
done
