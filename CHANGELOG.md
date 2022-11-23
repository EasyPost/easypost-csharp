# CHANGELOG

## Next Release

- [ADDED] Support for creating/registering carrier accounts that require custom workflows (e.g. FedEx, UPS)
- [CHANGED] `Constants` are now stored in `EasyPost.Constants` instead of `EasyPost.Exceptions.Constants`

## v4.0.2 (2022-11-01)

- Fix bug where the temporary internal API key switch when adding a credit card to a referral user was not reverted after the request.
  - After adding a credit card to a referral user, the existing Client would be misconfigured for following requests.

## v4.0.1 (2022-10-24)

- `myInsurance.Refresh()` function HTTP method fixed from `PATCH` to `GET`
  - This function has been marked as obsolete and will be removed in a future release
- Fix return type of `order.Buy()` when passing in a rate. Function will now return the updated order.
- Fix bug where request time limits were not being copied to a cloned Client.
- Fix bug where hashcode of any `EasyPostObject` or subtype was not consistent.
  - Hashcode and equality now consider properties of the object, including Client. Different properties and/or different Clients will result in different hashcodes and objects will not be considered equal.
- Fix bug where the wrong `SmartrateAccuracy` would be chosen
  - `Percentile75` might accidentally have been chosen rather than `Percentile85` due to a bug in the `SmartrateAccuracy` enum ID.
- Fix bug where some embedded elements (e.g. customs items) were not being created if included during a larger creation request (e.g. customs info create).
- Prevent users from attempting to buy a shipment with a `null` rate, avoiding a `NullReferenceException`.

## v4.0.0 (2022-10-12)

The `v4.0.0` release includes all the changes from the release candidate (see `v4.0.0-rc1` below) as well as the following items:

- Improved API error parsing
  - API error message may be an array rather than a string. Arrays will be concatenated (by comma) and returned as a string.
- Capture 1xx and 3xx HTTP status codes as errors
  - Any known 3xx status code from the EasyPost API will throw a `RedirectError` exception
  - Any unknown 3xx status code will throw a `UnexpectedHttpError` exception
  - Any 1xx status code (known or unknown) will throw a `UnexpectedHttpError` exception

## v4.0.0-rc1 (2022-09-26)

### Breaking Changes & New Features

- Library is now thread-safe
  - Initialize a `Client` object with an API key
  - Static methods (i.e. `create`, `retrieve`, retrieve `all` of a resource) exist in services, accessed via property of the client (e.g. `myClient.Address.Create()`)
  - Instance methods (i.e. `update`, `delete`) accessed on instance of a resource (i.e. `myShipment.Update()`)
- All properties are now title-cased rather than snake-cased to match standard .NET naming conventions
  - e.g. `myShipment.id` is now `myShipment.Id`, `myAddress.federal_tax_id` is now `myAddress.FederalTaxId`, `myTrackerCollection.has_more` is now `myTrackerCollection.HasMore`
  - Some properties have been renamed to avoid naming conflicts:
    - `Rate.rate` is now `Rate.Price`
    - `Message.message` is now `Message.Text`
- All properties are now nullable
  - Almost all properties will be assigned a value during JSON deserialization. This is mostly to address compiler warnings
  - Users can proceed with the assumption that any given property will not be null
- Consistent exception handling
  - All exceptions inherit from `EasyPostError`
  - API-related and HTTP-related exceptions will throw an `ApiError` or inherited-type exception
  - API exception types can be retrieved by HTTP status code via the `EasyPost.Exceptions.Constants` class (i.e. to anticipate what error will be thrown for a 404, etc.)
  - Common exception messages and templates can be found in the `EasyPost.Exceptions.Constants` class (i.e. for log parsing)
- Source code files have been organized
  - Most EasyPost-related objects (i.e. `Shipment`, `Address`, `Tracker`, etc.) are now in the `EasyPost.Model.API` namespace
- Dependencies updated to latest versions, including `RestSharp` v108

### Misc

- Under the hood improvements:
  - Underlying `Request`-`Client`-`ClientConfiguration` relationship has been re-architected to allow for thread safety
  - Process of generating an API request has been standardized and simplified
  - Improved accessibility levels of internal functions, to prevent accidental use by end users
  - Files have been organized into a more logical structure
  - Methods and properties have been organized (e.g. methods ordered by CRUD, properties ordered alphabetically)

## v3.6.1 (2022-09-22)

- Adds missing `dropoff_max_datetime` and `pickup_max_datetime` Shipment options

