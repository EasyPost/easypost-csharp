using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Extensions;
using ParameterSets = EasyPost.Parameters;

// ReSharper disable once CheckNamespace
namespace EasyPost.Tests._Utilities
{
    public static class Fixtures
    {
        public static byte[] EventBody
        {
            get
            {
                // need to go up levels to get out of the EasyPost.Tests directory
                // this path will get messed up on CI vs local if you change it and/or this Fixture.cs file location
                const string relativePath = "../examples/official/fixtures/event-body.json";
                string fullPath = Path.Combine(TestUtils.GetSourceFileDirectory(), relativePath);

                try
                {
                    string jsonString = File.ReadLines(fullPath).First();
                    return Encoding.UTF8.GetBytes(jsonString);
                }
                catch (Exception e)
                {
#pragma warning disable CA2201
                    throw new Exception($"Unable to read {fullPath}", e);
#pragma warning restore CA2201
                }
            }
        }

        internal static string WebhookHmacSignature => GetFixtureStructure().WebhookHmacSignature;

        internal static string WebhookSecret => GetFixtureStructure().WebhookSecret;

        internal static string WebhookUrl => GetFixtureStructure().WebhookUrl;

        // TODO: Add WebhookCustomHeaders
        // TODO: Pull in updated examples submodule, update all above references to webhook fixture data

        internal static Dictionary<string, object> BasicCarrierAccount => GetFixtureStructure().CarrierAccounts.Basic;

        internal static Dictionary<string, object> BasicCustomsInfo => GetFixtureStructure().CustomsInfos.Basic;

        internal static Dictionary<string, object> BasicCustomsItem => GetFixtureStructure().CustomsItems.Basic;

        internal static Dictionary<string, object> BasicInsurance => GetFixtureStructure().Insurances.Basic;

        internal static Dictionary<string, object> BasicClaim => GetFixtureStructure().Claims.Basic;

        internal static Dictionary<string, object> BasicOrder => GetFixtureStructure().Orders.Basic;

        internal static Dictionary<string, object> BasicParcel => GetFixtureStructure().Parcels.Basic;

        internal static Dictionary<string, object> BasicPickup
        {
            get
            {
                Dictionary<string, object> fixture = GetFixtureStructure().Pickups.Basic;

                const string pickupDate = "2024-08-21";

                fixture!.AddOrUpdate(pickupDate, "min_datetime");
                fixture!.AddOrUpdate(pickupDate, "max_datetime");

                return fixture;
            }
        }

        internal static Dictionary<string, object> BasicShipment => GetFixtureStructure().Shipments.BasicDomestic;

        internal static Dictionary<string, object> CaAddress1 => GetFixtureStructure().Addresses.CaAddress1;

        internal static Dictionary<string, object> CaAddress2 => GetFixtureStructure().Addresses.CaAddress2;

        internal static Dictionary<string, object> CreditCardDetails => GetFixtureStructure().CreditCards.Test;

        internal static Dictionary<string, object> FullShipment => GetFixtureStructure().Shipments.Full;

        internal static Dictionary<string, object> IncorrectAddress => GetFixtureStructure().Addresses.IncorrectAddress;

        internal static Dictionary<string, object> OneCallBuyShipment => new()
        {
            { "to_address", CaAddress1 },
            { "from_address", CaAddress2 },
            { "parcel", BasicParcel },
            { "service", UspsService },
            { "carrier_accounts", new List<string> { UspsCarrierAccountId } },
            { "carrier", Usps },
        };

        internal static int PageSize => GetFixtureStructure().PageSizes.Five;

        internal static string PickupService => GetFixtureStructure().ServiceNames.Usps.PickupService;

        internal static string PlannedShipDate => "2024-10-23";

        internal static string DesiredDeliveryDate => "2024-10-23";

        internal static Dictionary<string, object> ReferralCustomer => GetFixtureStructure().Users.Referral;

        internal static string ReportDate => "2022-04-12";

        internal static string ReportIdPrefix => "shprep_";

        internal static string ReportType => GetFixtureStructure().ReportTypes.Shipment;

        internal static Dictionary<string, object> RmaFormOptions => GetFixtureStructure().FormOptions.Rma;

        internal static Dictionary<string, object> TaxIdentifier => GetFixtureStructure().TaxIdentifiers.Basic;

        internal static string Usps => GetFixtureStructure().CarrierStrings.Usps;

