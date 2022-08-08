using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Tests
{
    public class CustomsItemTest : UnitTest
    {
        public CustomsItemTest() : base("customs_item")
        {
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            CustomsItem customsItem = await CreateBasicCustomsItem();

            Assert.IsType<CustomsItem>(customsItem);
            Assert.StartsWith("cstitem_", customsItem.id);
            Assert.Equal(23.0, customsItem.value);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CustomsItem customsItem = await CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = await Client.CustomsItem.Retrieve(customsItem.id);

            Assert.IsType<CustomsItem>(retrievedCustomsItem);
            Assert.Equal(customsItem, retrievedCustomsItem);
        }

        private async Task<CustomsItem> CreateBasicCustomsItem() => await Client.CustomsItem.Create(Fixture.BasicCustomsItem);
    }
}
