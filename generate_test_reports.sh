#!/bin/sh

tests=(NetCore31 Net50 Net60 NetFramework35)

for test in "${tests[@]}"; do
    test_folder="EasyPost.Tests.$test"
    cd "$test_folder"
    dotnet test --collect:"XPlat Code Coverage"
    reportgenerator -reports:TestResults/*/coverage.cobertura.xml -targetdir:../coveragereport/$test -reporttypes:Html
    cd ..
done
