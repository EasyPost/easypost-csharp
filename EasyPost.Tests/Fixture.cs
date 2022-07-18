using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Parameters;
using CustomsInfo = EasyPost.Models.API.CustomsInfo;

namespace EasyPost.Tests
{
    internal static class Fixture
    {
        // We keep the page_size of retrieving `all` records small so cassettes stay small
        internal const int PageSize = 5;

        internal const string PickupService = "NextDay";

        // If you need to re-record cassettes, increment this date by 1
        internal const string ReportDate = "2022-04-12";

        internal const string ReportIdPrefix = "shprep_";

        internal const string ReportType = "shipment";

        internal const string Usps = "USPS";

        internal const string UspsService = "First";

        internal static Addresses.Create CreateBasicAddressParams => new Addresses.Create
        {
            Name = "Jack Sparrow",
            Company = "EasyPost",
            Street1 = "388 Townsend St",
            Street2 = "Apt 20",
            City = "San Francisco",
            State = "CA",
            Zip = "94107",
            Country = "US",
            Phone = "5555555555"
        };

        internal static CarrierAccounts.Create CreateBasicCarrierAccountParams => new CarrierAccounts.Create
        {
            Type = "UpsAccount",
            Credentials = new Dictionary<string, object?>
            {
                {
                    "account_number", "A1A1A1"
                },
                {
                    "user_id", "USERID"
                },
                {
                    "password", "PASSWORD"
                },
                {
                    "access_license_number", "ALN"
                }
            }
        };

        internal static Shipments.Create CreateBasicShipmentParams => new Shipments.Create
        {
        };

        internal static Batches.Create CreateBatchParams => new Batches.Create
        {
        };

        internal static Parameters.CustomsInfo.Create CreateCustomsInfoParams => new Parameters.CustomsInfo.Create
        {
            EelPfc = "NOEEI 30.37(a)",
            CustomsCertify = true,
            CustomsSigner = "Dr. Steve Brule",
            ContentsType = "merchandise",
            ContentsExplanation = "",
            RestrictionType = "none",
            NonDeliveryOption = "return",
        };

        internal static Parameters.CustomsItems.Create CreateCustomsItemParams => new Parameters.CustomsItems.Create
        {
            Description = "Sweet shirts",
            Quantity = 2,
            Weight = 11,
            Value = 23.00,
            HsTariffNumber = "654321",
            OriginCountry = "US"
        };

        internal static Parameters.Beta.EndShippers.Create CreateEndShipperParams => new Parameters.Beta.EndShippers.Create
        {
            Name = "Jack Sparrow",
            Company = "EasyPost",
            Street1 = "388 Townsend St",
            Street2 = "Apt 20",
            City = "San Francisco",
            State = "CA",
            Zip = "94107",
            Country = "US",
            Phone = "5555555555",
            Email = "test@example.com"
        };

        internal static Shipments.Create CreateFullShipmentParams => new Shipments.Create
        {
            Options = new Options
            {
                LabelFormat = "PNG",
                InvoiceNumber = "123"
            },
            Reference = "123"
        };

        internal static Addresses.Create CreateIncorrectAddressToVerifyParams => new Addresses.Create
        {
            ToVerify = true,
            Street1 = "417 montgomery street",
            Street2 = "FL 5",
            City = "San Francisco",
            State = "CA",
            Zip = "94104",
            Country = "US",
            Company = "EasyPost",
            Phone = "415-123-4567"
        };

        internal static Parameters.Insurance.Create CreateInsuranceParams => new Parameters.Insurance.Create
        {
            Carrier = Fixture.Usps,
            Amount = 100
        };

        internal static Shipments.Create CreateOneCallBuyShipmentParams => new Shipments.Create
        {
            CarrierAccounts = new List<CarrierAccount>
            {
                new CarrierAccount
                {
                    Id = Fixture.UspsCarrierAccountId
                }
            },
            Carrier = Fixture.Usps,
            Service = Fixture.UspsService
        };

        internal static Orders.Create CreateOrderParams => new Orders.Create
        {
        };

        internal static Parameters.Parcels.Create CreateParcelParams => new Parameters.Parcels.Create
        {
            Length = 10,
            Width = 8,
            Height = 4,
            Weight = 15.4
        };

        internal static Addresses.Create CreatePickupAddressParams => new Addresses.Create
        {
            Name = "Dr. Steve Brule",
            Street1 = "179 N Harbor Dr",
            City = "Redondo Beach",
            State = "CA",
            Zip = "90277",
            Country = "US",
            Phone = "3331114444"
        };