## v3.6.0 (2022-09-21)

- Adds `end_shipper_id` shipment option
- Adds support to pass an EndShipper ID when buying a shipment
- Add Partner White Label support:
  - Create a referral customer
  - Update a referral customer's email address
  - List all referral customers
  - Add a credit card to a referral customer's account

## v3.5.0 (2022-08-25)

- Adds `ValidateWebhook` function
- Adds `duty_payment` shipment option
- Moves `EndShipper` out of beta to the general namespace

## v3.4.0 (2022-08-02)

- Adds Carbon Offset support
  - Adds the ability to create a shipment with carbon offset
  - Adds the ability to buy a shipment with carbon offset
  - Adds the ability to one-call-buy a shipment with carbon offset
  - Adds the ability to re-rate a shipment with carbon offset
- Removes the unusable `carrier` param from the `verify` function on an Address

## v3.3.0 (2022-07-18)

- Adds ability to generate shipment forms via `GenerateForm` function

## v3.2.0 (2022-07-11)

- Adds `DeletePaymentMethod`, `FundWallet`, and `RetrievePaymentMethods` functions
- Adds `billing_type` attribute in CarrierAccount and Rate classes
- Adds support for webhook secrets
- Collect OS details in User-Agent header
- Update functions now use `patch` instead of `put` under the hood to better match the API behavior and documentation. Behavior of these functions should remain the same

## v3.1.0 (2022-05-19)

- Adds the `EndShipper` class with the ability to create, retrieve, and update EndShipper objects
- Requests will now fail fast with an error if an API key is not provided instead of making a live API call with no key.

## v3.0.0

Upgrading major versions of this project? Refer to the [Upgrade Guide](UPGRADE_GUIDE.md).

### Breaking Changes

- Dropped explicit support for .NET Framework, replaced with .NET Standard 2.0
- Upgrades RestSharp from v106 to v107
- Project was built with C# 8.0
- Project is now entirely asynchronous which will require the addition of async/await on function calls
- Renames methods from `List()` to `All()` to make our library consistent (previously we had methods calling the `/all` endpoint with both names)
- Removes the unusable `Rating` class
- Removes `shipment.GetRates()` method since the shipment object already has rates. If you need to get new rates for a shipment, please use the `shipment.RegenerateRates()` method.
- Must use `verify` and `verify_strict` parameters to verify addresses during creation, per our API docs; `verification` and `strict_verification` will no longer work
- Clarify XList vs XCollection distinction:
  - `ReportList`, `ScanFormList`, `ShipmentList` and `TrackerList` renamed to `ReportCollection`, `ScanFormCollection`, `ShipmentCollection` and `TrackerCollection` to match the other names throughout the project
- Functions previously called `Destroy` are now called `Delete` for consistency (eg: deleting a carrier account)
- Removes non-static `Create()` functions on `Address`, `Order`, `Pickup` and `Shipment` classes for Visual Basic compatibility.

### Features

- Adds explicit support for .NET 5.0 & 6.0
- Adds F# & Visual Basic compatibility
- Adds `RetrieveMe()` which retrieves the authenticated user without the need to pass an ID
- Adds missing `billing_ref` and `dropoff_type` Shipment options
- Adds comprehensive test suite for .NET/.NET Core
- Adds `declaration` attribute to `CustomsInfo` class
- Adds missing `id` property to the `Brand` class
- Adds option to pass in a custom `HttpClient` to the Client constructor (.NET/.NET Core only)
- Adds `CreateAndBuy` function to the Batch class

### Bug Fixes

- Fixes bug where `AddressCollection` was storing `Batch` objects rather than `Address` objects
- Fixes Address creation respecting `verify` and `verify_strict` parameters
- Fixes a bug where Pickup error messages were not deserializing properly

## v2.8.1 (2022-02-17)

- Repackaged the project which contains all the changes made from `2.6.0` - `2.8.0` (see details below)
- Added .NET Core 3.1 to the released package (was previously built but not included starting in `2.6.0`)

**This release includes changes intended for v2.8.0**