        // This is the USPS carrier account ID that comes with your EasyPost account by default and should be used for all tests
        internal static string UspsCarrierAccountId
        {
            get
            {
                string? envVar = Environment.GetEnvironmentVariable("USPS_CARRIER_ACCOUNT_ID");
                // Fallback to the EasyPost C# Client Library Test User USPS carrier account
                return envVar ?? "ca_7642d249fdcf47bcb5da9ea34c96dfcf";
            }
        }

        internal static string UspsService => GetFixtureStructure().ServiceNames.Usps.FirstService;

        private static FixtureStructure GetFixtureStructure()
        {
            string fixtureData = ReadFixtureData();
            return JsonSerialization.ConvertJsonToObject<FixtureStructure>(fixtureData);
        }

        private static string ReadFixtureData()
        {
            // need to go up levels to get out of the EasyPost.Tests directory
            // this path will get messed up on CI vs local if you change it and/or this Fixture.cs file location
            const string path = "../examples/official/fixtures/client-library-fixtures.json";
            return TestUtils.ReadFile(path);
        }

        internal static class Parameters
        {
            internal static class Addresses
            {
                internal static ParameterSets.Address.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Address.Create
                    {
                        Id = fixture.GetOrNull<string>("id"),
                        Name = fixture.GetOrNull<string>("name"),
                        Street1 = fixture.GetOrNull<string>("street1"),
                        Street2 = fixture.GetOrNull<string>("street2"),
                        City = fixture.GetOrNull<string>("city"),
                        State = fixture.GetOrNull<string>("state"),
                        Zip = fixture.GetOrNull<string>("zip"),
                        Country = fixture.GetOrNull<string>("country"),
                        Phone = fixture.GetOrNull<string>("phone"),
                        Email = fixture.GetOrNull<string>("email"),
                        Verify = fixture.GetOrNullBoolean("verify"),
                        VerifyStrict = fixture.GetOrNullBoolean("verify_strict"),
                    };
                }

                internal static ParameterSets.Address.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Address.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class Batches
            {
                internal static ParameterSets.Batch.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    List<Dictionary<string, object>>? shipmentsFixture = fixture.GetOrNull<List<Dictionary<string, object>>>("shipments");

                    List<ParameterSets.IShipmentParameter>? shipments = null;
                    if (shipmentsFixture != null)
                    {
                        shipments = new List<ParameterSets.IShipmentParameter>();
                        foreach (Dictionary<string, object> shipmentFixture in shipmentsFixture)
                        {
                            shipments.Add(Shipments.Create(shipmentFixture));
                        }
                    }

                    return new ParameterSets.Batch.Create
                    {
                        Reference = fixture.GetOrNull<string>("reference"),
                        Shipments = shipments,
                    };
                }

