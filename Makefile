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

## coverage - Generate coverage reports (unit tests, not integration) for the project
coverage:
	bash scripts/unix/generate_test_reports.sh

## coverage-check - Check if the coverage is above the minimum threshold
coverage-check:
	bash scripts/unix/check_coverage.sh 88

## docs - Generates library documentation
docs:
	dotnet tool run docfx docs/docfx.json

## install-tools - Install required dotnet tools
install-tools:
	dotnet new tool-manifest || exit 0
	dotnet tool install --local security-scan --version 5.6.3 || exit 0
	dotnet tool install --local dotnet-format || exit 0
	dotnet tool install --local docfx --version 2.60.2 || exit 0

## install-release-tools - Install required tools for release
install-release-tools:
	bash scripts/unix/install_osslsigncode.sh

## install-styleguide - Import style guide (Unix only)
install-styleguide: | update-examples-submodule
	sh examples/symlink_directory_files.sh examples/style_guides/csharp .

## install - Install requirements
install: | install-tools update-examples-submodule

## lint - Lints the solution (EasyPost + Tests + Integration + F#/VB compatibilities) (check IDE and SA rule violations)
lint:
    # Lint the project code with dotnet-format
	dotnet tool run dotnet-format --no-restore --check
    # Lint the source code (only EasyPost, no tests et. al) by building with the "Linting" configuration (will trigger StyleCop)
	dotnet build EasyPost/EasyPost.csproj -c "Linting" -t:Rebuild -restore -p:EnforceCodeStyleInBuild=true

## lint-fix - Formats the project
lint-fix:
	dotnet tool run dotnet-format --no-restore

## lint-scripts - Lint and validate the Batch scripts (Windows only)
lint-scripts:
	scripts\win\lint_scripts.bat

## prep-release - Build, sign and package the project for distribution, signing with the provided certificate
# @parameters:
# sncert= - The strong-name certificate to use for signing the built assets.
# cert= - The authenticity certificate to use for signing the built assets.
# pass= - The password for the authenticity certificate.
prep-release:
	bash scripts/unix/build_release_nuget.sh EasyPost ${sncert} ${cert} ${pass} Release

## release - Cuts a release for the project on GitHub (requires GitHub CLI)
# tag = The associated tag title of the release
release:
	gh release create ${tag} *.nupkg

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
	bash scripts/unix/setup.sh

## test - Run all tests in all projects (unit + integration + compatibility)
## EasyPost.Tests will run in all frameworks
## EasyPost.Integration, EasyPost.Compatibility.VB and EasyPost.Compatibility.FSharp will run only in net7.0
test:
	dotnet test

## unit-test - Run the unit tests for a specific framework
# @parameters:
# fw= - The framework to build for.
unit-test:
	dotnet test EasyPost.Tests/EasyPost.Tests.csproj -f ${fw} -c "Debug" # Always run unit tests in Debug mode to allow access to internal members

## update-examples-submodule - Update the examples submodule
update-examples-submodule:
	git submodule init
	git submodule update

## integration-test - Run the integration tests for a specific framework
## @parameters:
## fw= - The framework to build for.
integration-test:
	dotnet test EasyPost.Integration/EasyPost.Integration.csproj -f ${fw} -c "Release" -restore # Always run integration tests in Release mode to check the end-user experience

## fs-compat-test - Run the F# compatibility tests for a specific framework
## @parameters:
## fw= - The framework to build for.
fs-compat-test:
	dotnet test EasyPost.Compatibility.FSharp/EasyPost.Compatibility.FSharp.fsproj -f ${fw} -restore

## vb-compat-test - Run the VB compatibility tests for a specific framework
## @parameters:
## fw= - The framework to build for.
vb-compat-test:
	dotnet test EasyPost.Compatibility.VB/EasyPost.Compatibility.VB.vbproj -f ${fw} -restore

.PHONY: help analyze build build-fw build-prod clean coverage coverage-check docs format install-styleguide install-tools install-release-tools install lint lint-scripts prep-release release restore scan setup-win setup-unix test update-examples-submodule unit-test integration-test fs-compat-test vb-compat-test
