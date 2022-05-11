# EasyPost .Net Client Library

[![CI](https://github.com/EasyPost/easypost-csharp/workflows/CI/badge.svg)](https://github.com/EasyPost/easypost-csharp/actions?query=workflow%3ACI)
[![NuGet version](https://badge.fury.io/nu/EasyPost-Official.svg)](https://badge.fury.io/nu/EasyPost-Official)

EasyPost, the simple shipping solution. You can sign up for an account at https://easypost.com.

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
            EasyPost.ClientManager.SetCurrent(Environment.GetEnvironmentVariable("EASYPOST_API_KEY"));

            Dictionary<string, object> fromAddress = new Dictionary<string, object>() {
                { "name", "Dr. Steve Brule" },
                { "street1", "417 Montgomery Street" },
                { "street2", "5th Floor" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94104" },
                { "phone", "4153334444" }
            };

            Dictionary<string, object> toAddress = new Dictionary<string, object>() {
                { "company", "EasyPost" },
                { "street1", "417 Montgomery Street" },
                { "street2", "Floor 5" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94104" },
                { "phone", "415-379-7678" }
            };

            Dictionary<string, object> parcel = new Dictionary<string, object>() {
                { "length", 8 },
                { "width", 6 },
                { "height", 5 },
                { "weight", 10 },
            }

            Shipment shipment = await Shipment.Create(new Dictionary<string, object>() {
                { "from_address", fromAddress },
                { "to_address", toAddress },
                { "parcel", parcel },
            });

            await shipment.Buy(shipment.LowestRate());

            Console.WriteLine(JsonConvert.SerializeObject(shipment, Formatting.Indented));
        }
    }
}
```

### Configuration

**Single API Key**

If you are operating with a single EasyPost API key, during the initialization of your application add the following to
configure EasyPost.

```cs
using EasyPost;

ClientManager.SetCurrent("ApiKey");
```

**Multiple API Keys**

If you are operating with multiple EasyPost API keys, or wish to delegate the construction of the client requests,
configure the `ClientManager` with a delegate at application initialization.

```cs
using EasyPost;

ClientManager.SetCurrent(() => new Client(new ClientConfiguration("yourApiKeyHere")));
```

### Warning about Threads

NOTE: The EasyPost .NET client library (in particular, the `ClientManager` global object) is not threadsafe; do not
attempt to perform requests from multiple threads in parallel. This can be particularly problematic if using multiple
API keys; make sure to always use a Mutex, Monitor, or other synchronization method to ensure that concurrent requests
do not enter the EasyPost library from different threads.

## Documentation

API Documentation can be found at: https://easypost.com/docs/api.

Upgrading major versions of this project? Refer to the [Upgrade Guide](UPGRADE_GUIDE.md).

## Development

It is highly recommended to use a purpose-built IDE when working with this project such as `Visual Studio`. Most actions
such as building, cleaning, and testing can be done via the GUI.

```bash
# Lint project
dotnet-format

# Run tests (recommended to instead run via an IDE like Visual Studio)
dotnet test

# Pull EasyVCR submodule
git submodule init
git submodule update --recursive
```

### Testing

The test suite in this project was specifically built to produce consistent results on every run, regardless of when
they run or who is running them. This project uses [EasyVCR](https://www.nuget.org/packages/EasyVCR/) to record and
replay HTTP requests and responses via "cassettes". When the suite is run, the HTTP requests and responses for each test
function will be saved to a cassette if they do not exist already and replayed from this saved file if they do, which
saves the need to make live API calls on every test run.

If you make an addition to this project, the request/response will get recorded automatically for you if
a `_vcr.SetUpTest("testName");` is included in the test function. When making changes to this project, you'll need to
re-record the associated cassette to force a new live API call for that test which will then record the request/response
used on the next run.

The test suite has been populated with various helpful fixtures that are available for use, each completely independent
from a particular user **with the exception of the USPS carrier account ID** which has a fallback value to our internal
testing user's ID. If you are a non-EasyPost employee and are re-recording cassettes, you may need to provide
the `USPS_CARRIER_ACCOUNT_ID` environment variable with the ID associated with your USPS account (which will be
associated with your API keys in use) for tests that use this fixture.

**Note on dates:** Some fixtures use hard-coded dates that may need to be incremented if cassettes get re-recorded (such
as reports or pickups).

#### Test Coverage

Unit test coverage reports can be generated by running the `generate_test_reports.sh` Bash script from the root of this
repository.

A report will be generated for each version of the library. Final reports will be stored in the `coveragereport` folder
in the root of the repository following generation.

The script requires the following tools installed in your PATH:

- `dotnet`
- [`reportgenerator`](https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-code-coverage?tabs=linux#generate-reports)
