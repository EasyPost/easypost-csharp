# EasyPost .Net Client Library

EasyPost is a simple shipping API. You can sign up for an account at https://easypost.com

## Installation

The easiest way to add EasyPost to your project is with the NuGet package manager.

```Install-Package EasyPost-Official```

See NuGet docs for instructions on installing via the [dialog](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog) or the [console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

## Usage

During the initialization of your application add the following to configure EasyPost.

```cs
EasyPost.Client.apiKey = "apiKey";
```

### Purchasing a label for a Shipment

```cs
using EasyPost;

Dictionary<string, object> fromAddress = new Dictionary<string, object>() {
    {"name", "Andrew Tribone"}, {"street1", "480 Fell St"}, {"street2", "#3"},
    {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94102"}
};
Dictionary<string, object> toAddress = new Dictionary<string, object>() {
    {"company", "Simpler Postage Inc"}, {"street1", "164 Townsend Street"}, {"street2", "Unit 1"},
    {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94107"}
};

Shipment shipment = Shipment.Create(new Dictionary<string, object>() {
    {"carrier", "USPS"}, {"service", "Priority"},
    {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
    {"to_address", toAddress}, {"from_address", fromAddress}, {"reference", "ShipmentRef"}
});

shipment.Buy(shipment.LowestRate(includeServices: new List<string>() {"Priority"},
                                 excludeCarriers: new List<string>() {"FedEx"}));

if (shipment.status == "purchased") shipment.GenerateLabel("pdf"); // Populates `shipment.postage_label`
```

## Documentation

Up-to-date documentation at: https://www.geteasypost.com/docs
