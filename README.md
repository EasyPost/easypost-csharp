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
using EasyPost.Parameters;

namespace example
{
    class exampleClass
    {
        static async Task Main()
        {
            Client client = new Client(new ClientConfiguration(Environment.GetEnvironmentVariable("EASYPOST_API_KEY")));

            Parameters.Shipment.Create createParameters = new() {
                ToAddress = new Parameters.Address.Create {
                    Name = "Dr. Steve Brule",
                    Street1 = "179 N Harbor Dr",
                    City = "Redondo Beach",
                    State = "CA",
                    Zip = "90277",
                    Country = "US",
                    Phone = "8573875756",
                    Email = "dr_steve_brule@gmail.com"
                },
                FromAddress = new Parameters.Address.Create {
                    Name = "EasyPost",
                    Street1 = "417 Montgomery Street",
                    Street2 = "5th Floor",
                    City = "San Francisco",
                    State = "CA",
                    Zip = "94104",
                    Country = "US",
                    Phone = "4153334445",
                    Email = "support@easypost.com"
                },
                Parcel = new Parameters.Parcel.Create {
                    Length = 20.2,
                    Width = 10.9,
                    Height = 5,
                    Weight = 65.9
                }
            }

            Shipment shipment = await client.Shipment.Create(parameters);

            Rate rate = shipment.LowestRate();

            Paramaters.Shipment.Buy buyParameters = new(rate);

            Shipment purchasedShipment = await client.Shipment.Buy(shipment.Id, buyParameters);

            Console.WriteLine(JsonConvert.SerializeObject(purchasedShipment, Formatting.Indented));
        }
    }
}
```

### Configuration

A `Client` object is the entry point into the EasyPost API. It is instantiated with a `ClientConfiguration` with your API key:

```csharp
using EasyPost;

Client myClient = new Client(new ClientConfiguration("EASYPOST_API_KEY"));
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

All API-calling functions are made from the appropriate service object (rather than against the resource object), by providing the ID of the related resource. For example, to buy a shipment:

```csharp
Shipment myPurchasedShipment = await myClient.Shipment.Buy(myShipment.Id, myShipment.LowestRate());
```

### Parameters

Most functions in this library accept a `Dictionary<string, object>` as their sole parameter, which is ultimately used as the body of the HTTP request against EasyPost's API. If you instead would like to use .NET objects to construct API call parameters, you can use the various `Parameters` classes (currently in beta).

For example, to create an address:

```csharp
// Use an object constructor to create the address creation parameters
var addressCreateParameters = new EasyPost.BetaFeatures.Parameters.Addresses.Create {
    Name = "My Name",
    Street1 = "123 Main St",
    City = "San Francisco",
    State = "CA",
    Zip = "94105",
    Country = "US",
    Phone = "415-123-4567"
};

// You can add additional parameters as needed outside of the constructor
addressCreateParameters.Company = "My Company";

// Then convert the object to a dictionary
// This step will validate the data and throw an exception if there are any errors (i.e. missing required parameters)
var addressCreateDictionary = addressCreateParameters.ToDictionary();

// Pass the dictionary into the address creation method as normal
var address = await myClient.Address.Create(addressCreateDictionary);
```

Using the `Parameters` classes is not required, but they can help in a number of ways:

