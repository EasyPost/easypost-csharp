#!/bin/sh

# check requirements
set -- dotnet reportgenerator
for req in "$@"; do
    if ! command -v "$req" > /dev/null 2>&1
    then
        echo "$req could not be found"
        exit
    fi
done

# generate reports for each version
set -- NetCore31 Net50 Net60 NetStandard20

for test in "$@"; do
    (
    test_folder="EasyPost.Tests.$test"
    cd "$test_folder" || exit
    dotnet test --collect:"XPlat Code Coverage"
    reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:../coveragereport/"$test" -reporttypes:Html
    )
done
