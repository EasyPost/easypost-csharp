using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CustomsItemTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("customs_item");
        }

        private static async Task<CustomsItem> CreateBasicCustomsItem(V2Client v2Client)
        {
            return await v2Client.CustomsItems.Create(Fixture.BasicCustomsItem);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client v2Client = _vcr.SetUpTest("create");

            CustomsItem customsItem = await CreateBasicCustomsItem(v2Client);

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.value);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");

            CustomsItem customsItem = await CreateBasicCustomsItem(v2Client);

            CustomsItem retrievedCustomsItem = await v2Client.CustomsItems.Retrieve(customsItem.id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem, retrievedCustomsItem);
        }
    }
}
