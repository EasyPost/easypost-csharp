# EasyPost .Net Client Library

[![CI](https://github.com/EasyPost/easypost-csharp/workflows/CI/badge.svg)](https://github.com/EasyPost/easypost-csharp/actions?query=workflow%3ACI)
[![Coverage Status](https://coveralls.io/repos/github/EasyPost/easypost-csharp/badge.svg?branch=master)](https://coveralls.io/github/EasyPost/easypost-csharp?branch=master)
[![NuGet version](https://badge.fury.io/nu/EasyPost-Official.svg)](https://badge.fury.io/nu/EasyPost-Official)

EasyPost, the simple shipping solution. You can sign up for an account at <https://easypost.com>.

## Install

```powershell
Install-Package EasyPost-Official
```

See NuGet docs for additional instructions on installing via
the [dialog](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog) or
the [console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

## Usage

A simple create & buy shipment example:

```csharp
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost;
using Newtonsoft.Json;

namespace example
{
    class exampleClass
    {
        static async Task Main()
        {
            Client client = new Client(Environment.GetEnvironmentVariable("EASYPOST_API_KEY"));

            Shipment shipment = await client.Shipment.Create(new Dictionary<string, object>()
            {
                {
                    "to_address", new Dictionary<string, object>()
                    {
                        { "name", "Dr. Steve Brule" },
                        { "street1", "179 N Harbor Dr" },
                        { "city", "Redondo Beach" },
                        { "state", "CA" },
                        { "zip", "90277" },
                        { "country", "US" },
                        { "phone", "8573875756" },
                        { "email", "dr_steve_brule@gmail.com" }
                    }
                },
                {
                    "from_address", new Dictionary<string, object>()
                    {
                        { "name", "EasyPost" },
                        { "street1", "417 Montgomery Street" },
                        { "street2", "5th Floor" },
                        { "city", "San Francisco" },
                        { "state", "CA" },
                        { "zip", "94104" },
                        { "country", "US" },
                        { "phone", "4153334445" },
                        { "email", "support@easypost.com" }
                    }
                },
                {
                    "parcel", new Dictionary<string, object>()
                    {
                        { "length", 20.2 },
                        { "width", 10.9 },
                        { "height", 5 },
                        { "weight", 65.9 }
                    }
                }
            });

            await shipment.Buy(shipment.LowestRate());

            Console.WriteLine(JsonConvert.SerializeObject(shipment, Formatting.Indented));
        }
    }
}
```

### Configuration

A `Client` object is the entry point into the EasyPost API. It is instantiated with your API key:

```csharp
using EasyPost;

Client myClient = new Client("EASYPOST_API_KEY");
```

An API key is required for all requests. You can find your API key in
your [EasyPost dashboard](https://easypost.com/account/api-keys).

Once declared, a client's API key cannot be changed. If you are using multiple API keys, you can create multiple client
objects.

### Services

All general API services can be accessed through the `Client` object. For example, to access the `Address` service:

```csharp
AddressService addressService = myClient.Address;
```

Beta services can be accessed via the `myClient.Beta` property.

```csharp
ExampleService betaService = myClient.Beta.Example;
```

### Resources

API objects cannot be created locally. All local objects are copies of server-side data, retrieved via an API call from
a service.

For example, to create a new shipment, you must use the client's Shipment service:

```csharp
Shipment myShipment = await myClient.Shipment.Create(new Dictionary<string, object>
{
    { "from_address", fromAddress },
    { "to_address", toAddress },
    { "parcel", parcel }
});
```

Functions involving a specific resource are then enacted on that resource. For example, to buy the shipment:

```csharp
await myShipment.Buy(myShipment.LowestRate());
```

Any generated local resource will have stored internally the same client used to create or retrieve them. Any API call
made against the resource will automatically use the same client. This will prevent potential issues of accidentally
using the wrong API key when interacting with a resource in a multi-client environment.

## Documentation

API documentation can be found at: <https://easypost.com/docs/api>.

Library documentation can be found on the web at [Fuget](https://fuget.org/packages/EasyPost-Official).

Upgrading major versions of this project? Refer to the [Upgrade Guide](UPGRADE_GUIDE.md).

## Development

It is highly recommended to use a purpose-built IDE when working with this project such as `Visual Studio`. Most actions
such as building, cleaning, and testing can be done via the GUI.

```bash
# Build project
make build

# Lint project
make lint

# Format project
make format

# Run tests (recommended to instead run via an IDE like Visual Studio)
EASYPOST_TEST_API_KEY=123... EASYPOST_PROD_API_KEY=123... make test

# Generate coverage reports
EASYPOST_TEST_API_KEY=123... EASYPOST_PROD_API_KEY=123... make coverage

# Run security analysis
make scan
```

#### NuGet Dependencies

The NuGet package dependencies for this project are listed in the `.csproj` files. This project
uses [NuGet package locks](https://docs.microsoft.com/en-us/nuget/consume-packages/package-references-in-project-files#locking-dependencies)
to keep specific versions of dependencies. The lock files will be used during NuGet `restore`, if present.

If you need to update or alter NuGet dependencies, delete the `package.lock.json` files first. They will be regenerated
during the next `restore`.

### Testing

The test suite in this project was specifically built to produce consistent results on every run, regardless of when
they run or who is running them. This project uses [EasyVCR](https://www.nuget.org/packages/EasyVCR/) to record and
replay HTTP requests and responses via "cassettes". When the suite is run, the HTTP requests and responses for each test
function will be saved to a cassette if they do not exist already and replayed from this saved file if they do, which
saves the need to make live API calls on every test run.

**Sensitive Data:** We've made every attempt to include scrubbers for sensitive data when recording cassettes so that
PII or sensitive info does not persist in version control; however, please ensure when recording or re-recording
cassettes that prior to committing your changes, no PII or sensitive information gets persisted by inspecting the
cassette.

**Making Changes:** If you make an addition to this project, the request/response will get recorded automatically for
you if `UseVCR("testName");` is included on the test function. When making changes to this project, you'll
need to re-record the associated cassette to force a new live API call for that test which will then record the
request/response used on the next run.

**Test Data:** The test suite has been populated with various helpful fixtures that are available for use, each
completely independent from a particular user **with the exception of the USPS carrier account ID** (
see [Unit Test API Keys](#unit-test-api-keys) for more information) which has a fallback value of our internal testing
user's ID. Some fixtures use hard-coded dates that may need to be incremented if cassettes get re-recorded (such as
reports or pickups).

#### Unit Test API Keys

The following are required on every test run:

- `EASYPOST_TEST_API_KEY`
- `EASYPOST_PROD_API_KEY`

The following are required when you need to re-record cassettes for applicable tests (fallback values are used
otherwise):

- `USPS_CARRIER_ACCOUNT_ID` (eg: one-call buying a shipment for non-EasyPost employees)
- `REFERRAL_CUSTOMER_PROD_API_KEY` (eg: adding a credit card to a referral customer)

Some tests may require a user with a particular set of enabled features such as a `Partner` user when creating
referrals. We have attempted to call out these functions in their respective docstrings.

#### Test Coverage

Unit test coverage reports can be generated by running the `generate_test_reports.sh` Bash script from the root of this
repository.

A report will be generated for each version of the library. Final reports will be stored in the `coveragereport` folder
in the root of the repository following generation.

The script requires the following tools installed in your PATH:

- `dotnet`
- [`reportgenerator`](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=linux#generate-reports)
