using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
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

            CustomsItem customsItem = await Client.CreateBasicCustomsItem();

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.Id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.Value);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            CustomsItem customsItem = await Client.CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = await Client.CustomsItems.Retrieve(customsItem.Id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem, retrievedCustomsItem);
        }
    }
}
