#!/bin/sh

FRAMEWORK="net7.0"

# Navigate to the test folder
TEST_FOLDER="EasyPost.Tests"
cd "$TEST_FOLDER" || exit

RESULTS_FOLDER="TestResults"

# Delete old test results
rm -rf "$RESULTS_FOLDER"

# Generate coverage report
# https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-test#:~:text=To%20collect%20code%20coverage%20on%20any%20platform%20that%20is%20supported%20by%20.NET%20Core%2C
dotnet test --collect:"XPlat Code Coverage" -f $FRAMEWORK

# install reportgenerator if not already installed
dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.1.10 || true # exit 0 will kill the script, not what we want

# check reportgenerator is available
set -- dotnet reportgenerator
for req in "$@"; do
    if ! command -v "$req" >/dev/null 2>&1; then
        echo "$req could not be found"
        exit
    fi
done

COVERAGE_REPORT_FOLDER="../coveragereport"

# Delete old coverage reports
rm -rf "$COVERAGE_REPORT_FOLDER"

# Generate HTML and lcov report
reportgenerator -reports:$RESULTS_FOLDER/*/coverage.cobertura.xml -targetdir:$COVERAGE_REPORT_FOLDER -reporttypes:Html
reportgenerator -reports:$RESULTS_FOLDER/*/coverage.cobertura.xml -targetdir:$COVERAGE_REPORT_FOLDER -reporttypes:lcov
