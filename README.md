# EasyPost .Net Client Library

EasyPost is a simple shipping API. You can sign up for an account at https://easypost.com

## Documentation

Up-to-date documentation at: https://www.easypost.com/docs/api/csharp

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

### [Address Verification](https://www.easypost.com/docs/api/csharp#create-and-verify-addresses)

An `Address` can be verified using one or many verifications [methods](https://www.easypost.com/docs/api/csharp#verifications-object). If `Address` is created without strict verifications the object will still be created, otherwise an `HttpException` will be raised.

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

address.Create();

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

### [Rating](https://www.easypost.com/docs/api/csharp#rates)

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

shipment.Create();

foreach (Rate rate in shipment.rates) {
    // process rates
}
```

### [Postage Label](https://www.easypost.com/docs/api/csharp#buy-a-shipment) Generation

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
    options = options
};

shipment.Buy(shipment.LowestRate(
    includeServices: new List<string>() { "Priority" },
    excludeCarriers: new List<string>() { "USPS" }
));

shipment.postage_label.url; // https://easypost-files.s3-us-west-2.amazonaws.com/files/postage_label/20160826/8e77c397d47b4d088f1c684b7acd802a.png

foreach (Form form in shipment.forms) {
    // process forms
}
```

### Warning about Threads

NOTE: The EasyPost .NET client library (in particular, the `ClientManager` global object) is not threadsafe; do not attempt to perform requests from multiple threads in parallel. This can be particularly problematic if using multiple API keys; make sure to always use a Mutex, Monitor, or other synchronization method to ensure that concurrent requests do not enter the EasyPost library from different threads.

### Releasing

1. Update the [CHANGELOG](CHANGELOG.md).
1. Bump `version` in `EasyPost.nuspec` and `AssemblyVersion` in all `csproj` files.
1. Rebuild the library to update `dll`s in `lib` directory.
1. Create a git tag named the version number, e.g. `2.1.2.1`, and push it.
1. Publish new version on Nuget.

### Reporting Issues

If you have an issue with the client feel free to open an issue on [GitHub](https://github.com/EasyPost/easypost-csharp/issues). If you have a general shipping question or a questions about EasyPost's service please contact support@easypost.com for additional assitance.
