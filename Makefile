## help - Display help about make targets for this Makefile
help:
	@cat Makefile | grep '^## ' --color=never | cut -c4- | sed -e "`printf 's/ - /\t- /;'`" | column -s "`printf '\t'`" -t

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

## build-dev - Build the project in Debug mode
build-dev:
	dotnet msbuild -property:Configuration="Debug" -target:Rebuild -restore

## build - Build the project in Release mode
build:
	dotnet msbuild -property:Configuration="Release" -target:Rebuild -restore

## install-cert - Install the PFX certificate to your system (Windows only)
# @parameters:
# cert= - The certificate to use for signing the built assets.
# pass= - The password for the certificate.
install-cert:
	scripts\install_cert.bat ${cert} ${pass}

## install-scanner - Install SecurityCodeScan to your system
install-scanner:
	dotnet tool install -g security-scan

## sign - Sign all generated DLLs and NuGet packages with the provided certificate (Windows only)
# @parameters:
# cert= - The certificate to use for signing the built assets.
# pass= - The password for the certificate.
sign:
	install-cert cert=${cert} pass=${pass}
	scripts\sign_assemblies.bat ${cert} ${pass} EasyPost

## clean - Clean the project
clean:
	dotnet clean

## restore - Restore the project
restore:
	dotnet restore

## lint - Lint the project
lint:
	dotnet format

## lint-check - Check lint issues in the project (does not make any changes)
lint-check:
	dotnet format --verify-no-changes

## test - Test the project
test:
	dotnet test

## lint-scripts - Lint and validate the Batch scripts (Windows only)
lint-scripts:
	scripts\lint_scripts.bat

## scan - Scan the project for security issues (must run install-scanner first)
# Makefile cannot access global dotnet tools, so you need to run the below command manually.
scan:
	security-scan --verbose --no-banner --ignore-msbuild-errors EasyPost.sln
	# "--ignore-msbuild-errors" needed since MSBuild does not like F#: https://github.com/security-code-scan/security-code-scan/issues/235

.PHONY: help release build-dev build install-cert sign clean restore lint lint-check test lint-scripts install-scanner scan
