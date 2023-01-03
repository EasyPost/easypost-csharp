using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
    public class OrderTests : UnitTest
    {
        public OrderTests() : base("order")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetRates()
        {
            UseVCR("get_rates");

            Order order = await Client.Order.Create(Fixtures.BasicOrder);

            await order.GetRates();

            List<Rate> rates = order.Rates;

            Assert.NotNull(rates);
            foreach (Rate rate in rates)
            {
                Assert.IsType<Rate>(rate);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestGetRatesWithNoId()
        {
            UseVCR("get_rates_with_no_id");

            Order order = await Client.Order.Create(Fixtures.BasicOrder);
            order.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await order.GetRates());
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            // buy with a carrier and service
            Order order = await Client.Order.Create(Fixtures.BasicOrder);

            order = await order.Buy(Fixtures.Usps, Fixtures.UspsService);

            List<Shipment> shipments = order.Shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
                Assert.NotNull(shipment.PostageLabel);
            }

            // buy with a rate
            order = await Client.Order.Create(Fixtures.BasicOrder);
            Rate rate = order.LowestRate();

            order = await order.Buy(rate);

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
        public async Task TestBuyWithNoId()
        {
            UseVCR("buy_with_no_id");

            Order order = await Client.Order.Create(Fixtures.BasicOrder);
            order.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await order.Buy(Fixtures.Usps, Fixtures.UspsService));
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
                Service = "something"
            };
            Rate badServiceRate = new Rate
            {
                Service = null,
                Carrier = "something"
            };

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await order.Buy(badCarrierRate));
            await Assert.ThrowsAsync<MissingPropertyError>(async () => await order.Buy(badServiceRate));
        }

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Order order = await Client.Order.Create(Fixtures.BasicOrder);

            // test lowest rate with no filters
            Rate lowestRate = order.LowestRate();
            Assert.Equal("First", lowestRate.Service);
            Assert.Equal("5.82", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (this rate is higher than the lowest but should filter)
            List<string> services = new() { "Priority" };
            lowestRate = order.LowestRate(null, services);
            Assert.Equal("Priority", lowestRate.Service);
            Assert.Equal("8.15", lowestRate.Price);
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
    }
}
