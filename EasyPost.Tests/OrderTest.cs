﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class OrderTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("order");
        }

        private static async Task<Order> CreateBasicOrder(Client client)
        {
            return await client.Orders.Create(Fixture.BasicOrder);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            Client client = _vcr.SetUpTest("create");

            Order order = await CreateBasicOrder(client);

            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.IsTrue(order.id.StartsWith("order_"));
            Assert.IsNotNull(order.rates);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            Client client = _vcr.SetUpTest("retrieve");

            Order order = await CreateBasicOrder(client);


            Order retrievedOrder = await client.Orders.Retrieve(order.id);

            Assert.IsInstanceOfType(retrievedOrder, typeof(Order));
            // Must compare IDs since other elements of objects may be different
            Assert.AreEqual(order.id, retrievedOrder.id);
        }

        [TestMethod]
        public async Task TestGetRates()
        {
            Client client = _vcr.SetUpTest("get_rates");

            Order order = await CreateBasicOrder(client);

            await order.GetRates();

            List<Rate> rates = order.rates;

            Assert.IsNotNull(rates);
            foreach (var rate in rates)
            {
                Assert.IsInstanceOfType(rate, typeof(Rate));
            }
        }

        [TestMethod]
        public async Task TestBuy()
        {
            Client client = _vcr.SetUpTest("buy");

            Order order = await CreateBasicOrder(client);

            await order.Buy(Fixture.Usps, Fixture.UspsService);

            List<Shipment> shipments = order.shipments;

            foreach (var shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
                Assert.IsNotNull(shipment.postage_label);
            }
        }
    }
}
