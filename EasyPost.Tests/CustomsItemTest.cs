using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.V2;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CustomsItemTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize() => _vcr = new TestUtils.VCR("customs_item");

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("create");

            CustomsItem customsItem = await CreateBasicCustomsItem(client);

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.value);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("retrieve");

            CustomsItem customsItem = await CreateBasicCustomsItem(client);

            CustomsItem retrievedCustomsItem = await client.CustomsItems.Retrieve(customsItem.id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem, retrievedCustomsItem);
        }

        private static async Task<CustomsItem> CreateBasicCustomsItem(V2Client client) => await client.CustomsItems.Create(Fixture.BasicCustomsItem);
    }
}
