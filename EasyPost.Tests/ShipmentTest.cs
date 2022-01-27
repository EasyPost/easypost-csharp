// ShipmentTest.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ShipmentTest
    {
        private Dictionary<string, object> parameters, options, toAddress, fromAddress;

        //Smart rate
        [TestMethod]
        public void ApiCallIsNotNull()
        {
            var shipment = Shipment.Create(parameters);
            var smartrateResult = shipment.GetSmartrates();
            Assert.IsNotNull(smartrateResult);
        }

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

            toAddress = new Dictionary<string, object>
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" }
            };
            fromAddress = new Dictionary<string, object>
            {
                { "name", "Andrew Tribone" },
                { "street1", "480 Fell St" },
                { "street2", "#3" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94102" }
            };
            options = new Dictionary<string, object>();
            parameters = new Dictionary<string, object>
            {
                {
                    "parcel",
                    new Dictionary<string, object>
                    {
                        { "length", 8 }, { "width", 6 }, { "height", 5 }, { "weight", 10 }
                    }
                },
                { "to_address", toAddress },
                { "from_address", fromAddress },
                { "reference", "ShipmentRef" },
                { "options", options }
            };
        }

        [TestMethod]
        public void TestBuyWithInsurance()
        {
            var shipment = Shipment.Create(parameters);
            shipment.GetRates();
            shipment.Buy(shipment.rates.First(), "100.00");

            Assert.AreEqual(shipment.insurance, "100.00");
        }

        [TestMethod]
        public void TestCarrierAccounts()
        {
            var to = Address.Create(toAddress);
            var from = Address.Create(fromAddress);
            var parcel = Parcel.Create(new Dictionary<string, object>
            {
                { "length", 8 }, { "width", 6 }, { "height", 5 }, { "weight", 10 }
            });
            var item = new CustomsItem { description = "description", quantity = 1 };
            var info = new CustomsInfo
            {
                customs_certify = "TRUE", eel_pfc = "NOEEI 30.37(a)", customs_items = new List<CustomsItem> { item }
            };

            var shipment = new Shipment
            {
                to_address = to,
                from_address = from,
                parcel = parcel,
                carrier_accounts = new List<CarrierAccount>
                {
                    new CarrierAccount { id = "ca_7642d249fdcf47bcb5da9ea34c96dfcf" }
                }
            };
            shipment.Create();
            if (shipment.rates.Count > 0)
            {
                Assert.IsTrue(shipment.rates.TrueForAll(r =>
                    r.carrier_account_id == "ca_7642d249fdcf47bcb5da9ea34c96dfcf"));
            }
        }


        [TestMethod]
        public void TestCarrierAccountsString()
        {
            parameters["carrier_accounts"] = new List<string> { "ca_7642d249fdcf47bcb5da9ea34c96dfcf" };
            var shipment = Shipment.Create(parameters);

            foreach (Rate rate in shipment.rates)
            {
                Assert.AreEqual("ca_7642d249fdcf47bcb5da9ea34c96dfcf", rate.carrier_account_id);
            }
        }

        [TestMethod]
        public void TestCreateAndBuyPlusInsurance()
        {
            var shipment = Shipment.Create(parameters);
            Assert.IsNotNull(shipment.rates);
            Assert.AreNotEqual(shipment.rates.Count, 0);

            shipment.Buy(shipment.rates[0]);
            Assert.IsNotNull(shipment.postage_label);
            Assert.AreNotEqual(shipment.fees.Count, 0);
            CollectionAssert.AllItemsAreNotNull(shipment.fees.Select(f => f.type).ToList());

            shipment.Insure(100.1);
            Assert.AreNotEqual(shipment.insurance, 100.1);
        }

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            var shipment = Shipment.Create(parameters);

            Assert.IsNotNull(shipment.id);
            Assert.AreEqual(shipment.reference, "ShipmentRef");

            var retrieved = Shipment.Retrieve(shipment.id);
            Assert.AreEqual(shipment.id, retrieved.id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceAlreadyCreated))]
        public void TestCreateWithId()
        {
            var shipment = new Shipment { id = "shp_asdlf" };
            shipment.Create();
        }

        [TestMethod]
        public void TestCreateWithPreCreatedAttributes()
        {
            var shipment = CreateShipmentResource();
            shipment.Create();
            Assert.IsNotNull(shipment.id);
        }

        [TestMethod]
        public void TestGenerateLabel()
        {
            var shipment = BuyShipment();

            shipment.GenerateLabel("pdf");
            Assert.IsNotNull(shipment.postage_label);
        }

        [TestMethod]
        public void TestGetRatesWithoutCreate()
        {
            var shipment = CreateShipmentResource();
            shipment.GetRates();
            Assert.IsNotNull(shipment.id);
            Assert.IsNotNull(shipment.rates);
        }

        [TestMethod]
        public void TestGetSmartrates()
        {
            var shipment = Shipment.Create(parameters);
            var smartrateResult = shipment.GetSmartrates();
            //Make sure shipment id from smartrate is the same as the created one
            Assert.AreEqual(shipment.rates[0].id, smartrateResult[0].id);
            Assert.IsNotNull(shipment.rates);
        }

        [TestMethod]
        public void TestInstanceOptions()
        {
            var tomorrow = DateTime.SpecifyKind(DateTime.Now.AddDays(1), DateTimeKind.Utc);

            var shipment = CreateShipmentResource();
            shipment.options = new Options { label_date = tomorrow };
            shipment.Create();

            Assert.AreEqual(tomorrow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                ((DateTime)shipment.options.label_date).ToString("yyyy-MM-ddTHH:mm:ssZ"));
        }

        [TestMethod]
        public void TestList()
        {
            var shipmentList = Shipment.List(new Dictionary<string, object> { { "page_size", 1 } });
            Assert.AreNotEqual(0, shipmentList.shipments.Count);

            var nextShipmentList = shipmentList.Next();
            Assert.AreNotEqual(shipmentList.shipments[0].id, nextShipmentList.shipments[0].id);
        }

        [TestMethod]
        public void TestLowestRate()
        {
            var lowestUSPS = new Rate { rate = "1.0", carrier = "USPS", service = "ParcelSelect" };
            var highestUSPS = new Rate { rate = "10.0", carrier = "USPS", service = "Priority" };
            var lowestUPS = new Rate { rate = "2.0", carrier = "UPS", service = "ParcelSelect" };
            var highestUPS = new Rate { rate = "20.0", carrier = "UPS", service = "Priority" };

            var shipment =
                new Shipment { rates = new List<Rate> { highestUSPS, lowestUSPS, highestUPS, lowestUPS } };

            var rate = shipment.LowestRate();
            Assert.AreEqual(rate, lowestUSPS);

            rate = shipment.LowestRate(new List<string> { "UPS" });
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(includeServices: new List<string> { "Priority" });
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(excludeCarriers: new List<string> { "USPS" });
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(excludeServices: new List<string> { "ParcelSelect" });
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(new List<string> { "FedEx" });
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void TestOptions()
        {
            var tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ");
            options["label_date"] = tomorrow;
            options["print_custom"] = new List<Dictionary<string, object>>
            {
                new Dictionary<string, object> { { "value", "value" }, { "name", "name" }, { "barcode", true } }
            };
            options["payment"] = new Dictionary<string, string>
            {
                { "type", "THIRD_PARTY" }, { "account", "12345" }, { "postal_code", "54321" }, { "country", "US" }
            };

            var shipment = Shipment.Create(parameters);

            Assert.AreEqual(((DateTime)shipment.options.label_date).ToString("yyyy-MM-ddTHH:mm:ssZ"), tomorrow);
            Assert.AreEqual(shipment.options.print_custom[0]["value"], "value");
            Assert.AreEqual(shipment.options.print_custom[0]["name"], "name");
            Assert.AreEqual(shipment.options.print_custom[0]["barcode"], true);
            Assert.AreEqual(shipment.options.payment["type"], "THIRD_PARTY");
            Assert.AreEqual(shipment.options.payment["account"], "12345");
            Assert.AreEqual(shipment.options.payment["postal_code"], "54321");
            Assert.AreEqual(shipment.options.payment["country"], "US");
        }

        [TestMethod]
        public void TestPostageInline()
        {
            options["postage_label_inline"] = true;
            var shipment = BuyShipment();
            Assert.IsNotNull(shipment.postage_label.label_file);
        }

        [TestMethod]
        public void TestRateErrorMessages()
        {
            parameters = new Dictionary<string, object>
            {
                { "to_address", toAddress },
                { "from_address", fromAddress },
                {
                    "parcel",
                    new Dictionary<string, object> { { "weight", 10 }, { "predefined_package", "FEDEXBOX" } }
                }
            };
            var shipment = Shipment.Create(parameters);

            Assert.IsNotNull(shipment.id);
            Assert.AreEqual(shipment.messages[0].carrier, "USPS");
            Assert.AreEqual(shipment.messages[0].type, "rate_error");
            Assert.AreEqual(shipment.messages[0].message,
                "Unable to retrieve USPS rates for another carrier's predefined_package parcel type.");
        }

        [TestMethod]
        public void TestRefund()
        {
            var shipment = BuyShipment();
            shipment.Refund();
            Assert.IsNotNull(shipment.refund_status);
        }

        //Tax Identifiers
        [TestMethod]
        public void TestTaxIdentifiers()
        {
            parameters["tax_identifiers"] = new List<TaxIdentifier>
            {
                new TaxIdentifier
                {
                    entity = "SENDER", tax_id = "12345", tax_id_type = "EORI", issuing_country = "GB"
                }
            };
            var shipment = Shipment.Create(parameters);
            Assert.IsNotNull(shipment.id);
            Assert.IsNotNull(shipment.rates);
            var taxIdentifier = shipment.tax_identifiers[0];
            Assert.AreEqual("SENDER", taxIdentifier.entity);
            Assert.AreEqual("HIDDEN", taxIdentifier.tax_id);
            Assert.AreEqual("EORI", taxIdentifier.tax_id_type);
            Assert.AreEqual("GB", taxIdentifier.issuing_country);

            var retrieved = Shipment.Retrieve(shipment.id);

            var retrievedTaxIdentifier = retrieved.tax_identifiers[0];
            Assert.AreEqual("SENDER", retrievedTaxIdentifier.entity);
            Assert.AreEqual("HIDDEN", retrievedTaxIdentifier.tax_id);
            Assert.AreEqual("EORI", retrievedTaxIdentifier.tax_id_type);
            Assert.AreEqual("GB", retrievedTaxIdentifier.issuing_country);
        }

        //TODO: Once we have a library for recording and replaying HTTP, assert that the following values actually match an integer
        [TestMethod]
        public void TestTimeInTransitData()
        {
            var shipment = Shipment.Create(parameters);
            var smartrateResult = shipment.GetSmartrates();
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_50, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_75, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_85, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_90, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_95, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_97, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_99, typeof(int));
        }

        private Shipment BuyShipment()
        {
            var shipment = Shipment.Create(parameters);
            shipment.GetRates();
            shipment.Buy(shipment.rates.First());
            return shipment;
        }

        private Shipment CreateShipmentResource()
        {
            var to = new Address
            {
                company = "Simpler Postage Inc",
                street1 = "164 Townsend Street",
                street2 = "Unit 1",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94107"
            };
            var from = new Address
            {
                name = "Andrew Tribone",
                street1 = "480 Fell St",
                street2 = "#3",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94102"
            };
            var parcel = new Parcel { length = 8, width = 6, height = 5, weight = 10 };
            var item = new CustomsItem { description = "description", quantity = 1 };
            var info = new CustomsInfo
            {
                customs_certify = "TRUE", eel_pfc = "NOEEI 30.37(a)", customs_items = new List<CustomsItem> { item }
            };


            return new Shipment { to_address = to, from_address = from, parcel = parcel, customs_info = info };
        }
    }
}
