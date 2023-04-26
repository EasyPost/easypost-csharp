using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
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

            BetaFeatures.Parameters.Orders.Create parameters = Fixtures.Parameters.Orders.Create(data);

            Order order = await Client.Order.Create(parameters);

            Assert.IsType<Order>(order);
            Assert.StartsWith("order_", order.Id);
            Assert.NotNull(order.Rates);
        }
        
        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            // buy with a carrier and service
            Dictionary<string, object> orderCreateData = Fixtures.BasicOrder;

            BetaFeatures.Parameters.Orders.Create orderCreateParameters = Fixtures.Parameters.Orders.Create(orderCreateData);

            Order order = await Client.Order.Create(orderCreateParameters);

            BetaFeatures.Parameters.Orders.Buy orderBuyParameters = new BetaFeatures.Parameters.Orders.Buy(Fixtures.Usps, Fixtures.UspsService);

            order = await Client.Order.Buy(order.Id, orderBuyParameters);

            List<Shipment> shipments = order.Shipments;

            foreach (Shipment shipment in shipments)
            {
                Assert.IsType<Shipment>(shipment);
                Assert.NotNull(shipment.PostageLabel);
            }

            // buy with a rate
            orderCreateData = Fixtures.BasicOrder;

            orderCreateParameters = Fixtures.Parameters.Orders.Create(orderCreateData);

            order = await Client.Order.Create(orderCreateParameters);

            Rate rate = order.LowestRate();

            orderBuyParameters = new BetaFeatures.Parameters.Orders.Buy(rate);

            order = await Client.Order.Buy(order.Id, rate);

            shipments = order.Shipments;

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
