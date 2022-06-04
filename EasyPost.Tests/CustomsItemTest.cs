using System.Threading.Tasks;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{

    public class CustomsItemTest : UnitTest
    {
        public CustomsItemTest() : base("customs_item", TestUtils.ApiKey.Test)
        {
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            CustomsItem customsItem = await CreateBasicCustomsItem();

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.value);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            CustomsItem customsItem = await CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = await V2Client.CustomsItems.Retrieve(customsItem.id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem, retrievedCustomsItem);
        }

        private async Task<CustomsItem> CreateBasicCustomsItem() => await V2Client.CustomsItems.Create(Fixture.BasicCustomsItem);
    }
}
