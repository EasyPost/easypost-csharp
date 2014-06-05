using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class BatchTest {
        Dictionary<string, object> fromAddress;
        Dictionary<string, object> toAddress;
        Dictionary<string, object> shipmentParameters;

        [TestInitialize]
        public void Initialize() {
            Client.apiKey = "cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi";

            fromAddress = new Dictionary<string, object>() {
                {"name", "Andrew Tribone"}, {"street1", "480 Fell St"}, {"street2", "#3"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94102"}
            };
            toAddress = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc"}, {"street1", "164 Townsend Street"}, {"street2", "Unit 1"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94107"}
            };
            shipmentParameters = new Dictionary<string, object>() {
                {"carrier", "USPS"}, {"service", "Priority"},
                {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
                {"to_address", toAddress}, {"from_address", fromAddress}
            };
        }

        [TestMethod]
        public void TestRetrieve() {
            Batch batch = Batch.Create();
            Batch retrieved = Batch.Retrieve(batch.id);
            Assert.AreEqual(batch.id, retrieved.id);
        }

        [TestMethod]
        public void TestAddRemoveShipments() {
            Batch batch = Batch.Create();
            Shipment shipment = Shipment.Create(shipmentParameters);

            batch.AddShipments(new List<Shipment>() {shipment});
            while (batch.shipments == null) { batch = Batch.Retrieve(batch.id); }
            Assert.AreEqual(batch.shipments[0].id, shipment.id);

            batch.RemoveShipments(new List<Shipment>() {shipment});
            while (batch.shipments != null) { batch = Batch.Retrieve(batch.id); }
        }

        [TestMethod]
        public void TestCreateThenBuyThenGenerateLabelAndScanForm() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                {"reference", "EasyPostCSharpTest"},
                {"shipments", new List<Dictionary<string, object>>() {shipmentParameters}}
            };

            Batch batch = Batch.Create(parameters);
            Assert.IsNotNull(batch.id);
            Assert.AreEqual(batch.reference, "EasyPostCSharpTest");
            Assert.AreEqual(batch.state, "creating");

            while (batch.state == "creating") { batch = Batch.Retrieve(batch.id); }
            batch.Buy();

            while (batch.state == "created") { batch = Batch.Retrieve(batch.id); }
            Assert.AreEqual(batch.state, "purchased");

            batch.GenerateLabel("pdf", orderBy: "reference DESC");
            Assert.AreEqual(batch.state, "label_generating");

            batch.GenerateScanForm();
        }
    }
}
