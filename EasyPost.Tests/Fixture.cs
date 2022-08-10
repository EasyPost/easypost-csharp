using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities;

namespace EasyPost.Tests
{
    public static class Fixture
    {
        // We keep the page_size of retrieving `all` records small so cassettes stay small
        public const int PageSize = 5;

        public const string PickupService = "NextDay";

        public const string ReportDate = "2022-04-12";

        public const string ReportIdPrefix = "shprep_";

        public const string ReportType = "shipment";

        public const string Usps = "USPS";

        public const string UspsService = "First";

        public static Dictionary<string, object?> BasicAddress => new Dictionary<string, object?>()
        {
            {
                "name",
                "Jack Sparrow"
            },
            {
                "company",
                "EasyPost"
            },
            {
                "street1",
                "388 Townsend St"
            },
            {
                "street2",
                "Apt 20"
            },
            {
                "city",
                "San Francisco"
            },
            {
                "state",
                "CA"
            },
            {
                "zip",
                "94107"
            },
            {
                "country",
                "US"
            },
            {
                "phone",
                "5555555555"
            }
        };

        public static Dictionary<string, object?> BasicCarrierAccount => new Dictionary<string, object?>()
        {
            {
                "type",
                "UpsAccount"
            },
            {
                "credentials",
                new Dictionary<string, object?>
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

        public static Dictionary<string, object?> BasicCustomsInfo => new Dictionary<string, object?>()
        {
            {
                "eel_pfc",
                "NOEEI 30.37(a)"
            },
            {
                "customs_certify",
                true
            },
            {
                "customs_signer",
                "Dr. Steve Brule"
            },
            {
                "contents_type",
                "merchandise"
            },
            {
                "contents_explanation",
                ""
            },
            {
                "restriction_type",
                "none"
            },
            {
                "non_delivery_option",
                "return"
            },
            {
                "customs_items",
                new List<object>
                {
                    BasicCustomsItem
                }
            }
        };

        public static Dictionary<string, object?> BasicCustomsItem => new Dictionary<string, object?>()
        {
            {
                "description",
                "Sweet shirts"
            },
            {
                "quantity",
                2
            },
            {
                "weight",
                11
            },
            {
                "value",
                23.00
            },
            {
                "hs_tariff_number",
                654321
            },
            {
                "origin_country",
                "US"
            }
        };

        public static Dictionary<string, object?> BasicOrder => new Dictionary<string, object?>()
        {
            {
                "to_address",
                BasicAddress
            },
            {
                "from_address",
                BasicAddress
            },
            {
                "shipments",
                new List<Dictionary<string, object?>>
                {
                    BasicShipment
                }
            }
        };

        public static Dictionary<string, object?> BasicParcel => new Dictionary<string, object?>()
        {
            {
                "length",
                "10"
            },
            {
                "width",
                "8"
            },
            {
                "height",
                "4"
            },
            {
                "weight",
                "15.4"
            }
        };

        // This fixture will require you to add a `shipment` key with a Shipment object from a test.
        // If you need to re-record cassettes, simply iterate the dates below and ensure they're one day in the future,
        // USPS only does "next-day" pickups including Saturday but not Sunday or Holidays.
        public static Dictionary<string, object?> BasicPickup
        {
            get
            {
                const string pickupDate = "2022-08-11";
                return new Dictionary<string, object?>
                {
                    {
                        "address", BasicAddress
                    },
                    {
                        "min_datetime", pickupDate
                    },
                    {
                        "max_datetime", pickupDate
                    },
                    {
                        "instructions", "Pickup at front door"
                    }
                };
            }
        }

        public static Dictionary<string, object?> BasicShipment => new Dictionary<string, object?>()
        {
            {
                "to_address",
                BasicAddress
            },
            {
                "from_address",
                BasicAddress
            },
            {
                "parcel",
                BasicParcel
            }
        };

        public static Dictionary<string, object?> EndShipperAddress => new Dictionary<string, object?>()
        {
            {
                "name",
                "Jack Sparrow"
            },
            {
                "company",
                "EasyPost"
            },
            {
                "street1",
                "388 Townsend St"
            },
            {
                "street2",
                "Apt 20"
            },
            {
                "city",
                "San Francisco"
            },
            {
                "state",
                "CA"
            },
            {
                "zip",
                "94107"
            },
            {
                "country",
                "US"
            },
            {
                "phone",
                "5555555555"
            },
            {
                "email",
                "test@example.com"
            }
        };

        public static string Event
        {
            get
            {
                Dictionary<string, object?> data = new Dictionary<string, object?>()
                {
                    {
                        "mode",
                        "production"
                    },
                    {
                        "description",
                        "batch.created"
                    },
                    {
                        "previous_attributes",
                        new Dictionary<string, object?>
                        {
                            {
                                "state", "purchasing"
                            }
                        }
                    },
                    {
                        "pending_urls",
                        new List<string>
                        {
                            "example.com/easypost-webhook"
                        }
                    },
                    {
                        "completed_urls",
                        new List<string>()
                    },
                    {
                        "created_at",
                        "2015-12-03T19:09:19Z"
                    },
                    {
                        "updated_at",
                        "2015-12-03T19:09:19Z"
                    },
                    {
                        "result",
                        new Dictionary<string, object?>
                        {
                            {
                                "id", "batch_..."
                            },
                            {
                                "object", "Batch"
                            },
                            {
                                "mode", "production"
                            },
                            {
                                "state", "purchased"
                            },
                            {
                                "num_shipments", 1
                            },
                            {
                                "reference", null
                            },
                            {
                                "created_at", "2015-12-03T19:09:19Z"
                            },
                            {
                                "updated_at", "2015-12-03T19:09:19Z"
                            },
                            {
                                "scan_form", null
                            },
                            {
                                "shipments", new Dictionary<string, object?>
                                {
                                    {
                                        "batch_status", "postage_purchased"
                                    },
                                    {
                                        "batch_message", null
                                    },
                                    {
                                        "id", "shp_123..."
                                    }
                                }
                            },
                            {
                                "status", new Dictionary<string, object?>
                                {
                                    {
                                        "created", 0
                                    },
                                    {
                                        "queued_for_purchase", 0
                                    },
                                    {
                                        "creation_failed", 0
                                    },
                                    {
                                        "postage_purchased", 1
                                    },
                                    {
                                        "postage_purchase_failed", 0
                                    }
                                }
                            },
                            {
                                "pickup", null
                            },
                            {
                                "label_url", null
                            }
                        }
                    },
                    {
                        "id",
                        "evt_..."
                    },
                    {
                        "object",
                        "Event"
                    }
                };
                return JsonSerialization.ConvertObjectToJson(data);
            }
        }

        public static Dictionary<string, object?> FullShipment => new Dictionary<string, object?>()
        {
            {
                "to_address",
                BasicAddress
            },
            {
                "from_address",
                BasicAddress
            },
            {
                "parcel",
                BasicParcel
            },
            {
                "customs_info",
                BasicCustomsInfo
            },
            {
                "options",
                new Dictionary<string, object?>
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
                "reference",
                "123"
            }
        };

        public static Dictionary<string, object?> IncorrectAddressToVerify => new Dictionary<string, object?>()
        {
            {
                "verify",
                new List<bool>
                {
                    true
                }
            },
            {
                "street1",
                "417 montgomery street"
            },
            {
                "street2",
                "FL 5"
            },
            {
                "city",
                "San Francisco"
            },
            {
                "state",
                "CA"
            },
            {
                "zip",
                "94104"
            },
            {
                "country",
                "US"
            },
            {
                "company",
                "EasyPost"
            },
            {
                "phone",
                "415-123-4567"
            }
        };

        public static Dictionary<string, object?> OneCallBuyShipment => new Dictionary<string, object?>()
        {
            {
                "to_address",
                BasicAddress
            },
            {
                "from_address",
                BasicAddress
            },
            {
                "parcel",
                BasicParcel
            },
            {
                "service",
                UspsService
            },
            {
                "carrier_accounts",
                new List<string>
                {
                    UspsCarrierAccountId
                }
            },
            {
                "carrier",
                Usps
            }
        };

        public static Dictionary<string, object?> PickupAddress => new Dictionary<string, object?>()
        {
            {
                "name",
                "Dr. Steve Brule"
            },
            {
                "street1",
                "179 N Harbor Dr"
            },
            {
                "city",
                "Redondo Beach"
            },
            {
                "state",
                "CA"
            },
            {
                "zip",
                "90277"
            },
            {
                "country",
                "US"
            },
            {
                "phone",
                "3331114444"
            }
        };

        public static Dictionary<string, object?> TaxIdentifier => new Dictionary<string, object?>()
        {
            {
                "entity",
                "SENDER"
            },
            {
                "tax_id_type",
                "IOSS"
            },
            {
                "tax_id",
                "12345"
            },
            {
                "issuing_country",
                "GB"
            }
        };

        // This is the USPS carrier account ID that comes with your EasyPost account by default and should be used for all tests
        public static string UspsCarrierAccountId
        {
            get
            {
                string envVar = Environment.GetEnvironmentVariable("USPS_CARRIER_ACCOUNT_ID");
                // Fallback to the EasyPost C# Client Library Test User USPS carrier account
                return envVar ?? "ca_7642d249fdcf47bcb5da9ea34c96dfcf";
            }
        }

        public static async Task<Dictionary<string, object?>> BasicInsurance(Client client)
        {
            Shipment shipment = await client.Shipment.Create(OneCallBuyShipment);
            return new Dictionary<string, object?>
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

        public static Dictionary<string, object?> BasicCarbonOffsetShipment
        {
            get
            {
                return new Dictionary<string, object?>
                {
                    { "to_address", PickupAddress },
                    {"from_address", BasicAddress },
                    {"parcel", BasicParcel },
                };
            }
        }

        public static Dictionary<string, object?> FullCarbonOffsetShipment
        {
            get
            {
                return new Dictionary<string, object?>
                {
                    {
                        "to_address", PickupAddress
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

        public static Dictionary<string, object?> OneCallBuyCarbonOffsetShipment
        {
            get
            {
                return new Dictionary<string, object?>
                {
                    {
                        "to_address", PickupAddress
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
    }
}
