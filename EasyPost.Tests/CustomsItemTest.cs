using System.Threading.Tasks;
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
            _vcr.SetUpTest("create");

            CustomsItem customsItem = await CreateBasicCustomsItem();

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.value);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");


            CustomsItem customsItem = await CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = await CustomsItem.Retrieve(customsItem.id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem, retrievedCustomsItem);
        }

        private static async Task<CustomsItem> CreateBasicCustomsItem() => await CustomsItem.Create(Fixture.BasicCustomsItem);
    }
}
