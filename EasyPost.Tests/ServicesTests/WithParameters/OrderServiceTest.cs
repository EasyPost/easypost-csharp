using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class OrderServiceTests : UnitTest
    {
        public OrderServiceTests() : base("order_service_with_parameters")
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

            Dictionary<string, object> data = Fixtures.BasicOrder;

            Parameters.Order.Create parameters = Fixtures.Parameters.Orders.Create(data);

            Order order = await Client.Order.Create(parameters);

            Assert.IsType<Order>(order);
            Assert.StartsWith("order_", order.Id);
            Assert.NotNull(order.Rates);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuyWithCarrierAndService()
        {
            UseVCR("buy_with_carrier_and_service");

            Dictionary<string, object> orderCreateData = Fixtures.BasicOrder;

            Parameters.Order.Create orderCreateParameters = Fixtures.Parameters.Orders.Create(orderCreateData);

            Order order = await Client.Order.Create(orderCreateParameters);

            Parameters.Order.Buy orderBuyParameters = new(Fixtures.Usps, Fixtures.UspsService);

            order = await Client.Order.Buy(order.Id, orderBuyParameters);

            List<Shipment> shipments = order.Shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
                Assert.NotNull(shipment.PostageLabel);
            }
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task BuyWithRate()
        {
            UseVCR("buy_with_rate");

            Dictionary<string, object> orderCreateData = Fixtures.BasicOrder;

            Parameters.Order.Create orderCreateParameters = Fixtures.Parameters.Orders.Create(orderCreateData);

            Order order = await Client.Order.Create(orderCreateParameters);

            Rate rate = order.LowestRate();

            order = await Client.Order.Buy(order.Id, rate);

            List<Shipment> shipments = order.Shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
                Assert.NotNull(shipment.PostageLabel);
            }
        }

        #endregion

        #endregion
    }
}
