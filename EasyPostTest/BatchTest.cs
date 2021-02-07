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
        Dictionary<string, object> batchShipmentParameters;

        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

            fromAddress = new Dictionary<string, object>() {
                { "name", "Andrew Tribone" },
                { "street1", "480 Fell St" },
                { "street2", "#3" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94102" }
            };
            toAddress = new Dictionary<string, object>() {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" }
            };
            shipmentParameters = new Dictionary<string, object>() {
                { "parcel", new Dictionary<string, object>() {
                    { "length", 8 },
                    { "width", 6 },
                    { "height", 5 },
                    { "weight", 10 }
                } },
                { "to_address", toAddress },
                { "from_address", fromAddress }
            };
            batchShipmentParameters = new Dictionary<string, object>() {
                { "parcel", new Dictionary<string, object>() {
                    { "length", 8 },
                    { "width", 6 },
                    { "height", 5 },
                    { "weight", 10 }
                } },
                { "to_address", toAddress },
                { "from_address", fromAddress },
                { "carrier", "USPS" },
                { "service", "Priority" }
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
            Shipment otherShipment = Shipment.Create(shipmentParameters);

            while (batch.state != "created")
                batch = Batch.Retrieve(batch.id);
            batch.AddShipments(new List<Shipment>() { shipment, otherShipment });

            while (batch.shipments == null) { batch = Batch.Retrieve(batch.id); }
            List<string> shipmentIds = batch.shipments.Select(ship => ship.id).ToList();
            Assert.AreEqual(2, batch.num_shipments);
            CollectionAssert.Contains(shipmentIds, shipment.id);
            CollectionAssert.Contains(shipmentIds, otherShipment.id);

            batch.RemoveShipments(new List<Shipment>() { shipment, otherShipment });
            Assert.AreEqual(0, batch.num_shipments);
        }

        public Batch CreateBatch() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                { "reference", "EasyPostCSharpTest" },
                { "shipments", new List<Dictionary<string, object>>() { batchShipmentParameters } }
            };

            return Batch.Create(parameters);
        }

        [TestMethod]
        public void TestCreateThenBuyThenGenerateLabelAndScanForm() {
            Batch batch = CreateBatch();

            Assert.IsNotNull(batch.id);
            Assert.AreEqual("EasyPostCSharpTest", batch.reference);
            Assert.AreEqual("creating", batch.state);

            while (batch.state == "creating") { batch = Batch.Retrieve(batch.id); }
            batch.Buy();

            while (batch.state == "created") { batch = Batch.Retrieve(batch.id); }
            Assert.AreEqual("purchased", batch.state);

            batch.GenerateLabel("pdf");
            Assert.AreEqual("label_generating", batch.state);

            batch.GenerateScanForm();
        }

        [TestMethod]
        public void TestGenerateLabelWithOrderBy() {
            Batch batch = CreateBatch();

            while (batch.state == "creating") { batch = Batch.Retrieve(batch.id); }
            batch.Buy();

            while (batch.state == "created") { batch = Batch.Retrieve(batch.id); }
            batch.GenerateLabel("pdf", orderBy: "reference DESC");

            Assert.AreEqual("label_generating", batch.state);
        }
    }
}
