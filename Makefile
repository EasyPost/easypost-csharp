## help - Display help about make targets for this Makefile
help:
	@cat Makefile | grep '^## ' --color=never | cut -c4- | sed -e "`printf 's/ - /\t- /;'`" | column -s "`printf '\t'`" -t

## release - Build, sign and package the project for distribution, signing with the provided certificate (Windows only)
# @parameters:
# cert= - The certificate to use for signing the built assets.
# pass= - The password for the certificate.
release:
	scripts/build_release_nuget EasyPost ${cert} ${pass} EasyPost "Release" "Any CPU"

## build-dev - Build the project in Debug mode
build-dev:
	dotnet msbuild -property:Configuration="Debug" -property:Platform="Any CPU" -target:Rebuild -restore

## build - Build the project in Release mode
build:
	dotnet msbuild -property:Configuration="Release" -property:Platform="Any CPU" -target:Rebuild -restore

## install-cert - Install the PFX certificate to your system (Windows only)
# @parameters:
# cert= - The certificate to use for signing the built assets.
# pass= - The password for the certificate.
install-cert:
	scripts/install_cert ${cert} ${pass}

## sign - Sign all generated DLLs and NuGet packages with the provided certificate (Windows only)
# @parameters:
# cert= - The certificate to use for signing the built assets.
# pass= - The password for the certificate.
sign:
	install-cert ${cert} ${pass}
	scripts/sign_assemblies ${cert} ${pass} EasyPost

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

.PHONY: help release build-dev build install-cert sign clean restore lint lint-check test
