using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
            UseVCR("create", ApiVersion.Latest);

            CustomsItem customsItem = await CreateBasicCustomsItem();

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.Id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.Value);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            CustomsItem customsItem = await CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = await Client.CustomsItems.Retrieve(customsItem.Id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem, retrievedCustomsItem);
        }

        private async Task<CustomsItem> CreateBasicCustomsItem() => await Client.CustomsItems.Create(Fixture.BasicCustomsItem);
    }
}
