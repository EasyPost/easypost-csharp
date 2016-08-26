# EasyPost .Net Client Library

EasyPost is a simple shipping API. You can sign up for an account at https://easypost.com

## Documentation

Up-to-date documentation at: https://www.easypost.com/docs/api/c-sharp

## Installation

The easiest way to add EasyPost to your project is with the NuGet package manager.

```
Install-Package EasyPost-Official
```

See NuGet docs for instructions on installing via the [dialog](http://docs.nuget.org/docs/start-here/managing-nuget-packages-using-the-dialog) or the [console](http://docs.nuget.org/docs/start-here/using-the-package-manager-console).

## Usage

The EasyPost API consists of many object types. There are several attributes that are consistent across all objects:

* `id` -- Guaranteed unique identifier of the object.
* `created_at`/`updated_at`  -- Timestamps of creation and last update time.

### Configuration

If you are operating with a single EasyPost API key, during the initialization of your application add the following to configure EasyPost.

```cs
using EasyPost;

ClientManager.SetCurrent("ApiKey");
```

If you are operating with multiple EasyPost API keys, or wish to delegate the construction of the client requests, configure the `ClientManager` with a delegate at application initialization.

```cs
using EasyPost;

ClientManager.SetCurrent(() => new Client(new ClientConfiguration("yourApiKeyHere")));
```

### [Address Verification](https://www.easypost.com/docs/api/c-sharp#create-and-verify-addresses)

An `Address` can be verified using one or many verifications [methods](https://www.easypost.com/docs/api/c-sharp#verifications-object). If `Address` is created without strict verifications the object will still be created, otherwise an `HttpException` will be raised.

```cs
using EasyPost;

Address address = new Address() {
    company = "Simpler Postage Inc",
    street1 = "164 Townsend Street",
    street2 = "Unit 1",
    city = "San Francisco",
    state = "CA",
    country = "US",
    zip = "94107",
    verify = new List<string>() { "delivery" }
};

Address address = address.Create();

if (address.verifications.delivery.success) {
    // successful verification
} else {
    // unsuccessful verification
}
```

```cs
using EasyPost;

Address address = new Address() {
    company = "Simpler Postage Inc",
    street1 = "164 Townsend Street",
    street2 = "Unit 1",
    city = "San Francisco",
    state = "CA",
    country = "US",
    zip = "94107",
    verify_strict = new List<string>() { "delivery" }
};

try {
    address.Create();
} except (HttpException) {
    // unsuccessful verification
}

// successful verification
```

### [Rating](https://www.easypost.com/docs/api/c-sharp#rates)

Rating is available through the `Shipment` object. Since we do not charge for rating there are rate limits for this action if you do not eventually purchase the `Shipment`. Please contact us at support@easypost.com if you have any questions.

```cs
Address fromAddress = new Address() { zip = "14534" };
Address toAddress = new Address() { zip = "94107" };

Parcel parcel = new Parcel() {
    length = 8,
    width = 6,
    height = 5,
    weight = 10
};

Shipment shipment = new Shipment() {
    from_address = fromAddress,
    to_address = toAddress,
    parcel = parcel
};

foreach (Rate rate in shipment.rates) {
    // process rates
}
```

### [Postage Label](https://www.easypost.com/docs/api/c-sharp#buy-a-shipment) Generation

Purchasing a shipment will generate a `PostageLabel` and any customs `Form`s that are needed for shipping.

```cs
Address fromAddress = new Address() { id = "adr_..." };
Address toAddress = new Address() {
    company = "EasyPost",
    street1 = "164 Townsend Street",
    street2 = "Unit 1",
    city = "San Francisco",
    state = "CA",
    country = "US",
    zip = "94107"
};

Parcel parcel = new Parcel() {
    length = 8,
    width = 6,
    height = 5,
    weight = 10
};

CustomsItem item = new CustomsItem() { description = "description" };
CustomsInfo info = new CustomsInfo() {
    customs_certify = "TRUE",
    eel_pfc = "NOEEI 30.37(a)",
    customs_items = new List<CustomsItem>() { item }
};

Options options = new Options() { label_format = "PDF" };

Shipment shipment = new Shipment() {
    from_address = fromAddress,
    to_address = toAddress,
    parcel = parcel,
    customs_info = info,
    optoins = options
};

shipment.Buy(shipment.LowestRate(
    includeServices: new List<Service>() { Service.Priority },
    excludeCarriers: new List<Carrier>() { Carrier.USPS }
));

shipment.postage_label.url; // https://easypost-files.s3-us-west-2.amazonaws.com/files/postage_label/20160826/8e77c397d47b4d088f1c684b7acd802a.png

foreach (Form form in shipment.forms) {
    // process forms
}
```

### Asynchronous Batch Processing

The `Batch` object allows you to perform operations on multiple `Shipment`s at once. This includes scheduling a `Pickup`, creating a `ScanForm` and consolidating labels. Operations performed on a `Batch` are asynchronous and take advantage of our [webhoook](https://www.easypost.com/docs/api/c-sharp#events) infrastructure.

```cs
using EasyPost;

Shipment shipment = new Shipment() {
    from_address = fromAddress,
    to_address = toAddress,
    parcel = parcel,
    optoins = options
};

Batch batch = Batch.CreateAndBuy(new Dictionary<string, object>() {
    { "reference", "MyReference" },
    { "shipments", new List<Dictionary<string, object>>() { shipment } }
});
```

This will produce two webhooks. One `batch.created` and one `batch.updated`. Process each `Batch` [state](https://www.easypost.com/docs/api/c-sharp#batch-object) according to your business logic.

```cs
using EasyPost;

Batch batch = Batch.Retrieve("batch_...");
batch.GenerateLabel("zpl"); // populates batch.label_url asynchronously
```

Consume the subsequent `batch.updated` webhook to process further.

### Reporting Issues

If you have an issue with the client feel free to open an issue on [GitHub](https://github.com/EasyPost/easypost-csharp/issues). If you have a general shipping question or a questions about EasyPost's service please contact support@easypost.com for additional assitance.
