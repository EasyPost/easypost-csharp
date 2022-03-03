using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class OrderTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "order", true);
        }

        private static Order CreateBasicOrder()
        {
            return Order.Create(Fixture.BasicOrder);
        }

        [TestMethod]
        public void TestCreate()
        {
            VCR.Replay("create");

            Order order = CreateBasicOrder();

            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.IsTrue(order.id.StartsWith("order_"));
            Assert.IsNotNull(order.rates);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");

            Order order = CreateBasicOrder();


            Order retrievedOrder = Order.Retrieve(order.id);

            Assert.IsInstanceOfType(retrievedOrder, typeof(Order));
            Assert.AreEqual(order.id, retrievedOrder.id);
        }

        [TestMethod]
        public void TestGetRates()
        {
            VCR.Replay("get_rates");


            Order order = CreateBasicOrder();

            order.GetRates();

            List<Rate> rates = order.rates;

            Assert.IsNotNull(rates);
            foreach (var rate in rates)
            {
                Assert.IsInstanceOfType(rate, typeof(Rate));
            }
        }

        [TestMethod]
        public void TestBuy()
        {
            VCR.Replay("buy");


            Order order = CreateBasicOrder();

            order.Buy(Fixture.Usps, Fixture.UspsService);

            List<Shipment> shipments = order.shipments;

            foreach (var shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
                Assert.IsNotNull(shipment.postage_label);
            }
        }
    }
}
