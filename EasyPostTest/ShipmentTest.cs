using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ShipmentTest {
        Dictionary<string, object> parameters, toAddress, fromAddress;

        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");

            toAddress = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc"}, {"street1", "164 Townsend Street"}, {"street2", "Unit 1"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94107"},
            };
            fromAddress = new Dictionary<string, object>() {
                {"name", "Andrew Tribone"}, {"street1", "480 Fell St"}, {"street2", "#3"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94102"}
            };
            parameters = new Dictionary<string, object>() {
                {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
                {"to_address", toAddress}, {"from_address", fromAddress}, {"reference", "ShipmentRef"}
            };
        }

        private Shipment buyShipment() {
            Shipment shipment = Shipment.Create(parameters);
            shipment.GetRates();
            shipment.Buy(shipment.rates.First());
            return shipment;
        }

        private Shipment createShipmentResource() {
            Address to = Address.Create(toAddress);
            Address from = Address.Create(fromAddress);
            Parcel parcel = Parcel.Create(new Dictionary<string, object>() {
                {"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}
            });
            CustomsItem item = new CustomsItem() { description = "description" };
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
            Shipment shipment = Shipment.Create(parameters);

            Assert.AreEqual(shipment.options.label_date, null);
        }

        [TestMethod]
        public void TestRateErrorMessages() {
            parameters = new Dictionary<string, object>() {
                {"parcel", new Dictionary<string, object>() {{"predefined_package", "FEDEXBOX"}, {"weight", 10}}},
                {"to_address", toAddress}, {"from_address", fromAddress}
            };
            Shipment shipment = Shipment.Create(parameters);

            Assert.IsNotNull(shipment.id);
            Assert.AreEqual(shipment.messages[0]["carrier"], "USPS");
            Assert.AreEqual(shipment.messages[0]["type"], "rate_error");
            Assert.AreEqual(shipment.messages[0]["message"], "Unable to retrieve USPS rates for another carrier's predefined_package parcel type.");
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceAlreadyCreated))]
        public void TestCreateWithId() {
            Shipment shipment = new Shipment() { id = "shp_asdlf" };
            shipment.Create();
        }

        [TestMethod]
        public void TestCreateWithPreCreatedAttributes() {
            Shipment shipment = createShipmentResource();
            shipment.Create();
            Assert.IsNotNull(shipment.id);
        }

        [TestMethod]
        public void TestGetRatesWithoutCreate() {
            Shipment shipment = createShipmentResource();
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

            shipment.Insure(100.1);
            Assert.AreNotEqual(shipment.insurance, 100.1);
        }

        [TestMethod]
        public void TestRefund() {
            Shipment shipment = buyShipment();
            shipment.Refund();
            Assert.IsNotNull(shipment.refund_status);
        }

        [TestMethod]
        public void TestGenerateLabelStampBarcode() {
            Shipment shipment = buyShipment();

            shipment.GenerateLabel("pdf");
            Assert.IsNotNull(shipment.postage_label);

            shipment.GenerateStamp();
            Assert.IsNotNull(shipment.stamp_url);

            shipment.GenerateBarcode();
            Assert.IsNotNull(shipment.barcode_url);
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
                {"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}
            });
            CustomsItem item = new CustomsItem() { description = "description" };
            CustomsInfo info = new CustomsInfo() {
                customs_certify = "TRUE",
                eel_pfc = "NOEEI 30.37(a)",
                customs_items = new List<CustomsItem>() { item }
            };

            Shipment shipment = new Shipment();
            shipment.to_address = to;
            shipment.from_address = from;
            shipment.parcel = parcel;
            shipment.carrier_accounts = new List<CarrierAccount> { new CarrierAccount { id = "ca_qn6QC6fd" } };
            shipment.Create();
            if (shipment.rates.Count > 0)
                Assert.IsTrue(shipment.rates.TrueForAll(r => r.carrier_account_id == "ca_qn6QC6fd"));
        }


        [TestMethod]
        public void TestCarrierAccountsString() {
            parameters["carrier_accounts"] = new List<string>() { "ca_qn6QC6fd" };
            Shipment shipment = Shipment.Create(parameters);

            foreach (Rate rate in shipment.rates) {
                Assert.AreEqual("ca_qn6QC6fd", rate.carrier_account_id);
            }
        }

        [TestMethod]
        public void TestList() {
            ShipmentList shipmentList = Shipment.List();
            Assert.AreNotEqual(0, shipmentList.shipments.Count);

            ShipmentList nextShipmentList = shipmentList.Next();
            Assert.AreNotEqual(shipmentList.shipments[0].id, nextShipmentList.shipments[0].id);
        }
    }
}