# EasyPost's .NET client library

The [EasyPost .NET client library](https://github.com/EasyPost/easypost-csharp) provides convenient access to the EasyPost API from .NET applications written in the .NET language.

## Installation

Install the library with [NuGet](https://www.nuget.org/packages/EasyPost-Official):

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
