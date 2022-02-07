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

        private static CustomsItem CreateBasicCustomsItem()
        {
            return CustomsItem.Create(Fixture.BasicCustomsItem);
        }

        [TestMethod]
        public void TestCreate()
        {
            VCR.Replay("create");

            CustomsItem customsItem = CreateBasicCustomsItem();

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.value);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");


            CustomsItem customsItem = CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = CustomsItem.Retrieve(customsItem.id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem.id, retrievedCustomsItem.id);
        }
    }
}
