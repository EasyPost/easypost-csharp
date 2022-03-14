using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class CustomsItemTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "customs_item", true);
        }

        private static async Task<CustomsItem> CreateBasicCustomsItem()
        {
            return await CustomsItem.Create(Fixture.BasicCustomsItem);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            CustomsItem customsItem = await CreateBasicCustomsItem();

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.value);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            CustomsItem customsItem = await CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = await CustomsItem.Retrieve(customsItem.id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem.id, retrievedCustomsItem.id);
        }
    }
}
