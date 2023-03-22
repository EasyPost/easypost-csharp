# This script will find and sign any NuGet packages with a provided PFX certificate for authenticity

# Requirements:
# - NuGet is installed on the machine and is accessible everywhere (added to PATH)

# Parse command line arguments
CERT_FILE=$1
CERT_PASSWORD=$2

# Set variables
TIMESTAMP_SERVER="http://timestamp.digicert.com?alg=sha256"
FOLDER="."
FILE_PATTERN="*.nupkg"

# Sign all NuGet packages found with our certificate to guarantee authenticity
echo "Signing NuGet packages with $CERT_FILE for authenticity..."
# Should only be one .nupkg file at this point, since we deleted the old ones
for file in $(find "$FOLDER" -name "$FILE_PATTERN"); do
    # Sign the file in-place
    dotnet nuget sign "$file" --timestamper "$TIMESTAMP_SERVER" --certificate-path "$CERT_FILE" --certificate-password "$CERT_PASSWORD"
done