- Adds the missing Insurance object and associated actions (closes #47)
- Adds support for updating a user's brand
- Adds support to one-call buy shipments and orders via the `service` key
- Adds support for retrieving all Batch objects
- Adds support for retrieving all Address objects
- Adds support for retrieving all Event objects
- Adds support to regenerate Shipment rates via the `RegenerateRates` method
- Adds support for creating trackers in bulk via the `CreateList` Tracker method
- Removes the unused `orderBy` parameter from the `Batch` object
- Update the `DefaultApiBase` to include `v2` and remove `v2` from every request url string
- Adds the .NET version in use to the User-Agent header
- Add a 30 second connection timeout and a 60 second request timeout for all HTTP requests
- Lints the entire project and adds/updates docstrings throughout
- Fixes the test suite for the project making it runnable once again

**This release includes changes intended for v2.7.0**

- Adds support for tax identifiers (PR #181)

**This release includes changes intended for v2.6.0 & v2.6.1**

- Adds missing `commercial_invoice_letterhead` option (closes #142)
- Adds missing `license_number` option
- Adds missing `receiver_liquor_license` option
- Adds missing `VerificationDetails` object (closes #140, #141, #162)
- Changes the Client delegate constructor from `internal` to `public`
- Updates various information in the README related to thread-safety, examples, releasing, etc
- Adds all missing dates and versions to the CHANGELOG
- Bumps RestSharp from 106.4.2 to 106.13.0
- Adds support for .NET Core 3.1
- Includes SmartRate handling.
- Updated Code Signing Identity.

## v2.8.0 (2022-02-16)

This release was mispackaged, please use `v2.8.1` or newer.

## v2.7.0 (2021-11-24)

This release was mispackaged, please use `v2.8.1` or newer.

## v2.6.0 & v2.6.1 (2021-11-03)

This release was mispackaged, please use `v2.8.1` or newer.

## v2.5.1.3 (2020-01-07)

- Add restricted delivery shipment option
- Correct certified mail type

## v2.5.1.2 (2019-09-29)

- Add certified mail, registered mail, and return receipt shipment options

## v2.5.1.1 (2019-07-05)

- Added suppress etd option

## v2.5.1 (2018-10-09)

- Added overlabel shipment options

## v2.5.0.1 (2018-10-03)

## v2.5.0 (2018-09-28)

## v2.4.0 (2018-06-22)

## v2.3.1.4 (2018-01-09)

## v2.3.1.3 (2017-11-29)

## v2.3.1.2 (2017-05-16)

## v2.3.1.1 (2017-04-20)

## v2.3.1 (2017-03-28)

## v2.3.0 (2017-03-11)

## v2.2.7 (2017-03-07)

## v2.2.6 (2017-01-24)

## v2.2.5 (2017-01-24)

## v2.2.4 (2016-12-15)

## v2.2.3 (2016-12-13)

## v2.2.2 (2016-12-07)

## v2.2.1 (2016-08-26)

## v2.2.0 (2016-08-25)

## v2.1.2.1 (2016-08-19)

## v2.1.1 (2016-07-08)

## v2.1.0 (2016-05-09)

## v2.0.3.1 (2016-03-12)

## v2.0.3 (2016-03-03)

## v2.0.2.1 (2016-02-19)

## v2.0.2 (2016-02-10)

## v2.0.1.1 (2016-01-14)

## v2.0.1 (2016-01-14)

## v2.0.0 (2016-01-12)

## v1.2.2.2 (2015-12-18)

## v1.2.2.1 (2015-11-09)

## v1.2.2 (2015-11-04)

## v1.2.1 (2015-10-27)

## v1.2.0.1 (2015-10-06)

## v1.2.0 (2015-09-15)

## v1.1.7.2 (2015-09-10)

## v1.1.7.1 (2015-08-12)

## v1.1.7 (2015-06-26)

## v1.1.6 (2015-06-11)

## v1.1.5.2 (2015-06-05)

## v1.1.5.1 (2015-05-13)

## v1.1.4.5 (2015-03-20)

## v1.1.4.4 (2015-02-06)

## v1.1.4.3 (2015-02-06)

## v1.1.4.1 (2015-01-29)

## v1.1.4 (2015-01-29)

## v1.1.3.2 (2015-01-20)

## v1.1.3 (2014-12-17)

## v1.1.2.6 (2014-11-19)

## v1.1.2.5 (2014-11-17)

## v1.1.2.4 (2014-11-03)

## v1.1.2.3 (2014-11-03)

## v1.1.2.1 (2014-10-31)

## v1.1.2 (2014-10-31)

## v1.1.1.2 (2014-10-30)

## v1.1.0 (2014-10-07)

## v1.0.1.7 (2014-08-15)

## v1.0.1.6 (2014-07-11)

## v1.0.1.4 (2014-06-10)

## v1.0.1.3 (2014-06-06)
