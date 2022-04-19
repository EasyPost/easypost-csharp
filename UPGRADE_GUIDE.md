# Upgrade Guide

Use the following guide to assist in the upgrade process of the `easypost-csharp` library between major versions.

-   [Upgrading from 2.x to 3.0](#upgrading-from-2x-to-30)

## Upgrading from 2.x to 3.0

### 3.0 High Impact Changes

- [Updating Dependencies](#30-updating-dependencies)
- [Project is Now Asynchronous](#30-project-is-now-asynchronous)
- [Address Verification Parameter Changes](#30-address-verification-parameter-changes)
- [Non-Static `Create()` Methods Removed](#30-removes-non-static-create-functions)
- [Renames `List()` Functions to `All()`](#30-renames-list-functions-to-all)
- [Clarify XList vs XCollection Distinctions](#30-clarify-xlist-vs-xcollection-distinctions)

### 3.0 Medium Impact Changes

-   [`Destroy` Functions Renamed `Delete`](#30-destroy-functions-renamed-delete)
-   [Removes `shipment.GetRates()` Shipment Method](#30-removal-of-shipmentgetrates-shipment-method)

### 3.0 Low Impact Changes

-   [Removes the Unusable Rating Class](#30-removes-the-unusable-rating-class)

## 3.0 Updating Dependencies

Likelihood of Impact: High

**Explicit .NET Framework Support Removed**

easypost-csharp no longer explicitly supports .NET Framework.

.NET Standard 2.0 is compatible with .NET Framework 4.6.1+. See [this article](https://docs.microsoft.com/en-us/dotnet/standard/net-standard?tabs=net-standard-2-0) for more information.

**.NET Standard 2.0 & NET 5.0 & 6.0 Support Added**

easypost-chsarp can now be used with .NET Standard 2.0, NET 5.0 & NET 6.0 along with Visual Basic and F#.

**Dependencies**

-   RestSharp was upgraded from v106 to v107
-   All dependencies had minor version bumps

## 3.0 Project is Now Asynchronous

Likelihood of Impact: High

Most functions are now asynchronous which requires prepending `await` to any function call and making the functions `async`. This change dramatically improves API response times and prevents deadlocks. Aside from the need to await for your async calls, the interfaces of methods remain the same (except for a couple notable changes listed below). A simple example of how to call an EasyPost function may look like:

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

Likelihood of Impact: High

The parameters to verify an address have been changed from `verification` to `verify` and `strict_verification` to `verify_strict` to match our other libraries and documentation. The new parameter names must be used to verify an address.

```csharp
// Old
addressData.Add("verifications_strict", new List<bool> { true });

// New
addressData.Add("verify_strict", new List<bool> { true });
```

## 3.0 Removes Non-Static `Create()` Functions

Likelihood of Impact: High

In order to make this library [Visual Basic-compatible](https://github.com/EasyPost/easypost-csharp/pull/230), non-static `Create()` functions are no longer supported.

This affects the `Address`, `Order`, `Pickup` and `Shipment` classes.

Previously, users could create, i.e., an `Address` object (`myAddress`) locally, set its attributes, and then call `myAddress.Create()` to send the address data to EasyPost's API.

Now, users must call `Address.Create()` to create an `Address` object, passing in the address attributes as a dictionary. This will send the data to EasyPost's API and return a local `Address` object.

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

Likelihood of Impact: High

Functions that called the `/all` method have been renamed from `List()` to `All()` to make function naming uniform (previously there were a mix of both naming conventions) which also brings these calls inline with our documentation. If you use any `List` functions in your project, you will need to update these.

```csharp
// Old
ShipmentList shipments = Shipment.List()

// New
ShipmentCollection shipments = Shipment.All()
```

## 3.0 Clarify XList vs XCollection Distinctions

Likelihood of Impact: High

We unified the names of all "collection" type objects from XList to XCollection (eg: ShipmentList -> ShipmentCollection). If you are using these objects, you will need to correct their names.

```csharp
// Old
ShipmentList shipments = Shipment.List()

// New
ShipmentCollection shipments = Shipment.All()
```

## 3.0 `Destroy` Functions Renamed `Delete`

Likelihood of Impact: Medium

Functions that were previously named `Destroy` are now named `Delete`. This change was made make this library consistent with our other libraries and documentation while also clarifying what the action is doing. You will need to correct these names in your code when performing actions such as deleting Webhooks, Users, and Carrier Accounts.

```csharp
// Old
webhook.Destroy();

// New
webhook.Delete();
```

## 3.0 Removal of `shipment.GetRates()` Shipment Method

Likelihood of Impact: Medium

The HTTP method used for the `get_rates` endpoint at the API level has changed from `POST` to `GET` and will only retrieve rates for a shipment instead of regenerating them. A new `/rerate` endpoint has been introduced to replace this functionality; In this library, you can now call the `shipment.RegenerateRates` method to regenerate rates. Due to the logic change, the `GetRates` method has been removed since a Shipment inherently already has rates associated.

```csharp
// Old
shipment.GetRates()

// New
shipment.RegenerateRates()
```

## 3.0 Removes the Unusable Rating Class

Likelihood of Impact: Low

The `Rating` class is unusable as you cannot "create" a rate and has been removed. Rates must be retrieved from a Shipment after creation.
