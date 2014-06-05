using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class ScanFormTest {
        Shipment firstShipment;
        Shipment secondShipment;

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
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                {"carrier", "USPS"}, {"service", "Priority"},
                {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
                {"to_address", toAddress}, {"from_address", fromAddress}, {"reference", "ShipmentRef"}
            };

            firstShipment = Shipment.Create(parameters);
            firstShipment.Buy(firstShipment.LowestRate());
            secondShipment = Shipment.Create(parameters);
            secondShipment.Buy(secondShipment.LowestRate());
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            List<Shipment> trackingCodes = new List<Shipment>() {firstShipment, secondShipment};

            ScanForm scanForm = ScanForm.Create(trackingCodes);
            CollectionAssert.Contains(scanForm.tracking_codes, firstShipment.tracking_code);
            CollectionAssert.Contains(scanForm.tracking_codes, secondShipment.tracking_code);

            ScanForm retrieved = ScanForm.Retrieve(scanForm.id);
            Assert.AreEqual(scanForm.id, retrieved.id);
        }
    }
}
