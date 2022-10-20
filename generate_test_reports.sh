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

test_folder="EasyPost.Tests"
cd "$test_folder" || exit
dotnet test --collect:"XPlat Code Coverage"
reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:../coveragereport -reporttypes:Html
