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
# fw= - The framework to build for.
build-fw:
	dotnet build EasyPost/EasyPost.csproj -c "Debug" -t:Rebuild -restore -f ${fw} -p:EnableNETAnalyzers=false

## build-prod - Build the project in Release mode
build-prod:
	dotnet build EasyPost/EasyPost.csproj -c "Release" -t:Rebuild -restore -p:EnableNETAnalyzers=false

## clean - Clean the project
clean:
	dotnet clean
	rm -rf *.nupkg

## coverage - Generate coverage reports for the project
coverage:
	./generate_test_reports.sh

## coverage-check - Check if the coverage is above the minimum threshold
coverage-check:
	./check_coverage.sh

## docs - Generates library documentation
docs:
	dotnet tool run docfx docs/docfx.json
	git add docs/* || exit 0

## format - Formats the project
format:
	dotnet tool run dotnet-format --no-restore

## install-tools - Install required dotnet tools
install-tools:
	dotnet new tool-manifest || exit 0
	dotnet tool install --local security-scan --version 5.6.3 || exit 0
	dotnet tool install --local dotnet-format || exit 0
	dotnet tool install --local docfx --version 2.60.2 || exit 0

## install - Install requirements
install: | install-tools
	git submodule init
	git submodule update

## lint - Lints the solution (EasyPost + Tests + F#/VB samples) (check IDE and SA rule violations)
lint:
    # Lint the source code with dotnet-format
	dotnet tool run dotnet-format --no-restore --check
    # Lint the source code by building with the "Linting" configuration (will trigger StyleCop)
	dotnet build EasyPost/EasyPost.csproj -c "Linting" -t:Rebuild -restore -p:EnforceCodeStyleInBuild=true

## lint-scripts - Lint and validate the Batch scripts (Windows only)
lint-scripts:
	scripts\lint_scripts.bat

## prep-release - Build, sign and package the project for distribution, signing with the provided certificate (Windows only)
# @parameters:
# sncert= - The strong-name certificate to use for signing the built assets.
# cert= - The authenticity certificate to use for signing the built assets.
# pass= - The password for the authenticity certificate.
prep-release:
	scripts\build_release_nuget.bat EasyPost ${sncert} ${cert} ${pass} EasyPost Release

## publish - Publish a specific NuGet file to nuget.org (Windows only)
# @parameters:
# file= - The NuGet file to publish
# key= - The API key for nuget.org
publish:
	scripts\publish_nuget.bat ${file} ${key}

## release - Cuts a release for the project on GitHub (requires GitHub CLI)
# tag = The associated tag title of the release
release:
	gh release create ${tag} *.nupkg

## restore - Restore the project
restore:
	dotnet restore

## scan - Scan the solution (EasyPost + Tests + F#/VB samples) for security issues (must run install-scanner first)
scan:
	dotnet tool run security-scan --verbose --no-banner --ignore-msbuild-errors EasyPost.sln
    # "--ignore-msbuild-errors" needed since MSBuild does not like F#: https://github.com/security-code-scan/security-code-scan/issues/235

## setup-win - Install required .NET versions and tools (Windows only)
setup-win:
	scripts\setup.bat

## setup-unix - Install required .NET versions and tools (Unix only)
setup-unix:
	bash scripts/setup.sh

## test - Test the project
test:
	dotnet test

## test-fw - Run the unit tests for a specific framework
# @parameters:
# fw= - The framework to build for.
test-fw:
    # Note, running .NET Framework tests on a non-Windows machine may cause issues: https://xunit.net/docs/getting-started/netfx/cmdline
	dotnet test EasyPost.Tests/EasyPost.Tests.csproj -f ${fw}

## uninstall-scanner - Uninstall SecurityCodeScan from your system
uninstall-scanner:
	dotnet tool uninstall security-scan

.PHONY: help analyze build build-fw build-prod clean coverage coverage-check docs format install-tools install lint lint-scripts pre-release publish-all publish release restore scan setup-win setup-unix test test-fw uninstall-scanner
