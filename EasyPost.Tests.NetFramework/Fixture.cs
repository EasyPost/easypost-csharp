using System;
using System.Collections.Generic;

namespace EasyPost.Tests.NetFramework
{
    public static class Fixture
    {
        // We keep the page_size of retrieving `all` records small so cassettes stay small
        public const int PageSize = 5;

        // This is the carrier account ID for the default USPS account that comes by default. All tests should use this carrier account
        public const string UspsCarrierAccountId = "ca_7642d249fdcf47bcb5da9ea34c96dfcf";

        // TODO: this ID doesn't exist for the API keys being used for the C# tests
        public const string ChildUserId = "user_608a91d0487e419bb465e5acbc999056";

        public const string Usps = "USPS";

        public const string UspsService = "First";

        public const string NextDayService = "NextDay";

        // If ever these need to change due to re-recording cassettes, simply increment this date by 1
        public const string ReportStartDate = "2022-02-01";

        // If ever these need to change due to re-recording cassettes, simply increment this date by 1
        public const string ReportEndDate = "2022-02-03";

        public static string RandomUrl => $"https://{Guid.NewGuid().ToString().Substring(0, 8)}.com";

        public static Dictionary<string, object> BasicAddress
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "name", "Jack Sparrow"
                    },
                    {
                        "company", "EasyPost"
                    },
                    {
                        "street1", "388 Townsend St"
                    },
                    {
                        "street2", "Apt 20"
                    },
                    {
                        "city", "San Francisco"
                    },
                    {
                        "state", "CA"
                    },
                    {
                        "zip", "94107"
                    },
                    {
                        "country", "US"
                    },
                    {
                        "phone", "5555555555"
                    },
                };
            }
        }

        public static Dictionary<string, object> IncorrectAddressToVerify
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "verify", new List<bool>
                        {
                            true
                        }
                    },
                    {
                        "street1", "417 montgomery streat"
                    },
                    {
                        "street2", "FL 5"
                    },
                    {
                        "city", "San Francisco"
                    },
                    {
                        "state", "CA"
                    },
                    {
                        "zip", "94104"
                    },
                    {
                        "country", "US"
                    },
                    {
                        "company", "EasyPost"
                    },
                    {
                        "phone", "415-123-4567"
                    }
                };
            }
        }

        public static Dictionary<string, object> PickupAddress
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "name", "Dr. Steve Brule"
                    },
                    {
                        "street1", "179 N Harbor Dr"
                    },
                    {
                        "city", "Redondo Beach"
                    },
                    {
                        "state", "CA"
                    },
                    {
                        "zip", "90277"
                    },
                    {
                        "country", "US"
                    },
                    {
                        "phone", "3331114444"
                    }
                };
            }
        }

        public static Dictionary<string, object> BasicParcel
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "length", "10"
                    },
                    {
                        "width", "8"
                    },
                    {
                        "height", "4"
                    },
                    {
                        "weight", "15.4"
                    }
                };
            }
        }

        public static Dictionary<string, object> BasicCustomsItem
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "description", "Sweet shirts"
                    },
                    {
                        "quantity", 2
                    },
                    {
                        "weight", 11
                    },
                    {
                        "value", 23.00
                    },
                    {
                        "hs_tariff_number", 654321
                    },
                    {
                        "origin_country", "US"
                    }
                };
            }
        }

        public static Dictionary<string, object> BasicCustomsInfo
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "eel_pfc", "NOEEI 30.37(a)"
                    },
                    {
                        "customs_certify", true
                    },
                    {
                        "customs_signer", "Dr. Steve Brule"
                    },
                    {
                        "contents_type", "merchandise"
                    },
                    {
                        "contents_explanation", ""
                    },
                    {
                        "restriction_type", "none"
                    },
                    {
                        "non_delivery_option", "return"
                    },
                    {
                        "customs_items", new List<object>
                        {
                            BasicCustomsItem
                        }
                    }
                };
            }
        }

        public static Dictionary<string, object> BasicCarrierAccount
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "type", "UpsAccount"
                    },
                    {
                        "credentials", new Dictionary<string, object>
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
                    }
                };
            }
        }

        public static Dictionary<string, object> TaxIdentifier
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "entity", "SENDER"
                    },
                    {
                        "tax_id_type", "IOSS"
                    },
                    {
                        "tax_id", "12345"
                    },
                    {
                        "issuing_country", "GB"
                    }
                };
            }
        }

        public static Dictionary<string, object> BasicShipment
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "to_address", BasicAddress
                    },
                    {
                        "from_address", BasicAddress
                    },
                    {
                        "parcel", BasicParcel
                    }
                };
            }
        }

        public static Dictionary<string, object> FullShipment
        {
            get
            {
                return new Dictionary<string, object>
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
                        "customs_info", BasicCustomsInfo
                    },
                    {
                        "options", new Dictionary<string, object>
                        {
                            {
                                "label_format", "PNG" // Must be PNG so we can convert to ZPL later
                            },
                            {
                                "invoice_number", "123"
                            }
                        }
                    },
                    {
                        "reference", "123"
                    }
                };
            }
        }

        public static Dictionary<string, object> OneCallBuyShipment
        {
            get
            {
                return new Dictionary<string, object>
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
            }
        }

        public static Dictionary<string, object> BasicInsurance
        {
            get
            {
                Shipment shipment = Shipment.Create(OneCallBuyShipment);
                return new Dictionary<string, object>
                {
                    {
                        "to_address", BasicAddress
                    },
                    {
                        "from_address", BasicAddress
                    },
                    {
                        "tracking_code", shipment.tracking_code
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

        // This fixture will require you to add a `shipment` key with a Shipment object from a test.
        // If you need to re-record cassettes, simply iterate the dates below and ensure they're one day in the future,
        // USPS only does "next-day" pickups including Saturday but not Sunday or Holidays.
        public static Dictionary<string, object> BasicPickup
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "address", BasicAddress
                    },
                    {
                        "min_datetime", (DateTime.Today.Date + TimeSpan.FromDays(1)).ToString("yyyy-MM-dd")
                    },
                    {
                        "max_datetime", (DateTime.Today.Date + TimeSpan.FromDays(2)).ToString("yyyy-MM-dd")
                    },
                    {
                        "instructions", "Pickup at front door"
                    }
                };
            }
        }

        public static Dictionary<string, object> BasicOrder
        {
            get
            {
                return new Dictionary<string, object>
                {
                    {
                        "to_address", BasicAddress
                    },
                    {
                        "from_address", BasicAddress
                    },
                    {
                        "shipments", new List<Dictionary<string, object>>
                        {
                            BasicShipment
                        }
                    }
                };
            }
        }
    }
}