        internal static Parameters.Pickups.Create CreatePickupParams => new Parameters.Pickups.Create
        {
            // If you need to re-record cassettes, simply iterate the dates below and ensure they're one day in the future,
            // USPS only does "next-day" pickups including Saturday but not Sunday or Holidays.
            MinDatetime = new DateTime(2022, 7, 21),
            MaxDatetime = new DateTime(2022, 7, 21),
            Instructions = "Pickup at front door"
        };

        internal static Parameters.Refunds.Create CreateRefundParams => new Parameters.Refunds.Create
        {
            Carrier = Fixture.Usps
        };

        internal static ScanForms.Create CreateScanFormParams => new ScanForms.Create
        {
        };

        internal static Trackers.Create CreateTrackersParams => new Trackers.Create
        {
            Carrier = Fixture.Usps,
            TrackingCode = "EZ1000000001"
        };

        internal static EndShipper EndShipper => new EndShipper
        {
            Name = "Jack Sparrow",
            Company = "EasyPost",
            Street1 = "388 Townsend St",
            Street2 = "Apt 20",
            City = "San Francisco",
            State = "CA",
            Zip = "94107",
            Country = "US",
            Phone = "5555555555",
            Email = "test@example.com"
        };

        internal static TaxIdentifier TaxIdentifier => new TaxIdentifier
        {
            Entity = "SENDER",
            TaxIdType = "IOSS",
            TaxId = "12345",
            IssuingCountry = "GB"
        };

        // This is the USPS carrier account ID that comes with your EasyPost account by default and should be used for all tests
        internal static string UspsCarrierAccountId
        {
            get
            {
                string envVar = Environment.GetEnvironmentVariable("USPS_CARRIER_ACCOUNT_ID");
                // Fallback to the EasyPost C# Client Library Test User USPS carrier account
                return envVar ?? "ca_7642d249fdcf47bcb5da9ea34c96dfcf";
            }
        }
    }

    internal static class FixtureFunctions
    {
        internal static async Task<Address> CreateBasicAddress(this Client client, Addresses.Create? parameters = null)
        {
            parameters ??= Fixture.CreateBasicAddressParams;

            return await client.Addresses.Create(parameters);
        }

        internal static async Task<Batch> CreateBasicBatch(this Client client, Batches.Create? parameters = null)
        {
            parameters ??= Fixture.CreateBatchParams;

            if (parameters.Shipments == null)
            {
                Shipment shipment = await client.CreateBasicShipment();
                parameters.Shipments = new List<Shipment>
                {
                    shipment
                };
            }

            return await client.Batches.Create(parameters);
        }

        internal static async Task<CarrierAccount> CreateBasicCarrierAccount(this Client client, Parameters.CarrierAccounts.Create? parameters = null)
        {
            parameters ??= Fixture.CreateBasicCarrierAccountParams;

            return await client.CarrierAccounts.Create(parameters);
        }

        internal static async Task<CustomsInfo> CreateBasicCustomsInfo(this Client client, Parameters.CustomsInfo.Create? parameters = null)
        {
            parameters ??= Fixture.CreateCustomsInfoParams;

            if (parameters.CustomsItems == null)
            {
                CustomsItem customsItem = await client.CreateBasicCustomsItem();
                parameters.CustomsItems = new List<CustomsItem>
                {
                    customsItem
                };
            }

            return await client.CustomsInfo.Create(parameters);
        }

        internal static async Task<CustomsItem> CreateBasicCustomsItem(this Client client, Parameters.CustomsItems.Create? parameters = null)
        {
            parameters ??= Fixture.CreateCustomsItemParams;

            return await client.CustomsItems.Create(parameters);
        }

        internal static async Task<EndShipper> CreateBasicEndShipper(this Client client, Parameters.Beta.EndShippers.Create? parameters = null)
        {
            parameters ??= Fixture.CreateEndShipperParams;

            return await client.EndShippers.Create(parameters);
        }

        internal static async Task<Models.API.Insurance> CreateBasicInsurance(this Client client, Parameters.Insurance.Create? parameters = null)
        {
            parameters ??= Fixture.CreateInsuranceParams;

            if (parameters.TrackingCode == null)
            {
                Shipment shipment = await client.CreateOneCallBuyShipment();

                Shipment retrievedShipment = await client.Shipments.Retrieve(shipment.Id); // We need to retrieve the shipment so that the tracking_code has time to populate

                parameters.ToAddress = retrievedShipment.ToAddress;
                parameters.FromAddress = retrievedShipment.FromAddress;
                parameters.TrackingCode = retrievedShipment.TrackingCode;
            }

            return await client.Insurance.Create(parameters);
        }

