using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Parameters;
using EasyPost.Parameters.Beta;
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

        internal static Address BasicAddress => new Address
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

        internal static CarrierAccount BasicCarrierAccount => new CarrierAccount
        {
            Type = "UpsAccount",
            Credentials = new Dictionary<string, object>
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

        internal static CustomsInfo BasicCustomsInfo => new CustomsInfo
        {
            EelPfc = "NOEEI 30.37(a)",
            CustomsCertify = "true", // TODO: Should this be a bool?
            CustomsSigner = "Dr. Steve Brule",
            ContentsType = "merchandise",
            ContentsExplanation = "",
            RestrictionType = "none",
            NonDeliveryOption = "return",
            CustomsItems = new List<CustomsItem>
            {
                BasicCustomsItem
            }
        };

        internal static CustomsItem BasicCustomsItem => new CustomsItem
        {
            Description = "Sweet shirts",
            Quantity = 2,
            Weight = 11,
            Value = 23.00,
            HsTariffNumber = "654321",
            OriginCountry = "US"
        };

        internal static Order BasicOrder => new Order
        {
            ToAddress = BasicAddress,
            FromAddress = BasicAddress,
            Shipments = new List<Shipment>
            {
                BasicShipment
            }
        };

        internal static Parcel BasicParcel => new Parcel
        {
            Length = 10,
            Width = 8,
            Height = 4,
            Weight = 15.4
        };

        // This fixture will require you to add a `shipment` key with a Shipment object from a test.
        // If you need to re-record cassettes, simply iterate the dates below and ensure they're one day in the future,
        // USPS only does "next-day" pickups including Saturday but not Sunday or Holidays.
        internal static Pickup BasicPickup
        {
            get
            {
                const string pickupDate = "2022-06-17";
                return new Pickup
                {
                    Address = BasicAddress,
                    MinDatetime = DateTime.Parse(pickupDate),
                    MaxDatetime = DateTime.Parse(pickupDate),
                    Instructions = "Pickup at front door"
                };
            }
        }

        internal static Shipment BasicShipment => new Shipment
        {
            ToAddress = BasicAddress,
            FromAddress = BasicAddress,
            Parcel = BasicParcel
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

        internal static Shipment FullShipment => new Shipment
        {
            ToAddress = BasicAddress,
            FromAddress = BasicAddress,
            Parcel = BasicParcel,
            CustomsInfo = BasicCustomsInfo,
            Options = new Options
            {
                LabelFormat = "PNG",
                InvoiceNumber = "123"
            },
            Reference = "123"
        };

        internal static Address IncorrectAddressToVerify => new Address
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

        internal static Dictionary<string, object> OneCallBuyShipment => new Dictionary<string, object>()
        {
            {
                "to_address", BasicAddress
            },
            {
                "from_address", BasicAddress
            },
            {
                "parcel", BasicParcel
            },
            {
                "service", UspsService
            },
            {
                "carrier_accounts", new List<string>
                {
                    UspsCarrierAccountId
                }
            },
            {
                "carrier", Usps
            }
        };

        internal static Shipment OneCallBuyShipment2 => new Shipment
        {
            ToAddress = BasicAddress,
            FromAddress = BasicAddress,
            Parcel = BasicParcel,
            Service = UspsService,
            // TODO: Figure out this time conflict
            CarrierAccounts = new List<CarrierAccount>
            {
                // UspsCarrierAccountId
            },
            // Carrier = Usps
        };

        internal static Address PickupAddress => new Address
        {
            Name = "Dr. Steve Brule",
            Street1 = "179 N Harbor Dr",
            City = "Redondo Beach",
            State = "CA",
            Zip = "90277",
            Country = "US",
            Phone = "3331114444"
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

        internal static async Task<Dictionary<string, object>> BasicInsurance(Client client)
        {
            Shipment shipment = await client.Shipments.Create(new Shipments.Create(OneCallBuyShipment));
            return new Dictionary<string, object>
            {
                {
                    "to_address", BasicAddress
                },
                {
                    "from_address", BasicAddress
                },
                {
                    "tracking_code", shipment.TrackingCode
                },
                {
                    "carrier", Usps
                },
                {
                    "amount", 100
                }
            };
        }
    }

    internal static class FixtureFunctions
    {
        internal static async Task<Address> CreateBasicAddress(this Client client) => await client.Addresses.Create(new Addresses.Create
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
        });

        internal static async Task<Batch> CreateBasicBatch(this Client client) =>
            await client.Batches.Create(new Batches.Create
            {
                Shipments = new List<Shipment>
                {
                    // TODO: Difference between shipment and batch shipment?
                }
            });

        internal static async Task<CustomsInfo> CreateBasicCustomsInfo(this Client client) => await client.CustomsInfo.Create(new Parameters.CustomsInfo.Create
        {
            CustomsInfo = Fixture.BasicCustomsInfo
        });

        internal static async Task<CustomsItem> CreateBasicCustomsItem(this Client client) => await client.CustomsItems.Create(new CustomsItems.Create
        {
            CustomsItem = Fixture.BasicCustomsItem
        });

        internal static async Task<EndShipper> CreateBasicEndShipper(this Client client) => await client.EndShippers.Create(new EndShippers.Create
        {
            EndShipper = Fixture.EndShipper
        });

        internal static async Task<Order> CreateBasicOrder(this Client client) => await client.Orders.Create(new Orders.Create
        {
            Order = Fixture.BasicOrder
        });

        internal static async Task<Parcel> CreateBasicParcel(this Client client) => await client.Parcels.Create(new Parcels.Create
        {
            Parcel = Fixture.BasicParcel
        });

        internal static async Task<Pickup> CreateBasicPickup(this Client client)
        {
            Shipment shipment = await client.Shipments.Create(new Shipments.Create
            {
                Shipment = Fixture.OneCallBuyShipment2
            });
            return await client.Pickups.Create(new Pickups.Create
            {
                Pickup = Fixture.BasicPickup,
                Shipment = shipment
            });
        }

        internal static async Task<List<Refund>> CreateBasicRefund(this Client client)
        {
            Shipment shipment = await client.CreateOneCallBuyShipment();
            Shipment retrievedShipment = await client.Shipments.Retrieve(shipment.Id); // We need to retrieve the shipment so that the tracking_code has time to populate

            return await client.Refunds.Create(new Refunds.Create
            {
                Refund = new Refund
                {
                    Carrier = Fixture.Usps,
                    TrackingCode = retrievedShipment.TrackingCode
                }
            });
        }

        internal static async Task<Shipment> CreateBasicShipment(this Client client) => await client.Shipments.Create(new Shipments.Create
        {
            Shipment = Fixture.BasicShipment
        });

        internal static async Task<Tracker> CreateBasicTracker(this Client client) => await client.Trackers.Create(new Trackers.Create
        {
            Carrier = Fixture.Usps,
            TrackingCode = "EZ1000000001"
        });

        internal static async Task<Shipment> CreateFullShipment(this Client client) => await client.Shipments.Create(new Shipments.Create
        {
            Shipment = Fixture.FullShipment
        });

        internal static async Task<Batch> CreateOneCallBuyBatch(this Client client) =>
            await client.Batches.Create(new Batches.Create
            {
                Shipments = new List<Shipment>
                {
                    // TODO: Difference between shipment and batch shipment?
                }
            });

        internal static async Task<Shipment> CreateOneCallBuyShipment(this Client client) => await client.Shipments.Create(new Shipments.Create
        {
            Shipment = Fixture.OneCallBuyShipment2
        });

        internal static async Task<ScanForm> GetBasicScanForm(this Client client)
        {
            Shipment shipment = await client.Shipments.Create(new Shipments.Create
            {
                Shipment = Fixture.OneCallBuyShipment2
            });
            return await client.ScanForms.Create(new ScanForms.Create
            {
                Shipments = new List<Shipment>
                {
                    shipment
                }
            });
        }

        internal static async Task<User> RetrieveMe(this Client client) => await client.Users.RetrieveMe();
    }
}
