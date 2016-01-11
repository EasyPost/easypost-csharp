using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class RateTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestRetrieve() {
            Dictionary<string, object> fromAddress = new Dictionary<string, object>() {
                {"name", "Andrew Tribone"}, {"street1", "480 Fell St"}, {"street2", "#3"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94102"}
            };
            Dictionary<string, object> toAddress = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc"}, {"street1", "164 Townsend Street"}, {"street2", "Unit 1"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94107"}
            };
            Shipment shipment = Shipment.Create(new Dictionary<string, object>() {
                {"carrier", "USPS"}, {"service", "Priority"},
                {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
                {"to_address", toAddress}, {"from_address", fromAddress}, {"reference", "ShipmentRef"}
            });

            shipment.GetRates();
            Rate rate = Rate.Retrieve(shipment.rates[0].id);
            Assert.AreEqual(rate.id, shipment.rates[0].id);

            Assert.IsNotNull(rate.rate);
            Assert.IsNotNull(rate.currency);
            Assert.IsNotNull(rate.list_rate);
            Assert.IsNotNull(rate.list_currency);
        }
    }
}