                internal static ParameterSets.Batch.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Batch.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                        Sort = fixture.GetOrNullEnum<SortDirection>("sort"),
                    };
                }
            }

            internal static class CarrierAccounts
            {
                internal static ParameterSets.CarrierAccount.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.CarrierAccount.Create
                    {
                        Description = fixture.GetOrNull<string>("description"),
                        Type = fixture.GetOrNull<string>("type"),
                        Reference = fixture.GetOrNull<string>("reference"),
                        Credentials = fixture.GetOrNull<Dictionary<string, object?>>("credentials"),
                        TestCredentials = fixture.GetOrNull<Dictionary<string, object?>>("test_credentials"),
                    };
                }

                internal static ParameterSets.CarrierAccount.CreateFedEx CreateFedEx(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.CarrierAccount.CreateFedEx
                    {
                        Description = fixture.GetOrNull<string>("description"),
                        Reference = fixture.GetOrNull<string>("reference"),
                        AccountNumber = "123456789",
                        CorporateAddressCity = "San Francisco",
                        CorporateAddressCountryCode = "US",
                        CorporateAddressPostalCode = "94105",
                        CorporateAddressState = "CA",
                        CorporateAddressStreet = "345 California St",
                        CorporateCompanyName = "EasyPost",
                        CorporateEmailAddress = "me@example.com",
                        CorporateFirstName = "Demo",
                        CorporateLastName = "User",
                        CorporateJobTitle = "Developer",
                        CorporatePhoneNumber = "5555555555",
                        ShippingAddressCity = "San Francisco",
                        ShippingAddressCountryCode = "US",
                        ShippingAddressPostalCode = "94105",
                        ShippingAddressState = "CA",
                        ShippingAddressStreet = "345 California St",
                    };
                }

                internal static ParameterSets.CarrierAccount.CreateUps CreateUps(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.CarrierAccount.CreateUps
                    {
                        Description = fixture.GetOrNull<string>("description"),
                        Reference = fixture.GetOrNull<string>("reference"),
                        AccountNumber = "123456789",
                    };
                }
            }

            internal static class Claims
            {
                internal static ParameterSets.Claim.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Claim.Create
                    {
                        Type = fixture.GetOrNullEnum<ClaimType>("type"),
                        Amount = fixture.GetOrNullDouble("amount"),
                        TrackingCode = fixture.GetOrNull<string>("tracking_code"),
                        EmailEvidenceAttachments = fixture.GetOrNull<string[]>("email_evidence_attachments"),
                        InvoiceAttachments = fixture.GetOrNull<string[]>("invoice_attachments"),
                        SupportingDocumentationAttachments = fixture.GetOrNull<string[]>("supporting_documentation_attachments"),
                        Description = fixture.GetOrNull<string>("description"),
                        ContactEmail = fixture.GetOrNull<string>("contact_email"),
                        PaymentMethod = fixture.GetOrNullEnum<ClaimPaymentMethod>("payment_method"),
                        RecipientName = fixture.GetOrNull<string>("recipient_name"),
                        CheckDeliveryAddress = fixture.GetOrNull<string>("check_delivery_address"),
                    };
                }

                internal static ParameterSets.Claim.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Claim.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class CustomsInfo
            {
                internal static ParameterSets.CustomsInfo.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    List<Dictionary<string, object>>? customsItemsFixture = fixture.GetOrNull<List<Dictionary<string, object>>>("customs_item");

                    List<ParameterSets.ICustomsItemParameter>? customsItems = null;
                    if (customsItemsFixture != null)
                    {
                        customsItems = new List<ParameterSets.ICustomsItemParameter>();
                        foreach (Dictionary<string, object> customsItemFixture in customsItemsFixture)
                        {
                            customsItems.Add(CustomsItems.Create(customsItemFixture));
                        }
                    }

                    string? restrictionType = fixture.GetOrNull<string>("restriction_type");
                    string? restrictionComments = fixture.GetOrNull<string>("restriction_comments");
                    if (restrictionType == "none")
                    {
                        restrictionComments ??= "placeholder";  // required if restrictionType is "none", either use the provided value or a fallback placeholder
                    }

                    return new ParameterSets.CustomsInfo.Create
                    {
                        Id = fixture.GetOrNull<string>("id"),
                        CustomsItems = customsItems,
                        Declaration = fixture.GetOrNull<string>("declaration"),
                        EelPfc = fixture.GetOrNull<string>("eel_pfc"),
                        ContentsType = fixture.GetOrNull<string>("contents_type"),
                        ContentsExplanation = fixture.GetOrNull<string>("contents_explanation"),
                        RestrictionType = restrictionType,
                        RestrictionComments = restrictionComments,
                        NonDeliveryOption = fixture.GetOrNull<string>("non_delivery_option"),
                        CustomsCertify = fixture.GetOrNullBoolean("customs_certify"),
                        CustomsSigner = fixture.GetOrNull<string>("customs_signer"),
                    };
                }
            }

            internal static class CustomsItems
            {
                internal static ParameterSets.CustomsItem.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.CustomsItem.Create
                    {
                        Description = fixture.GetOrNull<string>("description"),
                        Quantity = fixture.GetOrNullInt("quantity"),
                        HsTariffNumber = fixture.GetOrNull<string>("hs_tariff_number"),
                        OriginCountry = fixture.GetOrNull<string>("origin_country"),
                        Value = fixture.GetOrNullDouble("value"),
                        Weight = fixture.GetOrNullDouble("weight"),
                    };
                }
            }

            internal static class EndShippers
            {
                internal static ParameterSets.EndShipper.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.EndShipper.Create
                    {
                        Name = fixture.GetOrNull<string>("name"),
                        Street1 = fixture.GetOrNull<string>("street1"),
                        Street2 = fixture.GetOrNull<string>("street2"),
                        City = fixture.GetOrNull<string>("city"),
                        State = fixture.GetOrNull<string>("state"),
                        Zip = fixture.GetOrNull<string>("zip"),
                        Country = fixture.GetOrNull<string>("country"),
                        Phone = fixture.GetOrNull<string>("phone"),
                        Email = fixture.GetOrNull<string>("email"),
                    };
                }

                internal static ParameterSets.EndShipper.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.EndShipper.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                    };
                }
            }

            internal static class Events
            {
                internal static ParameterSets.Event.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Event.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class Insurance
            {
                internal static ParameterSets.Insurance.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? toAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("to_address");
                    Dictionary<string, object>? fromAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("from_address");

                    return new ParameterSets.Insurance.Create
                    {
                        Amount = fixture.GetOrNullDouble("amount"),
                        Carrier = fixture.GetOrNull<string>("carrier"),
                        ToAddress = Addresses.Create(toAddressFixture),
                        FromAddress = Addresses.Create(fromAddressFixture),
                        TrackingCode = fixture.GetOrNull<string>("tracking_code"),
                        Reference = fixture.GetOrNull<string>("reference"),
                    };
                }

                internal static ParameterSets.Insurance.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Insurance.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static Options Options(Dictionary<string, object>? fixture)
            {
                fixture ??= new Dictionary<string, object>();

                return new Options
                {
                    LabelFormat = fixture.GetOrNull<string>("label_format"),
                    InvoiceNumber = fixture.GetOrNull<string>("invoice_number"),
                };
            }

            internal static class Orders
            {
                internal static ParameterSets.Order.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? toAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("to_address");
                    Dictionary<string, object>? fromAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("from_address");

                    List<Dictionary<string, object>>? shipmentsFixture = fixture.GetOrNull<List<Dictionary<string, object>>>("shipments");

                    List<ParameterSets.IShipmentParameter>? shipments = null;
                    if (shipmentsFixture != null)
                    {
                        shipments = new List<ParameterSets.IShipmentParameter>();
                        foreach (Dictionary<string, object> shipmentFixture in shipmentsFixture)
                        {
                            shipments.Add(Shipments.Create(shipmentFixture));
                        }
                    }

                    return new ParameterSets.Order.Create
                    {
                        ToAddress = Addresses.Create(toAddressFixture),
                        FromAddress = Addresses.Create(fromAddressFixture),
                        Reference = fixture.GetOrNull<string>("reference"),
                        Shipments = shipments,
                    };
                }
            }

            internal static class Parcels
            {
                internal static ParameterSets.Parcel.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Parcel.Create
                    {
                        Id = fixture.GetOrNull<string>("id"),
                        Length = fixture.GetOrNullDouble("length"),
                        Width = fixture.GetOrNullDouble("width"),
                        Height = fixture.GetOrNullDouble("height"),
                        Weight = fixture.GetOrNullDouble("weight"),
                    };
                }
            }

            internal static class Pickups
            {
                internal static ParameterSets.Pickup.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? addressFixture = fixture.GetOrNull<Dictionary<string, object>>("address");

                    return new ParameterSets.Pickup.Create
                    {
                        Address = Addresses.Create(addressFixture),
                        MinDatetime = fixture.GetOrNull<string>("min_datetime"),
                        MaxDatetime = fixture.GetOrNull<string>("max_datetime"),
                        Instructions = fixture.GetOrNull<string>("instructions"),
                        Shipment = fixture.GetOrNull<Shipment>("shipment"),
                    };
                }

                internal static ParameterSets.Pickup.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Pickup.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class Rates
            {
                internal static ParameterSets.Beta.Rate.Retrieve RetrieveBeta(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? toAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("to_address");
                    Dictionary<string, object>? fromAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("from_address");
                    Dictionary<string, object>? parcelFixture = fixture.GetOrNull<Dictionary<string, object>>("parcel");

                    return new ParameterSets.Beta.Rate.Retrieve
                    {
                        ToAddress = Addresses.Create(toAddressFixture),
                        FromAddress = Addresses.Create(fromAddressFixture),
                        Parcel = Parcels.Create(parcelFixture),
                    };
                }
            }

            internal static class ReferralCustomers
            {
                internal static ParameterSets.ReferralCustomer.CreateReferralCustomer CreateReferralCustomer(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.ReferralCustomer.CreateReferralCustomer
                    {
                        Email = fixture.GetOrNull<string>("email"),
                        Name = fixture.GetOrNull<string>("name"),
                        PhoneNumber = fixture.GetOrNull<string>("phone"),
                    };
                }

                internal static ParameterSets.ReferralCustomer.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.ReferralCustomer.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class Refunds
            {
                internal static ParameterSets.Refund.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Refund.Create
                    {
                        Carrier = fixture.GetOrNull<string>("carrier"),
                        TrackingCodes = fixture.GetOrNull<List<string>>("tracking_codes"),
                    };
                }

                internal static ParameterSets.Refund.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Refund.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class Reports
            {
                internal static ParameterSets.Report.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Report.Create
                    {
                        AdditionalColumns = fixture.GetOrNull<List<string>>("additional_columns"),
                        Columns = fixture.GetOrNull<List<string>>("columns"),
                        EndDate = fixture.GetOrNull<string>("end_date"),
                        StartDate = fixture.GetOrNull<string>("start_date"),
                        IncludeChildren = fixture.GetOrNullBoolean("include_children"),
                        SendEmail = fixture.GetOrNullBoolean("send_email"),
                        Type = fixture.GetOrNull<string>("type"),
                    };
                }

                internal static ParameterSets.Report.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Report.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                        Type = fixture.GetOrNull<string>("type"),
                    };
                }
            }

            internal static class ScanForms
            {
                internal static ParameterSets.ScanForm.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.ScanForm.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class Shipments
            {
                internal static ParameterSets.Shipment.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? toAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("to_address");
                    Dictionary<string, object>? fromAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("from_address");
                    Dictionary<string, object>? parcelFixture = fixture.GetOrNull<Dictionary<string, object>>("parcel");
                    Dictionary<string, object>? customsInfoFixture = fixture.GetOrNull<Dictionary<string, object>>("customs_info");
                    Dictionary<string, object>? optionsFixture = fixture.GetOrNull<Dictionary<string, object>>("options");

                    List<Dictionary<string, object>>? taxIdentifiersFixture = fixture.GetOrNull<List<Dictionary<string, object>>>("tax_identifiers");

                    List<ParameterSets.ITaxIdentifierParameter>? taxIdentifiers = null;
                    if (taxIdentifiersFixture != null)
                    {
                        taxIdentifiers = new List<ParameterSets.ITaxIdentifierParameter>();
                        foreach (Dictionary<string, object> taxIdentifierFixture in taxIdentifiersFixture)
                        {
                            taxIdentifiers.Add(TaxIdentifiers.Create(taxIdentifierFixture));
                        }
                    }

                    return new ParameterSets.Shipment.Create
                    {
                        ToAddress = Addresses.Create(toAddressFixture),
                        FromAddress = Addresses.Create(fromAddressFixture),
                        Parcel = Parcels.Create(parcelFixture),
                        CustomsInfo = CustomsInfo.Create(customsInfoFixture),
                        Options = Options(optionsFixture),
                        CarrierAccountIds = fixture.GetOrNull<List<string>>("carrier_accounts"),
                        Carrier = fixture.GetOrNull<string>("carrier"),
                        Service = fixture.GetOrNull<string>("service"),
                        Reference = fixture.GetOrNull<string>("reference"),
                        TaxIdentifiers = taxIdentifiers,
                        Insurance = fixture.GetOrNullDouble("insurance"),
                    };
                }

                internal static ParameterSets.Shipment.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Shipment.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                        IncludeChildren = fixture.GetOrNullBoolean("include_children"),
                        Purchased = fixture.GetOrNullBoolean("purchased"),
                    };
                }
            }

            internal static class TaxIdentifiers
            {
                internal static ParameterSets.TaxIdentifier.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.TaxIdentifier.Create
                    {
                        Entity = fixture.GetOrNull<string>("entity"),
                        TaxIdType = fixture.GetOrNull<string>("tax_id_type"),
                        TaxId = fixture.GetOrNull<string>("tax_id"),
                        IssuingCountry = fixture.GetOrNull<string>("issuing_country"),
                    };
                }
            }

            internal static class Trackers
            {
                internal static ParameterSets.Tracker.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Tracker.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                        TrackingCode = fixture.GetOrNull<string>("tracking_code"),
                        Carrier = fixture.GetOrNull<string>("carrier"),
                    };
                }
            }

            internal static class Users
            {
                internal static ParameterSets.User.CreateChild CreateChild(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.User.CreateChild
                    {
                        Name = fixture.GetOrNull<string>("name"),
                    };
                }

                internal static ParameterSets.User.AllChildren AllChildren(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.User.AllChildren
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                    };
                }
            }

            internal static class Webhooks
            {
                internal static ParameterSets.Webhook.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Webhook.Create
                    {
                        Url = fixture.GetOrNull<string>("url"),
                        Secret = fixture.GetOrNull<string>("secret"),
                    };
                }

                internal static ParameterSets.Webhook.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new ParameterSets.Webhook.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }
        }
    }
}
