using System.Collections.Generic;
using System.Threading.Tasks;
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

        private static async Task<Order> CreateBasicOrder()
        {
            return await Order.Create(Fixture.BasicOrder);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            Order order = await CreateBasicOrder();

            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.IsTrue(order.id.StartsWith("order_"));
            Assert.IsNotNull(order.rates);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");

            Order order = await CreateBasicOrder();

            Order retrievedOrder = await Order.Retrieve(order.id);

            Assert.IsInstanceOfType(retrievedOrder, typeof(Order));
            // Must compare IDs since other elements of objects may be different
            Assert.AreEqual(order.id, retrievedOrder.id);
        }

        [TestMethod]
        public async Task TestGetRates()
        {
            _vcr.SetUpTest("get_rates");

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
            _vcr.SetUpTest("buy");

            Order order = await CreateBasicOrder();

            await order.Buy(Fixture.Usps, Fixture.UspsService);

            List<Shipment> shipments = order.shipments;

            foreach (var shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
                Assert.IsNotNull(shipment.postage_label);
            }
        }

        [TestMethod]
        public async Task TestLowestRate()
        {
            _vcr.SetUpTest("lowest_rate");

            Order order = await CreateBasicOrder();

            // test lowest rate with no filters
            Rate lowestRate = order.LowestRate();
            Assert.AreEqual("First", lowestRate.service);
            Assert.AreEqual("5.49", lowestRate.rate);
            Assert.AreEqual("USPS", lowestRate.carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new List<string>
            {
                "Priority"
            };
            lowestRate = order.LowestRate(null, services, null, null);
            Assert.AreEqual("Priority", lowestRate.service);
            Assert.AreEqual("7.37", lowestRate.rate);
            Assert.AreEqual("USPS", lowestRate.carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string>
            {
                "BAD_CARRIER"
            };
            Assert.ThrowsException<FilterFailure>(() => order.LowestRate(carriers, null, null, null));
        }
    }
}
