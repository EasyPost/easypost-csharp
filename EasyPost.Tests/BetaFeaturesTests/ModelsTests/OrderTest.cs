using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ModelsTests
{
    public class OrderTests : UnitTest
    {
        public OrderTests() : base("order_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

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

            order = await order.Buy(orderBuyParameters);

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

            order = await order.Buy(rate);

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
