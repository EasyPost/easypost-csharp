using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ShipmentTest {
        public Dictionary<string, object> parameters;

        [TestInitialize]
        public void Initialize() {
            Client.apiKey = "cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi";

            Dictionary<string, object> fromAddress = new Dictionary<string, object>() {
                {"name", "Andrew Tribone"}, {"street1", "480 Fell St"}, {"street2", "#3"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94102"}
            };
            Dictionary<string, object> toAddress = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc"}, {"street1", "164 Townsend Street"}, {"street2", "Unit 1"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94107"}
            };
            parameters = new Dictionary<string, object>() {
                {"carrier", "USPS"}, {"service", "Priority"},
                {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
                {"to_address", toAddress}, {"from_address", fromAddress}, {"reference", "ShipmentRef"}
            };
        }
        
        private Shipment buyShipment() {
            Shipment shipment = Shipment.Create(parameters);
            shipment.GetRates();
            shipment.Buy(shipment.rates[0]);
            return shipment;
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
        public void TestGetRatesAndBuyPlusInsurance() {
            Shipment shipment = Shipment.Create(parameters);
            shipment.GetRates();
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
            Rate lowestUSPS = new Rate() {rate = "1.0", carrier = "USPS", service = "ParcelSelect"};
            Rate highestUSPS = new Rate() {rate = "10.0", carrier = "USPS", service = "Priority"};
            Rate lowestUPS = new Rate() {rate = "2.0", carrier = "UPS", service = "ParcelSelect"};
            Rate highestUPS = new Rate() {rate = "20.0", carrier = "UPS", service = "Priority"};

            Shipment shipment = new Shipment() {rates = new List<Rate>() {highestUSPS, lowestUSPS, highestUPS, lowestUPS}};

            Rate rate = shipment.LowestRate();
            Assert.AreEqual(rate, lowestUSPS);

            rate = shipment.LowestRate(includeCarriers: new List<string>() {"UPS"});
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(includeServices: new List<string>() {"Priority"});
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(excludeCarriers: new List<string>() {"USPS"});
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(excludeServices: new List<string>() {"ParcelSelect"});
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(includeCarriers: new List<string>() {"FooBar"});
            Assert.IsNull(rate);
        }
    }
}
