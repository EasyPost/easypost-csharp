using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class CustomsItemTest : UnitTest
    {
        public CustomsItemTest() : base("customs_item")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            CustomsItem customsItem = await CreateBasicCustomsItem();

            Assert.IsType<CustomsItem>(customsItem);
            Assert.StartsWith("cstitem_", customsItem.Id);
            Assert.Equal(23.25, customsItem.Value);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CustomsItem customsItem = await CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = await Client.CustomsItem.Retrieve(customsItem.Id);

            Assert.IsType<CustomsItem>(retrievedCustomsItem);
            Assert.Equal(customsItem, retrievedCustomsItem);
        }

        #endregion

        private async Task<CustomsItem> CreateBasicCustomsItem() => await Client.CustomsItem.Create(Fixtures.BasicCustomsItem);
    }
}
