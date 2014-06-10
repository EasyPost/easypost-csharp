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
            Client.apiKey = "cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi";

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

            return new Shipment() {to_address = to, from_address = from, parcel = parcel};
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
        [ExpectedException(typeof(ResourceAlreadyCreated))]
        public void TestCreateWithId() {
            Shipment shipment = new Shipment() {id = "shp_asdlf"};
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

            rate = shipment.LowestRate(includeCarriers: new List<Carrier>() {Carrier.UPS});
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(includeServices: new List<Service>() {Service.Priority});
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(excludeCarriers: new List<Carrier>() {Carrier.USPS});
            Assert.AreEqual(rate, lowestUPS);

            rate = shipment.LowestRate(excludeServices: new List<Service>() {Service.ParcelSelect});
            Assert.AreEqual(rate, highestUSPS);

            rate = shipment.LowestRate(includeCarriers: new List<Carrier>() {Carrier.FedEx});
            Assert.IsNull(rate);
        }
    }
}
