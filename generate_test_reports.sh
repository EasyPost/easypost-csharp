#!/bin/sh

# install reportgenerator if not already installed
dotnet tool install --global dotnet-reportgenerator-globaltool --version 5.1.10 || true # exit 0 will kill the script, not what we want

# check requirements
set -- dotnet reportgenerator
for req in "$@"; do
    if ! command -v "$req" >/dev/null 2>&1; then
        echo "$req could not be found"
        exit
    fi
done

# Generate Cobertura report for each test project
test_folder="EasyPost.Tests"
cd "$test_folder" || exit
dotnet test --collect:"XPlat Code Coverage"

# Generate HTML and lcov report
reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:../coveragereport -reporttypes:Html
reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:../coveragereport -reporttypes:lcov
