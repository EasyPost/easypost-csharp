FW := "net10.0"

# Run static analysis for the project (check CA rule violations)
analyze:
    dotnet build EasyPost/EasyPost.csproj -c Release -t:Rebuild -restore -p:EnableNETAnalyzers=true -p:CodeAnalysisTreatWarningsAsErrors=true -p:RunAnalyzersDuringBuild=true -p:AnalysisLevel=latest -p:AnalysisMode=Minimum

# Build the project in Debug mode
build:
    dotnet build EasyPost/EasyPost.csproj -c Debug -t:Rebuild -restore -p:EnableNETAnalyzers=false

# Build the project in Debug mode for a specific framework
build-fw fw=FW:
    dotnet build EasyPost/EasyPost.csproj -c Debug -t:Rebuild -restore -f {{fw}} -p:EnableNETAnalyzers=false

# Build the project in Release mode
build-prod:
    dotnet build EasyPost/EasyPost.csproj -c Release -t:Rebuild -restore -p:EnableNETAnalyzers=false

# Clean the project
clean:
    dotnet clean

# Generate coverage reports (unit tests, not integration) for the project
coverage:
    ./scripts/unix/generate_test_reports.sh

# Check if the coverage is above the minimum threshold
coverage-check:
    ./scripts/unix/check_coverage.sh 85

# Generates library documentation
docs:
    dotnet tool run docfx docs/docfx.json

# Initialize the examples submodule
init-examples-submodule:
    git submodule init
    git submodule update

# Install required dotnet tools
install-tools:
    dotnet new tool-manifest --force || exit 0
    dotnet tool install --local security-scan --version 5.6.3 || exit 0
    dotnet tool install --local dotnet-format || exit 0
    dotnet tool install --local docfx --version 2.60.2 || exit 0

# Import style guide (Unix only)
install-styleguide: init-examples-submodule
    sh examples/symlink_directory_files.sh examples/style_guides/csharp .

# Install requirements
install: install-tools init-examples-submodule

# Lints the solution (EasyPost + Tests + Integration + F#/VB compatibilities) (check IDE and SA rule violations)
lint fw=FW:
    # Lint the project code with dotnet-format
    dotnet tool run dotnet-format --no-restore --check
    # Lint the source code (only EasyPost, no tests et. al) by building with the "Linting" configuration (will trigger StyleCop)
    dotnet build EasyPost/EasyPost.csproj -c Linting -t:Rebuild -restore -p:EnforceCodeStyleInBuild=true -f {{fw}}

# Formats the project
lint-fix:
    dotnet tool run dotnet-format --no-restore

# Lint and validate the Batch scripts (Windows only)
lint-scripts:
    scripts\win\lint_scripts.bat

# Publish the project to NuGet
# key: The NuGet API key to use for publishing.
publish key:
    dotnet nuget push *.nupkg --source https://api.nuget.org/v3/index.json --api-key {{key}} --skip-duplicate

# Cuts a release for the project on GitHub (requires GitHub CLI)
# tag: The associated tag title of the release
# target: Target branch or full commit SHA
release tag target:
    gh release create {{tag}} --target {{target}}

# Restore the project
restore:
    dotnet restore

# Scan the solution (EasyPost + Tests + Integration + F#/VB compatibilities) for security issues (must run install-scanner first)
scan:
    dotnet tool run security-scan --verbose --no-banner --ignore-msbuild-errors EasyPost.sln

# Install required .NET versions and tools (Windows only)
setup-win:
    scripts\win\setup.bat

# Install required .NET versions and tools (Unix only)
setup-unix:
    ./scripts/unix/setup.sh

# Run all tests in all projects in all configured frameworks (unit + integration + compatibility)
test:
    dotnet test

# Run the unit tests for a specific framework
# Always run unit tests in Debug mode to allow access to internal members
unit-test fw=FW:
    dotnet test EasyPost.Tests/EasyPost.Tests.csproj -f {{fw}} -c Debug

# Update the examples submodule
update-examples-submodule:
    git submodule init
    git submodule update --remote

# Run the integration tests for a specific framework
# Always run integration tests in Release mode to check the end-user experience
integration-test fw=FW:
    dotnet test EasyPost.Integration/EasyPost.Integration.csproj -f {{fw}} -c Release -restore

# Run the F# compatibility tests for a specific framework
fs-compat-test fw=FW:
    dotnet test EasyPost.Compatibility.FSharp/EasyPost.Compatibility.FSharp.fsproj -f {{fw}} -restore
# Run the VB compatibility tests for a specific framework
vb-compat-test fw=FW:
    dotnet test EasyPost.Compatibility.VB/EasyPost.Compatibility.VB.vbproj -f {{fw}} -restore

# Run the Net Standard compatibility tests for a specific framework
netstandard-compat-test fw=FW:
    dotnet test EasyPost.Compatibility.NetStandard/EasyPost.Compatibility.NetStandard.csproj -f {{fw}} -restore
