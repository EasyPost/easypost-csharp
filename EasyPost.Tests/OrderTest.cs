using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class OrderTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "order", true);
        }

        private static async Task<Order> CreateBasicOrder()
        {
            return await Order.Create(Fixture.BasicOrder);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Order order = await CreateBasicOrder();

            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.IsTrue(order.id.StartsWith("order_"));
            Assert.IsNotNull(order.rates);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");

            Order order = await CreateBasicOrder();


            Order retrievedOrder = await Order.Retrieve(order.id);

            Assert.IsInstanceOfType(retrievedOrder, typeof(Order));
            // Must compare IDs since other elements of objects may be different
            Assert.AreEqual(order.id, retrievedOrder.id);
        }

        [TestMethod]
        public async Task TestGetRates()
        {
            VCR.Replay("get_rates");


            Order order = await CreateBasicOrder();

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
            VCR.Replay("buy");


            Order order = await CreateBasicOrder();

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
