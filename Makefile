FW ?= net9.0

## help - Display help about make targets for this Makefile
help:
	@cat Makefile | grep '^## ' --color=never | cut -c4- | sed -e "`printf 's/ - /\t- /;'`" | column -s "`printf '\t'`" -t

## analyze - Run static analysis for the project (check CA rule violations)
analyze:
	dotnet build EasyPost/EasyPost.csproj -c "Release" -t:Rebuild -restore -p:EnableNETAnalyzers=true -p:CodeAnalysisTreatWarningsAsErrors=true -p:RunAnalyzersDuringBuild=true -p:AnalysisLevel=latest -p:AnalysisMode=Minimum

## build - Build the project in Debug mode
build:
	dotnet build EasyPost/EasyPost.csproj -c "Debug" -t:Rebuild -restore -p:EnableNETAnalyzers=false

## build-fw - Build the project in Debug mode for a specific framework
# @parameters:
# FW= - The framework to build for.
build-fw:
	dotnet build EasyPost/EasyPost.csproj -c "Debug" -t:Rebuild -restore -f ${FW} -p:EnableNETAnalyzers=false

## build-prod - Build the project in Release mode
build-prod:
	dotnet build EasyPost/EasyPost.csproj -c "Release" -t:Rebuild -restore -p:EnableNETAnalyzers=false

## clean - Clean the project
clean:
	dotnet clean

## coverage - Generate coverage reports (unit tests, not integration) for the project
coverage:
	./scripts/unix/generate_test_reports.sh

## coverage-check - Check if the coverage is above the minimum threshold
coverage-check:
	./scripts/unix/check_coverage.sh 85

## docs - Generates library documentation
docs:
	dotnet tool run docfx docs/docfx.json

## init-examples-submodule - Initialize the examples submodule
init-examples-submodule:
	git submodule init
	git submodule update

## install-tools - Install required dotnet tools
install-tools:
	dotnet new tool-manifest || exit 0
	dotnet tool install --local security-scan --version 5.6.3 || exit 0
	dotnet tool install --local dotnet-format || exit 0
	dotnet tool install --local docfx --version 2.60.2 || exit 0

## install-styleguide - Import style guide (Unix only)
install-styleguide: | update-examples-submodule
	sh examples/symlink_directory_files.sh examples/style_guides/csharp .

## install - Install requirements
install: | install-tools init-examples-submodule

## lint - Lints the solution (EasyPost + Tests + Integration + F#/VB compatibilities) (check IDE and SA rule violations)
## @parameters:
## FW= - The framework to build for.
lint:
    # Lint the project code with dotnet-format
	dotnet tool run dotnet-format --no-restore --check
    # Lint the source code (only EasyPost, no tests et. al) by building with the "Linting" configuration (will trigger StyleCop)
	dotnet build EasyPost/EasyPost.csproj -c "Linting" -t:Rebuild -restore -p:EnforceCodeStyleInBuild=true -f ${FW}

## lint-fix - Formats the project
lint-fix:
	dotnet tool run dotnet-format --no-restore

## lint-scripts - Lint and validate the Batch scripts (Windows only)
lint-scripts:
	scripts\win\lint_scripts.bat

## publish - Publish the project to NuGet
# @parameters:
# key= - The NuGet API key to use for publishing.
# ref: https://learn.microsoft.com/en-us/nuget/reference/cli-reference/cli-ref-push
publish:
	# Verify that no extraneous .nupkg files exist
	dotnet nuget push *.nupkg --source https://api.nuget.org/v3/index.json --api-key ${key} --skip-duplicate

## release - Cuts a release for the project on GitHub (requires GitHub CLI)
# tag = The associated tag title of the release
# target = Target branch or full commit SHA
release:
	gh release create ${tag} --target ${target}

## restore - Restore the project
restore:
	dotnet restore

## scan - Scan the solution (EasyPost + Tests + Integration + F#/VB compatibilities) for security issues (must run install-scanner first)
scan:
	dotnet tool run security-scan --verbose --no-banner --ignore-msbuild-errors EasyPost.sln
    # "--ignore-msbuild-errors" needed since MSBuild does not like F#: https://github.com/security-code-scan/security-code-scan/issues/235

## setup-win - Install required .NET versions and tools (Windows only)
setup-win:
	scripts\win\setup.bat

## setup-unix - Install required .NET versions and tools (Unix only)
setup-unix:
	./scripts/unix/setup.sh

## test - Run all tests in all projects in all configured frameworks (unit + integration + compatibility)
test:
	dotnet test

## unit-test - Run the unit tests for a specific framework
## Always run unit tests in Debug mode to allow access to internal members
## @parameters:
## FW= - The framework to build for.
unit-test:
	dotnet test EasyPost.Tests/EasyPost.Tests.csproj -f ${FW} -c "Debug"

## update-examples-submodule - Update the examples submodule
update-examples-submodule:
	git submodule init
	git submodule update --remote

## integration-test - Run the integration tests for a specific framework
## Always run integration tests in Release mode to check the end-user experience
## @parameters:
## FW= - The framework to build for.
integration-test:
	dotnet test EasyPost.Integration/EasyPost.Integration.csproj -f ${FW} -c "Release" -restore

## fs-compat-test - Run the F# compatibility tests for a specific framework
## @parameters:
## FW= - The framework to build for.
fs-compat-test:
	dotnet test EasyPost.Compatibility.FSharp/EasyPost.Compatibility.FSharp.fsproj -f ${FW} -restore

## vb-compat-test - Run the VB compatibility tests for a specific framework
## @parameters:
## FW= - The framework to build for.
vb-compat-test:
	dotnet test EasyPost.Compatibility.VB/EasyPost.Compatibility.VB.vbproj -f ${FW} -restore

## netstandard-compat-test - Run the Net Standard compatibility tests for a specific framework
## @parameters:
## FW= - The framework to build for.
netstandard-compat-test:
	dotnet test EasyPost.Compatibility.NetStandard/EasyPost.Compatibility.NetStandard.csproj -f ${FW} -restore

.PHONY: help analyze build build-fw build-prod clean coverage coverage-check docs format init-examples-submodule install-styleguide install-tools install lint lint-scripts release restore scan setup-win setup-unix test update-examples-submodule unit-test integration-test fs-compat-test vb-compat-test netstandard-compat-test
