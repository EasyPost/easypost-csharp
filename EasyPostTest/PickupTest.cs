using EasyPost;

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class PickupTest {
        Address address;
        Shipment shipment;
        Dictionary<string, object> parameters, toAddress, fromAddress;

        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("WzJHJ6SoPnBVYu0ae4aIHA");

            address = new Address() {
                company = "Simpler Postage Inc",
                street1 = "164 Townsend Street",
                street2 = "Unit 1",
                city = "San Francisco",
                state = "CA",
                country = "US",
                zip = "94107",
                phone = "1234567890"
            };

            toAddress = new Dictionary<string, object>() {
                {"company", "Simpler Postage Inc"}, {"street1", "164 Townsend Street"}, {"street2", "Unit 1"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94107"},
            };
            fromAddress = new Dictionary<string, object>() {
                {"name", "Andrew Tribone"}, {"street1", "480 Fell St"}, {"street2", "#3"},
                {"city", "San Francisco"}, {"state", "CA"}, {"country", "US"}, {"zip", "94102"}
            };
            shipment = Shipment.Create(new Dictionary<string, object>() {
                {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 10}}},
                {"to_address", toAddress}, {"from_address", fromAddress}, {"reference", "ShipmentRef"}
            });
            shipment.Buy(shipment.LowestRate());

            parameters = new Dictionary<string, object>() {
                {"is_account_address", false}, {"address", address}, {"shipment", shipment},
                {"min_datetime", DateTime.Now}, {"max_datetime", DateTime.Now}
            };
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            Pickup pickup = Pickup.Create(parameters);

            Assert.IsNotNull(pickup.id);
            Assert.AreEqual(pickup.address.street1, "164 Townsend Street");

            Pickup retrieved = Pickup.Retrieve(pickup.id);
            Assert.AreEqual(pickup.id, retrieved.id);
        }

        [TestMethod]
        public void TestBuyAndCancel() {
            Pickup pickup = Pickup.Create(parameters);

            pickup.Buy("UPS", pickup.pickup_rates[0].service);
            Assert.IsNotNull(pickup.confirmation);

            pickup.Cancel();
            Assert.AreEqual(pickup.status, "canceled");
        }
    }
}
