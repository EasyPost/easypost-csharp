# EasyPost .Net Client Library

EasyPost is a simple shipping API. You can sign up for an account at https://easypost.com

## Installation

The easiest way to add EasyPost to your project is with the NuGet package manager.

```Install-Package EasyPost-Official```

See NuGet docs for instructions on installing via the [dialog](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog) or the [console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

## Usage

See the [docs](https://www.easypost.com/docs/api#addresses) for more information.

During the initialization of your application add the following to configure EasyPost.

```cs
EasyPost.Client.apiKey = "apiKey";
```

### Address Verification

```cs
using EasyPost;

Address address = Address.CreateAndVerify(new Dictionary<string, object>() {
    {"company", "Simpler Postage Inc"}, {"street1", "164 Townsend Street"}, {"street2", "Unit 1"},
    {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94107"}
});
```

OR

```cs
using EasyPost;

Address address = new Address() {
    company = "Simpler Postage Inc", street1 = "164 Townsend Street", street2 = "Unit 1",
    city = "San Francisco", state = "CA", country = "US", zip = "94107"
};

address.Verify();
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
    {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
    {"to_address", toAddress}, {"from_address", fromAddress}, {"reference", "ShipmentRef"}
});

shipment.Buy(shipment.LowestRate(includeServices: new List<Service>() {Service.Priority},
                                 excludeCarriers: new List<Carrier>() {Carrier.FedEx}));

 shipment.GenerateLabel("pdf"); // Populates `shipment.postage_label.label_pdf_url`
```

OR

```cs
using EasyPost;

Address fromAddress = new Address() {
	name = "Andrew Tribone", street1 = "480 Fell St", street2 = "#3",
    city = "San Francisco", state = "CA", country = "US", zip = "94102"
};
Address toAddress = new Address() {
    company = "Simpler Postage Inc", street1 = "164 Townsend Street", street2 = "Unit 1",
    city = "San Francisco", state = "CA", country = "US", zip = "94107"
};
Parcel parcel = new Parcel() {length = 8, width = 6, height = 5, weight = 10};

Shipment shipment = new Shipment() {to_address = toAddress, from_address = fromAddress, parcel = parcel};
shipment.Buy(shipment.LowestRate(includeServices: new List<string>() {"Priority"},
                                 excludeCarriers: new List<string>() {"FedEx"}));

shipment.GenerateLabel("pdf"); // If there are no errors you will recieve postage_label, tracking_code and the selected_rate
```

### Asynchronous Batch Processing

Batches produce webhooks as its state changes. These hooks can be consumed by your application.

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
Dictionary<string, object> shipment = new Dictionary<string, object>() {
	{"carrier", "USPS"}, {"service", "Priority"} // Unlike creating a Shipment, these are used to purchase shipments within a batch.
    {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
    {"to_address", toAddress}, {"from_address", fromAddress}
};

Batch batch = Batch.CreateAndBuy(new Dictionary<string, object>() {
    {"reference", "MyReference"},
    {"shipments", new List<Dictionary<string, object>>() {shipment}}
});
```

If there are no errors you will recieve a webhook with a batch state of `purchased`.

```cs
using EasyPost;

Batch batch = Batch.Retrieve(id);
batch.GenerateLabel("zpl"); // Populate batch.label_url asynchronously. Consume the `label_generated` webhook to process further.
```

## Documentation

Up-to-date documentation at: https://www.geteasypost.com/docs
