using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.V2;
using EasyPost.Parameters.V2;
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
            UseVCR("buy", ApiVersion.Latest);

            Order order = await CreateBasicOrder();

            order = await order.Buy(new Orders.Buy
            {
                Carrier = Fixture.Usps,
                Service = Fixture.UspsService
            });

            List<Shipment> shipments = order.Shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsInstanceOfType(shipment, typeof(Shipment));
                Assert.IsNotNull(shipment.PostageLabel);
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Order order = await CreateBasicOrder();

            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.IsTrue(order.Id.StartsWith("order_"));
            Assert.IsNotNull(order.Rates);
        }

        [Fact]
        public async Task TestGetRates()
        {
            UseVCR("get_rates", ApiVersion.Latest);

            Order order = await CreateBasicOrder();

            await order.GetRates(); // this does not return anything, does actually update in-place

            List<Rate> rates = order.Rates;

            Assert.IsNotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsInstanceOfType(rate, typeof(Rate));
            }
        }

        [Fact]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate", ApiVersion.Latest);

            Order order = await CreateBasicOrder();

            // test lowest rate with no filters
            Rate lowestRate = order.LowestRate();
            Assert.AreEqual("First", lowestRate.Service);
            Assert.AreEqual("5.49", lowestRate.Price);
            Assert.AreEqual("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new List<string>
            {
                "Priority"
            };
            lowestRate = order.LowestRate(null, services);
            Assert.AreEqual("Priority", lowestRate.Service);
            Assert.AreEqual("7.37", lowestRate.Price);
            Assert.AreEqual("USPS", lowestRate.Carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string>
            {
                "BAD_CARRIER"
            };
            Assert.ThrowsException<FilterFailure>(() => order.LowestRate(carriers));
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Order order = await CreateBasicOrder();


            Order retrievedOrder = await Client.Orders.Retrieve(order.Id);

            Assert.IsInstanceOfType(retrievedOrder, typeof(Order));
            // Must compare IDs since other elements of objects may be different
            Assert.AreEqual(order.Id, retrievedOrder.Id);
        }

        private async Task<Order> CreateBasicOrder() => await Client.Orders.Create(new Orders.Create(Fixture.BasicOrder));
    }
}
