#!/bin/sh

FRAMEWORK="net9.0"

THRESHOLD=$1
THRESHOLD_TYPE=line

# Navigate to the test folder
TEST_FOLDER="EasyPost.Tests"
cd "$TEST_FOLDER" || exit

# Coverlet as a global tool does not report coverage properly (https://github.com/coverlet-coverage/coverlet/blob/2b8a4565b101ca70c3eaa9b5228c449dd3b81ad1/Documentation/GlobalTool.md)
# Instead, use the coverlet.msbuild package
# https://codeburst.io/code-coverage-in-net-core-projects-c3d6536fd7d7
# This will fail to run on machines without MSBuild installed

# If the coverage is below the threshold, exit with a non-zero exit code
dotnet test -p:CollectCoverage=true -p:CoverletOutput=TestResults/ -f $FRAMEWORK -p:Threshold="$THRESHOLD" -p:ThresholdType=$THRESHOLD_TYPE
