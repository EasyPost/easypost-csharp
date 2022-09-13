## help - Display help about make targets for this Makefile
help:
	@cat Makefile | grep '^## ' --color=never | cut -c4- | sed -e "`printf 's/ - /\t- /;'`" | column -s "`printf '\t'`" -t

## build - Build the project in Debug mode
build:
	dotnet msbuild -property:Configuration="Debug" -target:Rebuild -restore

## build-test-fw - Build the project for unit testing in Debug mode for a specific framework
# @parameters:
# fw= - The framework to build for.
build-test-fw:
	dotnet msbuild EasyPost.Tests/EasyPost.Tests.csproj -property:Configuration="Debug" -target:Rebuild -restore -property:TargetFramework=${fw}

## build-prod - Build the project in Release mode
build-prod:
	dotnet msbuild -property:Configuration="Release" -target:Rebuild -restore

## clean - Clean the project
clean:
	dotnet clean
	rm -rf *.nupkg

## coverage - Generate coverage reports for the project
coverage:
	./generate_test_reports.sh

## format - Formats the project
format:
	dotnet format

## install-cert - Install the PFX certificate to your system (Windows only)
# @parameters:
# cert= - The certificate to use for signing the built assets.
# pass= - The password for the certificate.
install-cert:
	scripts\install_cert.bat ${cert} ${pass}

## install-scanner - Install SecurityCodeScan to your system
install-scanner:
	dotnet tool install --local security-scan --version 5.6.3

## install - Install requirements
install:
	git submodule init
	git submodule update


## lint - Lints the project
lint:
	dotnet format --verify-no-changes

## lint-scripts - Lint and validate the Batch scripts (Windows only)
lint-scripts:
	scripts\lint_scripts.bat

## prep-release - Build, sign and package the project for distribution, signing with the provided certificate (Windows only)
# @parameters:
# cert= - The certificate to use for signing the built assets.
# pass= - The password for the certificate.
prep-release:
	scripts\build_release_nuget.bat EasyPost ${cert} ${pass} EasyPost Release

## publish-all - Publish all NuGet files to nuget.org.
# WARNING: Will publish ALL discovered NuGet files.
# @parameters:
# key= - The API key for nuget.org
publish-all:
	scripts\publish_all_nuget.bat ${key}

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

## scan - Scan the project for security issues (must run install-scanner first)
# Makefile cannot access global dotnet tools, so you need to run the below command manually.
scan:
	dotnet tool run security-scan --verbose --no-banner --ignore-msbuild-errors EasyPost.sln
    # "--ignore-msbuild-errors" needed since MSBuild does not like F#: https://github.com/security-code-scan/security-code-scan/issues/235

## setup - Install required .NET versions and tools (Windows only)
setup:
	scripts\setup.bat

## setup-tools - Set up the manifest files for dotnet tools
setup-tools:
	dotnet new tool-manifest

## sign - Sign all generated DLLs and NuGet packages with the provided certificate (Windows only)
# @parameters:
# cert= - The certificate to use for signing the built assets.
# pass= - The password for the certificate.
sign:
	install-cert cert=${cert} pass=${pass}
	scripts\sign_assemblies.bat ${cert} ${pass} EasyPost

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

.PHONY: help build build-test-fw build-prod clean format install-cert install-scanner install lint lint-scripts pre-release publish-all publish release restore scan setup setup-tools sign test test-fw uninstall-scanner
