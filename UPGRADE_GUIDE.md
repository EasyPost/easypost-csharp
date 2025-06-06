# Upgrade Guide

Use the following guide to assist in the upgrade process of the `easypost-csharp` library between major versions.

- [Upgrading from 6.x to 7.0](#upgrading-from-6x-to-70)
- [Upgrading from 5.x to 6.0](#upgrading-from-5x-to-60)
- [Upgrading from 4.x to 5.0](#upgrading-from-4x-to-50)
- [Upgrading from 3.x to 4.x](#upgrading-from-3x-to-40)
- [Upgrading from 2.x to 3.0](#upgrading-from-2x-to-30)

## Upgrading from 6.x to 7.0

### 7.0 High Impact Changes

- [.NET Support](#70-net-support)
- [Error Parsing](#70-error-parsing)

### 7.0 Medium Impact Changes

- [Deprecations](#70-deprecations)

## 7.0 .NET Support

*Likelihood of Impact: **High***

.NET Framework 4.7.2+ now required.

## 7.0 Error Parsing

*Likelihood of Impact: **High***

The `errors` key of an error response can return either a list of `FieldError` objects or a list of strings. The error parsing has been expanded to include both formats. As such, you will now need to check for the format of the `errors` field and handle the errors appropriately for the type that is returned.

The `Error` model has been removed since it is unused and we directly assign properties of an error response to the `ApiError` type.

The `PaymentRefund` now uses a list of `FieldError` instead of `Error` for the `errors` field.

## 7.0 Deprecations

The following parameters, functions, and classes have been removed:

- `EasyPost.Models.API.DeliveryDateForZipPairEstimate.EasyPostTimeInTransitData` property (use `EasyPost.Models.API.DeliveryDateForZipPairEstimate.TimeInTransitDetails` instead)
- `EasyPost.Models.API.Options.BillReceiverAccount` property (use `EasyPost.Models.API.Options.Payment` instead)
- `EasyPost.Models.API.Options.BillReceiverPostalCode` property (use `EasyPost.Models.API.Options.Payment` instead)
- `EasyPost.Models.API.Options.BillThirdPartyAccount` property (use `EasyPost.Models.API.Options.Payment` instead)
- `EasyPost.Models.API.Options.BillThirdPartyCountry` property (use `EasyPost.Models.API.Options.Payment` instead)
- `EasyPost.Models.API.Options.BillThirdPartyPostalCode` property (use `EasyPost.Models.API.Options.Payment` instead)
- `EasyPost.Models.API.Rate.EstDeliveryDays` property (use `EasyPost.Models.API.Rate.DeliveryDays` instead)
- `EasyPost.Models.API.SmartRate.EstDeliveryDays` property (use `EasyPost.Models.API.SmartRate.DeliveryDays` instead)
- `EasyPost.Models.API.RateWithEstimatedDeliveryDate.EasyPostTimeInTransitData` property (use `EasyPost.Models.API.RateWithEstimatedDeliveryDate.TimeInTransitDetails` instead)
- `EasyPost.Models.API.RecommendShipDateForShipmentResult.EasyPostTimeInTransitData` property (use `EasyPost.Models.API.RecommendShipDateForShipmentResult.TimeInTransitDetails` instead)
- `EasyPost.Models.API.ShipDateForZipPairRecommendation.EasyPostTimeInTransitData` property (use `EasyPost.Models.API.ShipDateForZipPairRecommendation.TimeInTransitDetails` instead)
- `EasyPost.Models.API.Tracker.TrackingUpdatedAt` property (use `EasyPost.Models.API.Tracker.UpdatedAt` instead)
- `EasyPost.Models.API.TimeInTransitDetails` class (use `EasyPost.Models.API.TimeInTransitDetailsForDeliveryDateEstimate` instead)
- `EasyPost.Parameters.Tracker.CreateList` class (related function was removed in v6.8.0)
- `EasyPost.Constants.CarrierAccounts.FedExAccount` variable (use `EasyPost.Models.API.CarrierAccountType.FedEx` instead)
- `EasyPost.Constants.CarrierAccounts.UpsAccount` variable (use `EasyPost.Models.API.CarrierAccountType.Ups` instead)

## Upgrading from 5.x to 6.0

**NOTICE:** v6 is deprecated.

### 6.0 High Impact Changes

- [.NET Support](#60-net-support)
- [Carbon Offset Removed](#60-carbon-offset-removed)

### 6.0 Low Impact Changes

- [CreateAndBuy Batch Function Removed](#60-createandbuy-batch-function-removed)

## 6.0 .NET Support

*Likelihood of Impact: **High***

**.NET Core 3.1 Support Dropped**

This library no longer supports .NET Core 3.1, which has reached end-of-life.

**.NET 8.0 Support Added**

This library now supports .NET 8.0.

## 6.0 Carbon Offset Removed

*Likelihood of Impact: **High***

EasyPost now offers Carbon Neutral shipments by default for free! Because of this, there is no longer a need to specify if you want to offset the carbon footprint of a shipment.

The `withCarbonOffset` parameter of the `Create`, `Buy`, and `RegenerateRates` shipment functions has been removed.

The `CarbonOffset` parameter of the `Shipment.Create`, `Shipment.Buy` and `Shipment.RegenerateRates` parameter sets has been removed.

This is a high-impact change for those using `EndShippers`, as the signature for the `Create` and `Buy` shipment function has changed. You will need to inspect these callsites to ensure that the EndShipper parameter is being passed in the correct place.

The `CarbonOffset` model has also been removed.

## 6.0 CreateAndBuy Batch Function Removed

*Likelihood of Impact: **Low***

The `create_and_buy` batch endpoint has been deprecated, and the `CreateAndBuy` Batch service function has been removed.

The correct procedure is to first create a batch and then purchase it with two separate API calls.

## Upgrading from 4.x to 5.0

**NOTICE:** v5 is deprecated.

### 5.0 High Impact Changes

- [Service Functions](#50-service-functions)
- [Client Configuration](#50-client-configuration)

### 5.0 Medium Impact Changes

- [Drop RestSharp Dependency](#50-drop-restsharp-dependency)
- [Naming Conventions](#50-naming-conventions)
- [Exception Changes](#50-exception-changes)
- [Parameter Sets](#50-parameter-sets)

## 5.0 Service Functions

*Likelihood of Impact: **High***

Previously, only `Create`, `Retrieve` and `All` functions were available in services, with all other functions related to a specific instance of a resource available on the resource itself.

For example, v4.x flow of creating a pickup and then buying it:

```csharp
var pickup = await myClient.Pickup.Create(parameters);
pickup.Buy();  // pickup variable is updated in-place
```

In v5.0, the flow is now:

```csharp
var pickup = await myClient.Pickup.Create(parameters);
purchasedPickup = await myClient.Pickup.Buy(pickup.Id); // need to capture the updated Pickup object
```

## 5.0 Client Configuration

*Likelihood of Impact: **High***

The process of configuring a `Client` has been overhauled to allow for more flexibility in the configuration process.

Old method:

```csharp
Client myClient = new Client("my_api_key");
```

New method:

```csharp
Client myClient = new Client(new ClientConfiguration("my_api_key"));
```

The `ClientConfiguration` class has a number of optional parameters that can be set to customize the behavior of the `Client` instance.

```csharp
Client myClient = new Client(new ClientConfiguration("my_api_key") {
  ApiBase = "optional_api_base_override",
  CustomHttpClient = myOptionalCustomHttpClient,
  Timeout = TimeSpan.FromMilliseconds(myCustomTimeoutMilliseconds)
});
```

## 5.0 Drop RestSharp Dependency

*Likelihood of Impact: **Medium***

The RestSharp dependency in this library has been dropped in favor of using the `System.Net.Http` and `Newtonsoft.Json` libraries directly.

This should have no impact on the end-user experience of using this library, but if you were reliant on the RestSharp dependency in this library being transitively used for another aspect of your codebase, you will need to now install RestSharp directly.

## 5.0 Naming Conventions

*Likelihood of Impact: **Medium***

The following classes and properties have been renamed or altered:

- `Smartrate` is now `SmartRate`

## 5.0 Exception Changes

*Likelihood of Impact: **Medium***

Some exception types have been consolidated or altered:

- `UnexpectedHttpError` is now `UnknownHttpError`
- `UnknownApiError` has been removed and replaced with `UnknownHttpError`
- `ExternalApiError` no longer inherits from `ApiError` and instead inherits from `EasyPostError`
  - Any EasyPost API failure will raise an `ApiError`-based exception
  - Non-EasyPost API/HTTP failures (e.g. calls to Stripe) will raise an `ExternalApiError` exception
- All `EasyPostError` exceptions now have a `PrettyPrint` getter method that returns a human-readable string representation of the error
- An EasyPost API timeout will raise a `TimeoutError` exception rather than a `System.Threading.Tasks.TaskCanceledException`

## 5.0 Parameter Sets

*Likelihood of Impact: **Medium***

The Parameter objects introduced in [`v4.5.0`](CHANGELOG.md#v450-2023-03-22) have been moved out of beta. As a result, the classes are available in a different namespace.

Old namespace:

```csharp
var parameters = new EasyPost.BetaFeatures.Parameters.Addresses.Create();
```

New namespace:

```csharp
var parameters = new EasyPost.Parameters.Address.Create();
```

Note that the namespaces have also changed from plural to singular (e.g. `Addresses` to `Address`, `Shipments` to `Shipment`) to better correlate with the service names on a `Client` instance.

## Upgrading from 3.x to 4.0

**NOTICE:** v4 is deprecated.

### 4.0 High Impact Changes

- [Updating Dependencies](#40-updating-dependencies)
- [Client Instance](#40-client-instance)
- [Language Conventions](#40-language-conventions)
- [Nullable Properties](#40-nullable-properties)
- [Error Types](#40-error-types)
- [Namespace Changes](#40-namespace-changes)

### 4.0 Low Impact Changes

- [Beta Feature Access](#40-beta-feature-access)
- [Services and Resources](#40-services-and-resources)

## 4.0 Updating Dependencies

*Likelihood of Impact: **High***

**Dependencies**

- RestSharp was upgraded from v107 to v108
- All dependencies had minor version bumps and have been locked to the latest (at the time) major version

## 4.0 Client Instance

*Likelihood of Impact: **High***

The library is now designed around the idea of a `Client`. Users will initialize a `Client` instance with their API key
and then use that instance to make API calls.

This change allows for multiple clients to be instantiated with different API keys and for the library to be used in a
multi-threaded environment.

```csharp
// Old
EasyPost.ClientManager.SetCurrent("EASYPOST_API_KEY"); // global API key

// New
var client1 = new EasyPost.Client("EASYPOST_API_KEY_1"); // per-client API key
var client2 = new EasyPost.Client("EASYPOST_API_KEY_2"); // per-client API key
```

All functions are now accessed via the `Client` instance. For example, to create a shipment:

```csharp
// Old
EasyPost.Shipment shipment = await EasyPost.Shipment.Create(parameters);

// New
EasyPost.Shipment shipment = await myClient.Shipment.Create(parameters);
```

## 4.0 Language Conventions

*Likelihood of Impact: **High***

All functions, classes and properties have been renamed to follow the C# language conventions. The most notable changes
here is that all properties of an object are now PascalCase rather than snake_case.

```csharp
// Old
var id = myAddress.id;
var toAddress = myShipment.to_address;
var formType = myScanForm.form_file_type;

// New
var id = myAddress.Id;
var toAddress = myShipment.ToAddress;
var formType = myScanForm.FormFileType;
```

## 4.0 Nullable Properties

*Likelihood of Impact: **High***

All properties of all objects are now nullable. This means that if a property is not returned by the API, it will be
either `null` or the default value for the given type (e.g. `false` for `bool`).

This change silences compilation warnings for the library.

The majority of properties for an object will likely be populated as a result of an API call, but if not, end-users will
know that the value of a non-populated property is guaranteed to be `null`.

End users may need to add additional null checks or nullability markers in their code, although this change will likely
play well with any existing null checks.

```csharp
// Old
string name = myAddress.name; // "name" is non-nullable, but not guaranteed to be populated
bool correctName = name.startsWith("John"); // compilation warning: "name" may be null

// New
string? name = myAddress.Name; // "Name" and "name" are nullable
bool correctName = name?.startsWith("John") ?? false; // no compilation warning
```

## 4.0 Error Types

*Likelihood of Impact: **High***

Error handling has been overhauled and a number of specific exception types have been introduced.

All exceptions inherit from `EasyPost.Exceptions.EasyPostError` (which extends `System.Exception`). Users can catch this exception type to handle all
errors thrown by the library.

Subclasses of `EasyPostError` are grouped into two categories:

- `EasyPost.Exceptions.ApiError` for errors returned by the API. Subclasses of this exception type are:
  - `EasyPost.Exceptions.ConnectionError` - thrown when the library is unable to connect to the API
  - `EasyPost.Exceptions.ExternalApiError` - thrown when an issue occurs with an external API (e.g. Stripe)
  - `EasyPost.Exceptions.GatewayTimeoutError` - thrown when the API gateway times out (504 status code)
  - `EasyPost.Exceptions.InternalServerError` - thrown when an internal server error occurs (500 status code)
  - `EasyPost.Exceptions.InvalidRequestError` - thrown when the API received an invalid request (422 status code)
  - `EasyPost.Exceptions.MethodNotAllowedError` - thrown when the API receives a request with an invalid HTTP method (
      405 status code)
  - `EasyPost.Exceptions.NotFoundError` - thrown when the API receives a request for a resource that does not exist (
      404 status code)
  - `EasyPost.Exceptions.PaymentError` - thrown when a payment error occurs (402 status code)
  - `EasyPost.Exceptions.ProxyError` - thrown when the library is unable to connect to the API via a proxy
  - `EasyPost.Exceptions.RateLimitError` - thrown when the API rate limit is exceeded (429 status code)
  - `EasyPost.Exceptions.RetryError` - thrown when an error occurs during a retry
  - `EasyPost.Exceptions.ServiceUnavailableError` - thrown when the API is unavailable (503 status code)
  - `EasyPost.Exceptions.SslError` - thrown when the library is unable to connect to the API via SSL
  - `EasyPost.Exceptions.TimeoutError` - thrown when the API request times out (408 status code)
  - `EasyPost.Exceptions.UnauthorizedError` - thrown when the API receives an unauthorized request (401 or 403 status
      code)
  - `EasyPost.Exceptions.UnexpectedHttpError` - thrown when an unknown HTTP error occurs (unexpected 5xx status code)
  - `EasyPost.Exceptions.UnknownApiError` - thrown when an unknown API error occurs (unexpected 4xx status code)
  - `EasyPost.Exceptions.VcrError` - thrown when an error occurs with the VCR (used for testing only)
- Generic exceptions
  - `EasyPost.Exceptions.FilteringError` - thrown when an error occurs during filtering (e.g. calculating lowest rate)
  - `EasyPost.Exceptions.InvalidObjectError` - thrown when an invalid object is being used
  - `EasyPost.Exceptions.JsonError`
    - `EasyPost.Exceptions.JsonDeserializationError` - thrown when an error occurs during JSON deserialization
    - `EasyPost.Exceptions.JsonSerializationError` - thrown when an error occurs during JSON serialization
    - `EasyPost.Exceptions.JsonNoDataError` - thrown when no data is provided for JSON deserialization
  - `EasyPost.Exceptions.MissingPropertyError` - thrown when a required property is missing (e.g. `Id` for most
      objects)
  - `EasyPost.Exceptions.SignatureVerificationError` - thrown when the signature for a webhook is invalid
  - `EasyPost.Exceptions.ValidationError`
    - `EasyPost.Exceptions.MissingParameterError` - thrown when a required parameter is missing
    - `EasyPost.Exceptions.InvalidParameterError` - thrown when an invalid parameter is passed to a function

Any exception thrown by the EasyPost library will be one of the above types.

Any `EasyPost.Exceptions.ApiError` exception will have the following properties:

- `int? StatusCode` - the HTTP status code returned by the API call (e.g. 404)
- `string? Code` - the error code returned by the API (e.g. "PARAMETER.INVALID")
- `List<EasyPost.Error>? Errors` - a list of errors returned by the API (e.g. "Invalid parameter: to_address")

Any `EasyPost.Exceptions.ApiError` exception can be pretty-printed using the `PrettyPrint` string property.

Users can better anticipate exception information through utilities in the `EasyPost.Exceptions.Constants` namespace.

- Check what exception type will be thrown for a given HTTP status code by
  using `EasyPost.Exceptions.Constants.GetEasyPostExceptionType(HttpStatusCode statusCode)`
- Check error message strings/templates in `EasyPost.Exceptions.Constants.ErrorMessages`

## 4.0 Namespace Changes

*Likelihood of Impact: **High***

As a result of organizing the source code of this library, the namespace of several classes has changed.

- Most EasyPost-related objects are now in the `EasyPost.Models.API` namespace
  - e.g. `EasyPost.Address` is now `EasyPost.Models.API.Address`
- Exceptions, as noted in [Error Types](#40-error-types), are now in the `EasyPost.Exceptions` namespace
- Services (static functions) are now in the `EasyPost.Services` namespace (see [Services and Resources](#40-services-and-resources))
  - This should be opaque to end users accessing services through the `EasyPost.Client` class.

## 4.0 Beta Feature Access

*Likelihood of Impact: **Low***

Following the [client instance redesign](#40-client-instance), beta features are now accessed via the `Beta` property of
the `Client` instance.

This "beta client" can only be used to access beta features. Using a beta client to access non-beta features may result
in unexpected behavior.

```csharp
// Old
var betaObject = await EasyPost.Beta.SomeService.SomeFunction(parameters);

// New
var betaObject = await myClient.Beta.SomeService.SomeFunction(parameters);
```

Any beta function will use the same client (and associated API key) from which the beta function was accessed (
i.e. `myClient` in the example above).

## 4.0 Services and Resources

*Likelihood of Impact: **Low***

Static and instance-based methods have been divided into separate services and resources. For example, the static
method `Shipment.Create` is now accessible in the Shipment service via `myClient.Shipment.Create`. The instance
method `myShipment.Buy` is still accessible only via a Shipment instance.

Instances of an object cannot be initialized directly. Instead, use the service to create the instance via a `Create`
, `Retrieve`, or `All` API call.

Outside of the [new client instance change](#40-client-instance) and the [namespace change](#40-namespace-changes), this design change should not have any additional
significant impact on usage, but it is worth noting the new organizational structure.

## Upgrading from 2.x to 3.0

**NOTICE:** v3 is deprecated.

[v3 Docs](https://github.com/EasyPost/examples/tree/master/official/docs/csharp/v3)

### 3.0 High Impact Changes

- [Updating Dependencies](#30-updating-dependencies)
- [Project is Now Asynchronous](#30-project-is-now-asynchronous)
- [Address Verification Parameter Changes](#30-address-verification-parameter-changes)
- [Non-Static `Create()` Methods Removed](#30-removes-non-static-create-functions)
- [Renames `List()` Functions to `All()`](#30-renames-list-functions-to-all)
- [Clarify XList vs XCollection Distinctions](#30-clarify-xlist-vs-xcollection-distinctions)

### 3.0 Medium Impact Changes

- [`Destroy` Functions Renamed `Delete`](#30-destroy-functions-renamed-delete)
- [Removes `shipment.GetRates()` Shipment Method](#30-removal-of-shipmentgetrates-shipment-method)

### 3.0 Low Impact Changes

- [Removes the Unusable Rating Class](#30-removes-the-unusable-rating-class)

## 3.0 Updating Dependencies

*Likelihood of Impact: **High***

**Explicit .NET Framework Support Removed**

easypost-csharp no longer explicitly supports .NET Framework.

.NET Standard 2.0 is compatible with .NET Framework 4.6.1+.
See [this article](https://docs.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0) for more
information.

**.NET Standard 2.0 & NET 5.0 & 6.0 Support Added**

easypost-csharp can now be used with .NET Standard 2.0, NET 5.0 & NET 6.0 along with Visual Basic and F#.

**Dependencies**

- RestSharp was upgraded from v106 to v107
- All dependencies had minor version bumps

## 3.0 Project is Now Asynchronous

*Likelihood of Impact: **High***

Most functions are now asynchronous which requires prepending `await` to any function call and making the
functions `async`. This change dramatically improves API response times and prevents deadlocks. Aside from the need to
await for your async calls, the interfaces of methods remain the same (except for a couple notable changes listed below)
. A simple example of how to call an EasyPost function may look like:

```csharp
// `async` now required
static async Task Main()
{
    // Must call `await` on EasyPost function calls
    Shipment shipment = await Shipment.Create(new Dictionary<string, object>() {
        { "from_address", fromAddress },
        { "to_address", toAddress },
        { "parcel", parcel },
    });

    // Must call `await` on EasyPost function calls
    await shipment.Buy(shipment.LowestRate());
}
```

This change may require refactoring the parent methods to also be asynchronous.

## 3.0 Address Verification Parameter Changes

*Likelihood of Impact: **High***

The parameters to verify an address have been changed from `verification` to `verify` and `strict_verification`
to `verify_strict` to match our other libraries and documentation. The new parameter names must be used to verify an
address.

```csharp
// Old
addressData.Add("verifications_strict", new List<bool> { true });

// New
addressData.Add("verify_strict", new List<bool> { true });
```

## 3.0 Removes Non-Static `Create()` Functions

*Likelihood of Impact: **High***

In order to make this library [Visual Basic-compatible](https://github.com/EasyPost/easypost-csharp/pull/230),
non-static `Create()` functions are no longer supported.

This affects the `Address`, `Order`, `Pickup` and `Shipment` classes.

Previously, users could create, i.e., an `Address` object (`myAddress`) locally, set its attributes, and then
call `myAddress.Create()` to send the address data to EasyPost's API.

Now, users must call `Address.Create()` to create an `Address` object, passing in the address attributes as a
dictionary. This will send the data to EasyPost's API and return a local `Address` object.

```csharp
// Old
Address myAddress = new Address();
myAddress.company = "EasyPost";
await myAddress.Create();

// New
Address myAddress = await Address.Create(new Dictionary<string, object>() {
    { "company", "EasyPost" },
});
```

## 3.0 Renames `List()` Functions to `All()`

*Likelihood of Impact: **High***

Functions that called the `/all` method have been renamed from `List()` to `All()` to make function naming uniform (
previously there were a mix of both naming conventions) which also brings these calls inline with our documentation. If
you use any `List` functions in your project, you will need to update these.

```csharp
// Old
ShipmentList shipments = Shipment.List()

// New
ShipmentCollection shipments = Shipment.All()
```

## 3.0 Clarify XList vs XCollection Distinctions

*Likelihood of Impact: **High***

We unified the names of all "collection" type objects from XList to XCollection (eg: ShipmentList -> ShipmentCollection)
. If you are using these objects, you will need to correct their names.

```csharp
// Old
ShipmentList shipments = Shipment.List()

// New
ShipmentCollection shipments = Shipment.All()
```

## 3.0 `Destroy` Functions Renamed `Delete`

*Likelihood of Impact: **Medium***

Functions that were previously named `Destroy` are now named `Delete`. This change was made make this library consistent
with our other libraries and documentation while also clarifying what the action is doing. You will need to correct
these names in your code when performing actions such as deleting Webhooks, Users, and Carrier Accounts.

```csharp
// Old
webhook.Destroy();

// New
webhook.Delete();
```

## 3.0 Removal of `shipment.GetRates()` Shipment Method

*Likelihood of Impact: **Medium***

The HTTP method used for the `get_rates` endpoint at the API level has changed from `POST` to `GET` and will only
retrieve rates for a shipment instead of regenerating them. A new `/rerate` endpoint has been introduced to replace this
functionality; In this library, you can now call the `shipment.RegenerateRates` method to regenerate rates. Due to the
logic change, the `GetRates` method has been removed since a Shipment inherently already has rates associated.

```csharp
// Old
shipment.GetRates()

// New
shipment.RegenerateRates()
```

## 3.0 Removes the Unusable Rating Class

*Likelihood of Impact: **Low***

The `Rating` class is unusable as you cannot "create" a rate and has been removed. Rates must be retrieved from a
Shipment after creation.
