// <copyright file="OrderTest.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class OrderTest
    {
        Dictionary<string, object> parameters, toAddress, fromAddress;

        List<Dictionary<string, object>> shipments;

        [TestInitialize]
        public void Initialize()
        {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

            toAddress = new Dictionary<string, object>()
            {
                { "company", "Simpler Postage Inc" },
                { "street1", "164 Townsend Street" },
                { "street2", "Unit 1" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94107" },
            };
            fromAddress = new Dictionary<string, object>()
            {
                { "name", "Andrew Tribone" },
                { "street1", "480 Fell St" },
                { "street2", "#3" },
                { "city", "San Francisco" },
                { "state", "CA" },
                { "country", "US" },
                { "zip", "94102" }
            };
            shipments = new List<Dictionary<string, object>>()
            {
                new Dictionary<string, object>()
                {
                    {
                        "parcel",
                        new Dictionary<string, object>()
                            { { "length", 8 }, { "width", 6 }, { "height", 5 }, { "weight", 18 } }
                    }
                },
                new Dictionary<string, object>()
                {
                    {
                        "parcel",
                        new Dictionary<string, object>()
                            { { "length", 9 }, { "width", 5 }, { "height", 4 }, { "weight", 18 } }
                    }
                }
            };

            parameters = new Dictionary<string, object>()
            {
                { "to_address", toAddress },
                { "from_address", fromAddress },
                { "reference", "OrderRef" },
                { "shipments", shipments }
            };
        }

        [TestMethod]
        public void TestCreateAndRetrieveOrder()
        {
            var order = Order.Create(parameters);

            Assert.IsNotNull(order.id);
            Assert.AreEqual(order.reference, "OrderRef");

            var retrieved = Order.Retrieve(order.id);
            Assert.AreEqual(order.id, retrieved.id);
        }

        [TestMethod]
        public void TestGetRates()
        {
            var order = Order.Create(parameters);
            var old = order.rates;
            order.GetRates();
            Assert.AreNotEqual(old, order.rates);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceAlreadyCreated))]
        public void TestCreateOrderWithId()
        {
            var order = new Order() { id = "order_asjhd" };
            order.Create();
        }

        // [TestMethod]
        // public void TestCreateFromInstance() {
        //     var order = new Order() {
        //         to_address = Address.Create(toAddress),
        //         from_address = Address.Create(fromAddress),
        //         reference = "OrderRef",
        //         shipments = shipments.Select(shipment => Shipment.Create(shipment)).ToList(),
        //         carrier_accounts = new List<CarrierAccount>() { new CarrierAccount() { id = "ca_7642d249fdcf47bcb5da9ea34c96dfcf" } }
        //     };

        //     order.Create();

        //     Assert.IsNotNull(order.id);
        //     Assert.AreEqual(order.reference, "OrderRef");
        //     CollectionAssert.AreEqual(
        //         new List<string>() { "ca_7642d249fdcf47bcb5da9ea34c96dfcf" },
        //         new HashSet<string>(order.shipments.SelectMany(s => s.rates).Select(r => r.carrier_account_id)).ToList()
        //     );
        // }

        [TestMethod]
        public void TestBuyOrder()
        {
            var order = Order.Create(parameters);
            order.Buy("USPS", "Priority");

            Assert.IsNotNull(order.shipments[0].postage_label);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpException))]
        public void TestFailure()
        {
            Order.Create(new Dictionary<string, object>());
        }

        [TestMethod]
        public void TestOrderCarrierAccounts()
        {
            var carrierAccounts = new Dictionary<string, object>()
                { { "id", "ca_7642d249fdcf47bcb5da9ea34c96dfcf" } };
            parameters.Add("carrier_accounts", carrierAccounts);
            var order = Order.Create(parameters);

            parameters.Remove("carrier_accounts");
            var largeOrder = Order.Create(parameters);

            Assert.IsTrue(order.rates.Count < largeOrder.rates.Count);
        }
    }
}
