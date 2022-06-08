using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class OrderTest : UnitTest
    {
        public OrderTest() : base("order")
        {
        }

        [Fact]
        public async Task TestBuy()
        {
            UseVCR("buy", ApiVersion.V2);

            Order order = await CreateBasicOrder();

            await order.Buy(Fixture.Usps, Fixture.UspsService);

            List<Shipment> shipments = order.shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
                Assert.IsNotNull(shipment.postage_label);
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.V2);

            Order order = await CreateBasicOrder();

            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.IsTrue(order.id.StartsWith("order_"));
            Assert.IsNotNull(order.rates);
        }

        [Fact]
        public async Task TestGetRates()
        {
            UseVCR("get_rates", ApiVersion.V2);

            Order order = await CreateBasicOrder();

            await order.GetRates();

            List<Rate> rates = order.rates;

            Assert.IsNotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsInstanceOfType(rate, typeof(Rate));
            }
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.V2);

            Order order = await CreateBasicOrder();


            Order retrievedOrder = await Client.Orders.Retrieve(order.id);

            Assert.IsInstanceOfType(retrievedOrder, typeof(Order));
            // Must compare IDs since other elements of objects may be different
            Assert.AreEqual(order.id, retrievedOrder.id);
        }

        private async Task<Order> CreateBasicOrder() => await Client.Orders.Create(Fixture.BasicOrder);

        [Fact]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate", ApiVersion.V2);

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
