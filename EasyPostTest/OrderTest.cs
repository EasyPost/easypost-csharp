using EasyPost;

using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class OrderTest {
        Dictionary<string, object> parameters, toAddress, fromAddress;
        List<Dictionary<string, object>> shipments;

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
            shipments = new List<Dictionary<string, object>>() {
                new Dictionary<string, object>() {    
                    {"parcel", new Dictionary<string, object>() {{"length", 8}, {"width", 6}, {"height", 5}, {"weight", 18}}}
                },
                new Dictionary<string, object>() {    
                    {"parcel", new Dictionary<string, object>() {{"length", 9}, {"width", 5}, {"height", 4}, {"weight", 18}}}
                }
            };
            parameters = new Dictionary<string, object>() {
                {"to_address", toAddress}, {"from_address", fromAddress}, {"reference", "ShipmentRef"}, {"shipments", shipments}
            };
        }

        //private Order createOrderResource() {
        //    Address to = Address.Create(toAddress);
        //    Address from = Address.Create(fromAddress);
        //    List<Container> containers = new List<Container>() {};
        //    List<Item> items = new List<Item>() { };

        //    return new Order() {to_address = to, from_address = from, containers = containers, items = items};
        //}

        [TestMethod]
        public void TestCreateAndRetrieveOrder() {
            Order order = Order.Create(parameters);

            Assert.IsNotNull(order.id);
            Assert.AreEqual(order.reference, "ShipmentRef");

            Order retrieved = Order.Retrieve(order.id);
            Assert.AreEqual(order.id, retrieved.id);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceAlreadyCreated))]
        public void TestCreateOrderWithId() {
            Order order = new Order() { id = "order_asjhd" };
            order.Create();
        }

        // Can implement when we have items and containers working properly
        //[TestMethod]
        //public void TestCreateOrderWithPreCreatedAttributes() {
        //    Order order = createOrderResource();
        //    order.Create();
        //    Assert.IsNotNull(order.id);
        //}

        [TestMethod]
        public void TestBuyOrder() {
            Order order = Order.Create(parameters);
            order.Buy("USPS", "Priority");

            Assert.IsNotNull(order.shipments[0].postage_label);
        }
    }
}
