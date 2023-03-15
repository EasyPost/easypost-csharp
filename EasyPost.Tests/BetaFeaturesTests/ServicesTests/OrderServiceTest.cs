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

        #endregion

        #endregion
    }
}