        internal static async Task<Order> CreateBasicOrder(this Client client, Orders.Create? parameters = null)
        {
            parameters ??= Fixture.CreateOrderParams;

            if (parameters.Shipments == null)
            {
                Shipment shipment = await client.CreateBasicShipment();
                parameters.Shipments = new List<Shipment>
                {
                    shipment
                };
                parameters.ToAddress = shipment.ToAddress;
                parameters.FromAddress = shipment.FromAddress;
            }

            return await client.Orders.Create(parameters);
        }

        internal static async Task<Parcel> CreateBasicParcel(this Client client, Parcels.Create? parameters = null)
        {
            parameters ??= Fixture.CreateParcelParams;

            return await client.Parcels.Create(parameters);
        }

        internal static async Task<Pickup> CreateBasicPickup(this Client client, Pickups.Create? parameters = null)
        {
            parameters ??= Fixture.CreatePickupParams;

            if (parameters.Shipment == null)
            {
                Shipment shipment = await client.CreateOneCallBuyShipment();
                parameters.Shipment = shipment;
                parameters.Address = shipment.ToAddress;
            }

            return await client.Pickups.Create(parameters);
        }

        internal static async Task<List<Refund>> CreateBasicRefund(this Client client, Shipment? shipment = null, Refunds.Create? parameters = null)
        {
            shipment ??= await client.CreateOneCallBuyShipment();

            if (shipment.Id == null)
            {
                throw new Exception("Shipment is missing an ID");
            }

            Shipment retrievedShipment = await client.Shipments.Retrieve(shipment.Id); // We need to retrieve the shipment so that the tracking_code has time to populate

            parameters ??= Fixture.CreateRefundParams;
            parameters.TrackingCode = retrievedShipment.TrackingCode;

            return await client.Refunds.Create(parameters);
        }

        internal static async Task<Shipment> CreateBasicShipment(this Client client, Shipments.Create? parameters = null)
        {
            parameters ??= Fixture.CreateBasicShipmentParams;

            if (parameters.FromAddress == null)
            {
                Address address = await client.CreateBasicAddress();
                parameters.FromAddress = address;
                parameters.ToAddress = address;
            }

            parameters.Parcel ??= await client.CreateBasicParcel();

            return await client.Shipments.Create(parameters);
        }

        internal static async Task<Tracker> CreateBasicTracker(this Client client, Trackers.Create? parameters = null)
        {
            parameters ??= Fixture.CreateTrackersParams;

            return await client.Trackers.Create(parameters);
        }

        internal static async Task<Shipment> CreateFullShipment(this Client client, Shipments.Create? parameters = null)
        {
            parameters ??= Fixture.CreateFullShipmentParams;

            if (parameters.FromAddress == null)
            {
                Address address = await client.CreateBasicAddress();
                parameters.FromAddress = address;
                parameters.ToAddress = address;
            }

            parameters.Parcel ??= await client.CreateBasicParcel();
            parameters.CustomsInfo ??= await client.CreateBasicCustomsInfo();

            return await client.Shipments.Create(parameters);
        }

        internal static async Task<Batch> CreateOneCallBuyBatch(this Client client, Batches.Create? parameters = null)
        {
            parameters ??= Fixture.CreateBatchParams;

            if (parameters.Shipments == null)
            {
                Shipment shipment = await client.CreateOneCallBuyShipment();
                parameters.Shipments = new List<Shipment>
                {
                    shipment
                };
            }

            return await client.Batches.Create(parameters);
        }

        internal static async Task<Shipment> CreateOneCallBuyShipment(this Client client, Shipments.Create? parameters = null)
        {
            parameters ??= Fixture.CreateOneCallBuyShipmentParams;

            if (parameters.FromAddress == null)
            {
                Address address = await client.CreateBasicAddress();
                parameters.FromAddress = address;
                parameters.ToAddress = address;
            }

            parameters.Parcel ??= await client.CreateBasicParcel();

            return await client.Shipments.Create(parameters);
        }

        internal static async Task<ScanForm> GetBasicScanForm(this Client client, ScanForms.Create? parameters = null)
        {
            parameters ??= Fixture.CreateScanFormParams;

            if (parameters.Shipments == null)
            {
                Shipment shipment = await client.CreateOneCallBuyShipment();
                parameters.Shipments = new List<Shipment>
                {
                    shipment
                };
            }

            return await client.ScanForms.Create(parameters);
        }

        internal static async Task<User> RetrieveMe(this Client client) => await client.Users.RetrieveMe();
    }
}
