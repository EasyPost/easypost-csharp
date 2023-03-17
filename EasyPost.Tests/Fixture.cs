using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Extensions;

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

        internal static Dictionary<string, object> BasicCarrierAccount => GetFixtureStructure().CarrierAccounts.Basic;

        internal static Dictionary<string, object> BasicCustomsInfo => GetFixtureStructure().CustomsInfos.Basic;

        internal static Dictionary<string, object> BasicCustomsItem => GetFixtureStructure().CustomsItems.Basic;

        internal static Dictionary<string, object> BasicInsurance => GetFixtureStructure().Insurances.Basic;

        internal static Dictionary<string, object> BasicOrder => GetFixtureStructure().Orders.Basic;

        internal static Dictionary<string, object> BasicParcel => GetFixtureStructure().Parcels.Basic;

        internal static Dictionary<string, object> BasicPickup
        {
            get
            {
                Dictionary<string, object> fixture = GetFixtureStructure().Pickups.Basic;

                const string pickupDate = "2023-03-21";

                fixture.AddOrUpdate("min_datetime", pickupDate);
                fixture.AddOrUpdate("max_datetime", pickupDate);

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

        internal static string WebhookUrl => GetFixtureStructure().WebhookUrl;


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
                internal static BetaFeatures.Parameters.Addresses.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Addresses.Create
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

                internal static BetaFeatures.Parameters.Addresses.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Addresses.All
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
                internal static BetaFeatures.Parameters.Batches.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    List<Dictionary<string, object>>? shipmentsFixture = fixture.GetOrNull<List<Dictionary<string, object>>>("shipments");

                    List<IShipmentParameter>? shipments = null;
                    if (shipmentsFixture != null)
                    {
                        shipments = new List<IShipmentParameter>();
                        foreach (Dictionary<string, object> shipmentFixture in shipmentsFixture)
                        {
                            shipments.Add(Fixtures.Parameters.Shipments.Create(shipmentFixture));
                        }
                    }

                    return new BetaFeatures.Parameters.Batches.Create
                    {
                        Reference = fixture.GetOrNull<string>("reference"),
                        Shipments = shipments,
                    };
                }

                internal static BetaFeatures.Parameters.Batches.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Batches.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class CarrierAccounts
            {
                internal static BetaFeatures.Parameters.CarrierAccounts.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.CarrierAccounts.Create
                    {
                        Description = fixture.GetOrNull<string>("description"),
                        Type = fixture.GetOrNull<string>("type"),
                        Reference = fixture.GetOrNull<string>("reference"),
                        Credentials = fixture.GetOrNull<Dictionary<string, object?>>("credentials"),
                        TestCredentials = fixture.GetOrNull<Dictionary<string, object?>>("test_credentials"),
                        RegistrationData = fixture.GetOrNull<Dictionary<string, object?>>("registration_data"),
                    };
                }
            }

            internal static class CustomsInfo
            {
                internal static BetaFeatures.Parameters.CustomsInfo.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    List<Dictionary<string, object>>? customsItemsFixture = fixture.GetOrNull<List<Dictionary<string, object>>>("customs_item");

                    List<ICustomsItemParameter>? customsItems = null;
                    if (customsItemsFixture != null)
                    {
                        customsItems = new List<ICustomsItemParameter>();
                        foreach (Dictionary<string, object> customsItemFixture in customsItemsFixture)
                        {
                            customsItems.Add(Fixtures.Parameters.CustomsItems.Create(customsItemFixture));
                        }
                    }

                    return new BetaFeatures.Parameters.CustomsInfo.Create
                    {
                        Id = fixture.GetOrNull<string>("id"),
                        CustomsItems = customsItems,
                        EelPfc = fixture.GetOrNull<string>("eel_pfc"),
                        ContentsType = fixture.GetOrNull<string>("contents_type"),
                        ContentsExplanation = fixture.GetOrNull<string>("contents_explanation"),
                        RestrictionType = fixture.GetOrNull<string>("restriction_type"),
                        NonDeliveryOption = fixture.GetOrNull<string>("non_delivery_option"),
                        CustomsCertify = fixture.GetOrNullBoolean("customs_certify"),
                        CustomsSigner = fixture.GetOrNull<string>("customs_signer"),
                    };
                }
            }

            internal static class CustomsItems
            {
                internal static BetaFeatures.Parameters.CustomsItems.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.CustomsItems.Create
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
                internal static BetaFeatures.Parameters.EndShippers.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.EndShippers.Create
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

                internal static BetaFeatures.Parameters.EndShippers.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.EndShippers.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                    };
                }
            }

            internal static class Events
            {
                internal static BetaFeatures.Parameters.Events.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Events.All
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
                internal static BetaFeatures.Parameters.Insurance.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? toAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("to_address");
                    Dictionary<string, object>? fromAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("from_address");

                    return new BetaFeatures.Parameters.Insurance.Create
                    {
                        Amount = fixture.GetOrNullDouble("amount"),
                        Carrier = fixture.GetOrNull<string>("carrier"),
                        ToAddress = Fixtures.Parameters.Addresses.Create(toAddressFixture),
                        FromAddress = Fixtures.Parameters.Addresses.Create(fromAddressFixture),
                        TrackingCode = fixture.GetOrNull<string>("tracking_code"),
                        Reference = fixture.GetOrNull<string>("reference"),
                    };
                }

                internal static BetaFeatures.Parameters.Insurance.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Insurance.All
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
                internal static BetaFeatures.Parameters.Orders.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? toAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("to_address");
                    Dictionary<string, object>? fromAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("from_address");

                    List<Dictionary<string, object>>? shipmentsFixture = fixture.GetOrNull<List<Dictionary<string, object>>>("shipments");

                    List<IShipmentParameter>? shipments = null;
                    if (shipmentsFixture != null)
                    {
                        shipments = new List<IShipmentParameter>();
                        foreach (Dictionary<string, object> shipmentFixture in shipmentsFixture)
                        {
                            shipments.Add(Fixtures.Parameters.Shipments.Create(shipmentFixture));
                        }
                    }

                    return new BetaFeatures.Parameters.Orders.Create
                    {
                        ToAddress = Fixtures.Parameters.Addresses.Create(toAddressFixture),
                        FromAddress = Fixtures.Parameters.Addresses.Create(fromAddressFixture),
                        Reference = fixture.GetOrNull<string>("reference"),
                        Shipments = shipments,
                    };
                }
            }

            internal static class Parcels
            {
                internal static BetaFeatures.Parameters.Parcels.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Parcels.Create
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
                internal static BetaFeatures.Parameters.Pickups.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? addressFixture = fixture.GetOrNull<Dictionary<string, object>>("address");

                    return new BetaFeatures.Parameters.Pickups.Create
                    {
                        Address = Fixtures.Parameters.Addresses.Create(addressFixture),
                        MinDatetime = fixture.GetOrNull<string>("min_datetime"),
                        MaxDatetime = fixture.GetOrNull<string>("max_datetime"),
                        Instructions = fixture.GetOrNull<string>("instructions"),
                        Shipment = fixture.GetOrNull<Shipment>("shipment"),
                    };
                }

                internal static BetaFeatures.Parameters.Pickups.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Pickups.All
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
                internal static BetaFeatures.Parameters.Beta.Rates.Retrieve RetrieveBeta(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? toAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("to_address");
                    Dictionary<string, object>? fromAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("from_address");
                    Dictionary<string, object>? parcelFixture = fixture.GetOrNull<Dictionary<string, object>>("parcel");

                    return new BetaFeatures.Parameters.Beta.Rates.Retrieve
                    {
                        ToAddress = Fixtures.Parameters.Addresses.Create(toAddressFixture),
                        FromAddress = Fixtures.Parameters.Addresses.Create(fromAddressFixture),
                        Parcel = Fixtures.Parameters.Parcels.Create(parcelFixture),
                    };
                }
            }

            internal static class ReferralCustomers
            {
                internal static BetaFeatures.Parameters.ReferralCustomers.CreateReferralCustomer CreateReferralCustomer(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.ReferralCustomers.CreateReferralCustomer
                    {
                        Email = fixture.GetOrNull<string>("email"),
                        Name = fixture.GetOrNull<string>("name"),
                        PhoneNumber = fixture.GetOrNull<string>("phone"),
                    };
                }

                internal static BetaFeatures.Parameters.ReferralCustomers.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.ReferralCustomers.All
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
                internal static BetaFeatures.Parameters.Refunds.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Refunds.Create
                    {
                        Carrier = fixture.GetOrNull<string>("carrier"),
                        TrackingCodes = fixture.GetOrNull<List<string>>("tracking_codes"),
                    };
                }

                internal static BetaFeatures.Parameters.Refunds.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Refunds.All
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
                internal static BetaFeatures.Parameters.Reports.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Reports.Create
                    {
                        AdditionalColumns = fixture.GetOrNull<List<string>>("additional_columns"),
                        Columns = fixture.GetOrNull<List<string>>("columns"),
                        EndDate = fixture.GetOrNull<string>("end_date"),
                        StartDate = fixture.GetOrNull<string>("start_date"),
                        IncludeChildren = fixture.GetOrNullBoolean("include_children"),
                        SendEmail = fixture.GetOrNullBoolean("send_email"),
                    };
                }

                internal static BetaFeatures.Parameters.Reports.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Reports.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class ScanForms
            {
                internal static BetaFeatures.Parameters.ScanForms.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.ScanForms.All
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
                internal static BetaFeatures.Parameters.Shipments.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    Dictionary<string, object>? toAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("to_address");
                    Dictionary<string, object>? fromAddressFixture = fixture.GetOrNull<Dictionary<string, object>>("from_address");
                    Dictionary<string, object>? parcelFixture = fixture.GetOrNull<Dictionary<string, object>>("parcel");
                    Dictionary<string, object>? customsInfoFixture = fixture.GetOrNull<Dictionary<string, object>>("customs_info");
                    Dictionary<string, object>? optionsFixture = fixture.GetOrNull<Dictionary<string, object>>("options");

                    List<Dictionary<string, object>>? taxIdentifiersFixture = fixture.GetOrNull<List<Dictionary<string, object>>>("tax_identifiers");

                    List<ITaxIdentifierParameter>? taxIdentifiers = null;
                    if (taxIdentifiersFixture != null)
                    {
                        taxIdentifiers = new List<ITaxIdentifierParameter>();
                        foreach (Dictionary<string, object> taxIdentifierFixture in taxIdentifiersFixture)
                        {
                            taxIdentifiers.Add(Fixtures.Parameters.TaxIdentifiers.Create(taxIdentifierFixture));
                        }
                    }

                    return new BetaFeatures.Parameters.Shipments.Create
                    {
                        ToAddress = Fixtures.Parameters.Addresses.Create(toAddressFixture),
                        FromAddress = Fixtures.Parameters.Addresses.Create(fromAddressFixture),
                        Parcel = Fixtures.Parameters.Parcels.Create(parcelFixture),
                        CustomsInfo = Fixtures.Parameters.CustomsInfo.Create(customsInfoFixture),
                        Options = Fixtures.Parameters.Options(optionsFixture),
                        CarbonOffset = fixture.GetOrDefault<bool>("carbon_offset"),  // this will always be true or false, never null
                        CarrierAccountIds = fixture.GetOrNull<List<string>>("carrier_accounts"),
                        Carrier = fixture.GetOrNull<string>("carrier"),
                        Service = fixture.GetOrNull<string>("service"),
                        Reference = fixture.GetOrNull<string>("reference"),
                        TaxIdentifiers = taxIdentifiers,
                        Insurance = fixture.GetOrNullDouble("insurance"),
                    };
                }

                internal static BetaFeatures.Parameters.Shipments.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Shipments.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class TaxIdentifiers
            {
                internal static BetaFeatures.Parameters.TaxIdentifiers.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.TaxIdentifiers.Create
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
                internal static BetaFeatures.Parameters.Trackers.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Trackers.All
                    {
                        PageSize = fixture.GetOrNullInt("page_size"),
                        BeforeId = fixture.GetOrNull<string>("before_id"),
                        AfterId = fixture.GetOrNull<string>("after_id"),
                        StartDatetime = fixture.GetOrNull<string>("start_datetime"),
                        EndDatetime = fixture.GetOrNull<string>("end_datetime"),
                    };
                }
            }

            internal static class Users
            {
                internal static BetaFeatures.Parameters.Users.CreateChild CreateChild(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Users.CreateChild
                    {
                        Name = fixture.GetOrNull<string>("name"),
                    };
                }
            }

            internal static class Webhooks
            {
                internal static BetaFeatures.Parameters.Webhooks.Create Create(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Webhooks.Create
                    {
                        Url = fixture.GetOrNull<string>("url"),
                        Secret = fixture.GetOrNull<string>("secret"),
                    };
                }

                internal static BetaFeatures.Parameters.Webhooks.All All(Dictionary<string, object>? fixture)
                {
                    fixture ??= new Dictionary<string, object>();

                    return new BetaFeatures.Parameters.Webhooks.All
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
