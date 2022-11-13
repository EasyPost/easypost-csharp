using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class OrderServiceTests : UnitTest
    {
        public OrderServiceTests() : base("order_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Order order = await CreateBasicOrder();

            Assert.IsType<Order>(order);
            Assert.StartsWith("order_", order.Id);
            Assert.NotNull(order.Rates);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetRates()
        {
            UseVCR("get_rates");

            Order order = await CreateBasicOrder();

            order = await Client.Order.GetRates(order.Id);

            Assert.NotNull(order.Rates);
            foreach (Rate rate in order.Rates)
            {
                Assert.IsType<Rate>(rate);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Order order = await CreateBasicOrder();

            Order retrievedOrder = await Client.Order.Retrieve(order.Id);

            Assert.IsType<Order>(retrievedOrder);
            // Must compare IDs since other elements of objects may be different
            Assert.Equal(order.Id, retrievedOrder.Id);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            // buy with a carrier and service
            Order order = await CreateBasicOrder();

            order = await Client.Order.Buy(order.Id, Fixtures.Usps, Fixtures.UspsService);

            List<Shipment> shipments = order.Shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
                Assert.NotNull(shipment.PostageLabel);
            }

            // buy with a rate
            order = await CreateBasicOrder();
            Rate rate = Client.Order.LowestRate(order);

            order = await Client.Order.Buy(order.Id, rate);

            shipments = order.Shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
                Assert.NotNull(shipment.PostageLabel);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoRateDetails()
        {
            UseVCR("buy_with_no_rate_details");

            Order order = await CreateBasicOrder();

            Rate badCarrierRate = new()
            {
                Carrier = null,
                Service = "something"
            };
            Rate badServiceRate = new()
            {
                Service = null,
                Carrier = "something"
            };

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await Client.Order.Buy(order.Id, badCarrierRate));
            await Assert.ThrowsAsync<MissingPropertyError>(async () => await Client.Order.Buy(order.Id, badServiceRate));
        }

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Order order = await CreateBasicOrder();

            // test lowest rate with no filters
            Rate lowestRate = Client.Order.LowestRate(order);
            Assert.Equal("First", lowestRate.Service);
            Assert.Equal("5.82", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new() { "Priority" };
            lowestRate = Client.Order.LowestRate(order, null, services);
            Assert.Equal("Priority", lowestRate.Service);
            Assert.Equal("8.15", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new() { "BAD_CARRIER" };
            Assert.Throws<FilteringError>(() => Client.Order.LowestRate(order, carriers));

            // test lowest rate with empty rate list
            order.Rates = new List<Rate>();
            Assert.Throws<FilteringError>(() => Client.Order.LowestRate(order, carriers));

            // test lowest rate with null rate list
            order.Rates = null;
            Assert.Throws<MissingPropertyError>(() => Client.Order.LowestRate(order, carriers));
        }

        #endregion

        private async Task<Order> CreateBasicOrder() => await Client.Order.Create(Fixtures.BasicOrder);
    }
}
