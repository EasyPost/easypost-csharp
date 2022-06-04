using System.Collections.Generic;
using System.Threading.Tasks;
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
            UseVCR("buy");

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
            UseVCR("create");

            Order order = await CreateBasicOrder();

            Assert.IsInstanceOfType(order, typeof(Order));
            Assert.IsTrue(order.id.StartsWith("order_"));
            Assert.IsNotNull(order.rates);
        }

        [Fact]
        public async Task TestGetRates()
        {
            UseVCR("get_rates");

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
            UseVCR("retrieve");

            Order order = await CreateBasicOrder();


            Order retrievedOrder = await V2Client.Orders.Retrieve(order.id);

            Assert.IsInstanceOfType(retrievedOrder, typeof(Order));
            // Must compare IDs since other elements of objects may be different
            Assert.AreEqual(order.id, retrievedOrder.id);
        }

        private async Task<Order> CreateBasicOrder() => await V2Client.Orders.Create(Fixture.BasicOrder);
    }
}
