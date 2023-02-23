using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Internal.Annotations;
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

        #endregion

        #endregion
    }
}