- Naturally enforces parameter types (can't assign a string to an int parameter, for example)
- Removes the need to remember parameter names (i.e. "name" vs "company")
- Prevents typos in parameter names
- Removes the need to know the exact JSON schema of the HTTP request body (parameters will be serialized into the proper schema behind-the-scenes)
- Validates parameters (i.e. ensure required parameters are present)
- Allows for IDE auto-completion
- Allows for IDE parameter documentation
- Provides a more natural way to construct parameters
- Facilitates ASP.NET Core model binding (bind an HTML form to a `Parameters` instance)

### HTTP Hooks

Users can audit the HTTP requests and responses being made by the library by setting the `Hooks` property of a `ClientConfiguration` with a set of event handlers. Available handlers include:

- `OnRequestExecuting` - Called before an HTTP request is made. An `OnRequestExecutingEventArgs` object is passed to the
  handler, which contains details about the `HttpRequestMessage` that will be sent to the server.
  - The `HttpRequestMessage` at this point is configured with all expected data (headers, body, etc.). Modifying
    any data in the callback will NOT affect the actual request that is sent to the server.
- `OnRequestResponseReceived` - Called after an HTTP request is made. An `RequestResponseReceivedEventArgs` object is
  passed to the handler, which contains details about the `HttpResponseMessage` that was received from the server.

Users can interact with these details in their callbacks as they see fit (e.g. logging).

```csharp
void OnRequestExecutingHandler(object? sender, OnRequestExecutingEventArgs args) {
    // Interact with details about the HttpRequestMessage here via args
    System.Console.WriteLine($"Making HTTP call to {args.RequestUri}");
}

void OnRequestResponseReceivedHandler(object? sender, OnRequestResponseReceivedEventArgs args) {
    // Interact with details about the HttpResponseMessage here via args
    System.Console.WriteLine($"Received HTTP response with status code {args.ResponseStatusCode}");
}

Client client = new Client(new ClientConfiguration("EASYPOST_API_KEY")
{
    Hooks = new Hooks {
        OnRequestExecuting = OnRequestExecutingHandler,
        OnRequestResponseReceived = OnRequestResponseReceivedHandler,
    },
});
```

Users
can [subscribe to or unsubscribe from callbacks](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/events/how-to-subscribe-to-and-unsubscribe-from-events)
at any time via the `Hooks` property of a client.

```csharp
// Add a new callback
client.Hooks.OnRequestExecuting += (sender, args) => { /* ... */ };

// Remove a callback
client.Hooks.OnRequestExecuting -= OnRequestExecutingHandler;
```

## Documentation

API documentation can be found at: <https://easypost.com/docs/api>.

Library documentation can be found on the web at: <https://easypost.github.io/easypost-csharp> or locally on
the [`gh-pages` branch](https://github.com/EasyPost/easypost-csharp/tree/gh-pages).

Upgrading major versions of this project? Refer to the [Upgrade Guide](UPGRADE_GUIDE.md).

## Support

New features and bug fixes are released on the latest major release of this library. If you are on an older major release of this library, we recommend upgrading to the most recent release to take advantage of new features, bug fixes, and security patches. Older versions of this library will continue to work and be available as long as the API version they are tied to remains active; however, they will not receive updates and are considered EOL.

For additional support, see our [org-wide support policy](https://github.com/EasyPost/.github/blob/main/SUPPORT.md).

## Development

It is highly recommended to use a purpose-built IDE when working with this project such as `Visual Studio`. Most actions
such as building, cleaning, and testing can be done via the GUI.

```bash
# Build project
make build

# Lint project
make lint
make lint-fix

# Run tests (recommended to instead run via an IDE like Visual Studio)
EASYPOST_TEST_API_KEY=123... EASYPOST_PROD_API_KEY=123... make test
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

**NOTE** .NET Framework/.NET Standard unit tests cannot currently be run on Apple Silicon (M1, M2, etc.). Instead, run
unit tests in one framework at a time with, e.g `make unit-test fw=net8.0`. Valid frameworks:

- `net462` (.NET Framework 4.6.2, the oldest non-EOL version of .NET Framework; will not run on Apple Silicon)
- `net5.0` (.NET 5.0)
- `net6.0` (.NET 6.0)
- `net7.0` (.NET 7.0)
- `net8.0` (.NET 8.0)

#### Test Coverage

Unit test coverage reports can be generated by running the `generate_test_reports.sh` Bash script from the root of this
repository.

A report will be generated for each version of the library. Final reports will be stored in the `coveragereport` folder
in the root of the repository following generation.

The script requires the following tools installed in your PATH:

- `dotnet`
- [`reportgenerator`](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=linux#generate-reports)
