using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests {
    [TestClass]
    public class ShipmentTest {
        Dictionary<string, object> parameters, options, toAddress, fromAddress;

        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

            toAddress = new Dictionary<string, object>() {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" }
            };
            fromAddress = new Dictionary<string, object>() {
                { "name", "Andrew Tribone" },
                { "street1", "480 Fell St" },
                { "street2", "#3" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94102" }
            };
            options = new Dictionary<string, object>();
            parameters = new Dictionary<string, object>() {
                {"parcel", new Dictionary<string, object>() {
                    { "length", 8 },
                    { "width", 6 },
                    { "height", 5 },
                    { "weight", 10 }
                } },
                { "to_address", toAddress },
                { "from_address", fromAddress },
                { "reference", "ShipmentRef" },
                { "options", options }
            };
        }

        private Shipment BuyShipment() {
            Shipment shipment = Shipment.Create(parameters);
            shipment.GetRates();
            shipment.Buy(shipment.rates.First());
            return shipment;
        }

        private Shipment CreateShipmentResource() {
            Address to = new Address() {
                company = "Simpler Postage Inc",
                street1 = "164 Townsend Street",
                street2 = "Unit 1",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94107"
            };
            Address from = new Address() {
                name = "Andrew Tribone",
                street1 = "480 Fell St",
                street2 = "#3",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94102"
            };
            Parcel parcel = new Parcel() {
                length = 8,
                width = 6,
                height = 5,
                weight = 10
            };
            CustomsItem item = new CustomsItem() { description = "description", quantity = 1 };
            CustomsInfo info = new CustomsInfo() {
                customs_certify = "TRUE",
                eel_pfc = "NOEEI 30.37(a)",
                customs_items = new List<CustomsItem>() { item }
            };


            return new Shipment() {
                to_address = to,
                from_address = from,
                parcel = parcel,
                customs_info = info
            };
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            Shipment shipment = Shipment.Create(parameters);

            Assert.IsNotNull(shipment.id);
            Assert.AreEqual(shipment.reference, "ShipmentRef");

            Shipment retrieved = Shipment.Retrieve(shipment.id);
            Assert.AreEqual(shipment.id, retrieved.id);
        }

        [TestMethod]
        public void TestOptions() {
            string tomorrow = DateTime.Now.AddDays(1).ToString("yyyy-MM-ddTHH:mm:ssZ");
            options["label_date"] = tomorrow;
            options["print_custom"] = new List<Dictionary<string, object>>() {
                new Dictionary<string, object>() {
                    { "value", "value" },
                    { "name", "name" },
                    { "barcode", true }
                }
            };
            options["payment"] = new Dictionary<string, string>() {
                { "type", "THIRD_PARTY" },
                { "account", "12345" },
                { "postal_code", "54321" },
                { "country", "US" }
            };

            Shipment shipment = Shipment.Create(parameters);

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
        public void TestInstanceOptions() {
            DateTime tomorrow = DateTime.SpecifyKind(DateTime.Now.AddDays(1), DateTimeKind.Utc);

            Shipment shipment = CreateShipmentResource();
            shipment.options = new Options() {
                label_date = tomorrow
            };
            shipment.Create();

            Assert.AreEqual(tomorrow.ToString("yyyy-MM-ddTHH:mm:ssZ"), ((DateTime)shipment.options.label_date).ToString("yyyy-MM-ddTHH:mm:ssZ"));
        }

        [TestMethod]
        public void TestRateErrorMessages() {
            parameters = new Dictionary<string, object>() {
                { "to_address", toAddress },
                { "from_address", fromAddress },
                { "parcel", new Dictionary<string, object>() {
                    { "weight", 10 },
                    { "predefined_package", "FEDEXBOX" }
                } }
            };
            Shipment shipment = Shipment.Create(parameters);

            Assert.IsNotNull(shipment.id);
            Assert.AreEqual(shipment.messages[0].carrier, "USPS");
            Assert.AreEqual(shipment.messages[0].type, "rate_error");
            Assert.AreEqual(shipment.messages[0].message, "Unable to retrieve USPS rates for another carrier's predefined_package parcel type.");
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceAlreadyCreated))]
        public void TestCreateWithId() {
            Shipment shipment = new Shipment() { id = "shp_asdlf" };
            shipment.Create();
        }

        [TestMethod]
        public void TestCreateWithPreCreatedAttributes() {
            Shipment shipment = CreateShipmentResource();
            shipment.Create();
            Assert.IsNotNull(shipment.id);
        }

        [TestMethod]
        public void TestGetRatesWithoutCreate() {
            Shipment shipment = CreateShipmentResource();
            shipment.GetRates();
            Assert.IsNotNull(shipment.id);
            Assert.IsNotNull(shipment.rates);
        }

        [TestMethod]
        public void TestCreateAndBuyPlusInsurance() {
            Shipment shipment = Shipment.Create(parameters);
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
        public void TestBuyWithInsurance() {
            Shipment shipment = Shipment.Create(parameters);
            shipment.GetRates();
            shipment.Buy(shipment.rates.First(), "100.00");

            Assert.AreEqual(shipment.insurance, "100.00");
        }

        [TestMethod]
        public void TestPostageInline() {
            options["postage_label_inline"] = true;
            Shipment shipment = BuyShipment();
            Assert.IsNotNull(shipment.postage_label.label_file);
        }

        [TestMethod]
        public void TestRefund() {
            Shipment shipment = BuyShipment();
            shipment.Refund();
            Assert.IsNotNull(shipment.refund_status);
        }

        [TestMethod]
        public void TestGenerateLabel() {
            Shipment shipment = BuyShipment();

            shipment.GenerateLabel("pdf");
            Assert.IsNotNull(shipment.postage_label);
        }

        [TestMethod]
        public void TestLowestRate() {
            Rate lowestUSPS = new Rate() { rate = "1.0", carrier = "USPS", service = "ParcelSelect" };
            Rate highestUSPS = new Rate() { rate = "10.0", carrier = "USPS", service = "Priority" };
            Rate lowestUPS = new Rate() { rate = "2.0", carrier = "UPS", service = "ParcelSelect" };
            Rate highestUPS = new Rate() { rate = "20.0", carrier = "UPS", service = "Priority" };

            Shipment shipment = new Shipment() { rates = new List<Rate>() { highestUSPS, lowestUSPS, highestUPS, lowestUPS } };

            Rate rate = shipment.LowestRate();
            Assert.AreEqual(rate, lowestUSPS);

            rate = shipment.LowestRate(includeCarriers: new List<string>() { "UPS" });
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(includeServices: new List<string>() { "Priority" });
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(excludeCarriers: new List<string>() { "USPS" });
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(excludeServices: new List<string>() { "ParcelSelect" });
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(includeCarriers: new List<string>() { "FedEx" });
            Assert.IsNull(rate);
        }

        [TestMethod]
        public void TestCarrierAccounts() {
            Address to = Address.Create(toAddress);
            Address from = Address.Create(fromAddress);
            Parcel parcel = Parcel.Create(new Dictionary<string, object>() {
                { "length", 8 },
                { "width", 6 },
                { "height", 5 },
                { "weight", 10 }
            });
            CustomsItem item = new CustomsItem() { description = "description", quantity = 1 };
            CustomsInfo info = new CustomsInfo() {
                customs_certify = "TRUE",
                eel_pfc = "NOEEI 30.37(a)",
                customs_items = new List<CustomsItem>() { item }
            };

            Shipment shipment = new Shipment {
                to_address = to,
                from_address = from,
                parcel = parcel,
                carrier_accounts = new List<CarrierAccount> { new CarrierAccount { id = "ca_7642d249fdcf47bcb5da9ea34c96dfcf" } }
            };
            shipment.Create();
            if (shipment.rates.Count > 0)
                Assert.IsTrue(shipment.rates.TrueForAll(r => r.carrier_account_id == "ca_7642d249fdcf47bcb5da9ea34c96dfcf"));
        }


        [TestMethod]
        public void TestCarrierAccountsString() {
            parameters["carrier_accounts"] = new List<string>() { "ca_7642d249fdcf47bcb5da9ea34c96dfcf" };
            Shipment shipment = Shipment.Create(parameters);

            foreach (Rate rate in shipment.rates) {
                Assert.AreEqual("ca_7642d249fdcf47bcb5da9ea34c96dfcf", rate.carrier_account_id);
            }
        }

        [TestMethod]
        public void TestList() {
            ShipmentList shipmentList = Shipment.List(new Dictionary<string, object>() { { "page_size", 1 } });
            Assert.AreNotEqual(0, shipmentList.shipments.Count);

            ShipmentList nextShipmentList = shipmentList.Next();
            Assert.AreNotEqual(shipmentList.shipments[0].id, nextShipmentList.shipments[0].id);
        }

        //Smart rate
        [TestMethod]
        public void ApiCallIsNotNull()
        {
            Shipment shipment = Shipment.Create(parameters);
            List<Smartrate> smartrateResult = shipment.GetSmartrates();
            Assert.IsNotNull(smartrateResult);
        }

        [TestMethod]
        public void TestGetSmartrates()
        {
            Shipment shipment = Shipment.Create(parameters);
            List<Smartrate> smartrateResult = shipment.GetSmartrates();
            //Make sure shipment id from smartrate is the same as the created one
            Assert.AreEqual(shipment.rates[0].id, smartrateResult[0].id);
            Assert.IsNotNull(shipment.rates);
        }

        //TODO: Once we have a library for recording and replaying HTTP, assert that the following values actually match an integer
        [TestMethod]
        public void TestTimeInTransitData()
        {
            Shipment shipment = Shipment.Create(parameters);
            List<Smartrate> smartrateResult = shipment.GetSmartrates();
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_50, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_75, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_85, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_90, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_95, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_97, typeof(int));
            Assert.IsInstanceOfType(smartrateResult[0].time_in_transit.percentile_99, typeof(int));
        }

        //Tax Identifiers
        [TestMethod]
        public void TestTaxIdentifiers()
        {
            parameters["tax_identifiers"] = new List<TaxIdentifier>() {
                new TaxIdentifier {
                    entity = "SENDER",
                    tax_id = "12345",
                    tax_id_type = "EORI",
                    issuing_country = "GB"
                }
            };
            Shipment shipment = Shipment.Create(parameters);
            Assert.IsNotNull(shipment.id);
            Assert.IsNotNull(shipment.rates);
            TaxIdentifier taxIdentifier = shipment.tax_identifiers[0];
            Assert.AreEqual("SENDER", taxIdentifier.entity);
            Assert.AreEqual("HIDDEN", taxIdentifier.tax_id);
            Assert.AreEqual("EORI", taxIdentifier.tax_id_type);
            Assert.AreEqual("GB", taxIdentifier.issuing_country);

            Shipment retrieved = Shipment.Retrieve(shipment.id);

            TaxIdentifier retrievedTaxIdentifier = retrieved.tax_identifiers[0];
            Assert.AreEqual("SENDER", retrievedTaxIdentifier.entity);
            Assert.AreEqual("HIDDEN", retrievedTaxIdentifier.tax_id);
            Assert.AreEqual("EORI", retrievedTaxIdentifier.tax_id_type);
            Assert.AreEqual("GB", retrievedTaxIdentifier.issuing_country);
        }

    }
}
