using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
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

            Order order = await Client.Order.Create(Fixtures.BasicOrder);

            Assert.IsType<Order>(order);
            Assert.StartsWith("order_", order.Id);
            Assert.NotNull(order.Rates);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Order order = await Client.Order.Create(Fixtures.BasicOrder);

            Order retrievedOrder = await Client.Order.Retrieve(order.Id);

            Assert.IsType<Order>(retrievedOrder);
            // Must compare IDs since other elements of objects may be different
            Assert.Equal(order.Id, retrievedOrder.Id);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetRates()
        {
            UseVCR("get_rates");

            Order order = await Client.Order.Create(Fixtures.BasicOrder);

            order = await Client.Order.RefreshRates(order.Id);

            List<Rate> rates = order.Rates;

            Assert.NotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsType<Rate>(rate);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            // buy with a carrier and service
            Order order = await Client.Order.Create(Fixtures.BasicOrder);

            order = await Client.Order.Buy(order.Id, Fixtures.Usps, Fixtures.UspsService);

            List<Shipment> shipments = order.Shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
                Assert.NotNull(shipment.PostageLabel);
            }

            // buy with a rate
            order = await Client.Order.Create(Fixtures.BasicOrder);
            Rate rate = order.LowestRate();

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

            Order order = await Client.Order.Create(Fixtures.BasicOrder);

            Rate badCarrierRate = new Rate
            {
                Carrier = null,
                Service = "something",
            };
            Rate badServiceRate = new Rate
            {
                Service = null,
                Carrier = "something",
            };

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await Client.Order.Buy(order.Id, badCarrierRate));
            await Assert.ThrowsAsync<MissingPropertyError>(async () => await Client.Order.Buy(order.Id, badServiceRate));
        }

        [Fact]
        [Testing.Function]
        public void TestLowestRateFilters()
        {
            // Mock rates since these can change from the API and we want to test the local filtering logic, not the API call
            // API call is tested in TestGetRates
            List<Rate> rates = new List<Rate>
            {
                new Rate
                {
                    Service = "Priority",
                    Carrier = "USPS",
                    Price = "7.58",
                },
                new Rate
                {
                    Service = "First",
                    Carrier = "USPS",
                    Price = "6.07",
                },
            };
            Order order = new Order
            {
                Rates = rates,
            };

            // test lowest rate with no filters
            Rate lowestRate = order.LowestRate();
            Assert.Equal("First", lowestRate.Service);
            Assert.Equal("6.07", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new() { "Priority" };
            lowestRate = order.LowestRate(null, services);
            Assert.Equal("Priority", lowestRate.Service);
            Assert.Equal("7.58", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new() { "BAD_CARRIER" };
            Assert.Throws<FilteringError>(() => order.LowestRate(carriers));

            // test lowest rate with empty rate list
            order.Rates = new List<Rate>();
            Assert.Throws<FilteringError>(() => order.LowestRate(carriers));

            // test lowest rate with null rate list
            order.Rates = null;
            Assert.Throws<MissingPropertyError>(() => order.LowestRate(carriers));
        }

        #endregion

        #endregion
    }
}
