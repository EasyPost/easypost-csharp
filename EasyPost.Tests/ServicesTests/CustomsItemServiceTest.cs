using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class CustomsItemServiceTests : UnitTest
    {
        public CustomsItemServiceTests() : base("customs_item_service")
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

            CustomsItem customsItem = await Client.CustomsItem.Create(Fixtures.BasicCustomsItem);

            Assert.IsType<CustomsItem>(customsItem);
            Assert.StartsWith("cstitem_", customsItem.Id);
            Assert.Equal(23.25, customsItem.Value);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CustomsItem customsItem = await Client.CustomsItem.Create(Fixtures.BasicCustomsItem);

            CustomsItem retrievedCustomsItem = await Client.CustomsItem.Retrieve(customsItem.Id);

            Assert.IsType<CustomsItem>(retrievedCustomsItem);
            Assert.Equal(customsItem, retrievedCustomsItem);
        }

        #endregion

        #endregion
    }
}
