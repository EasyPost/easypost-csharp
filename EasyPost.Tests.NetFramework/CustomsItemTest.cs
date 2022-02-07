using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class CustomsItemTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        private static CustomsItem CreateBasicCustomsItem()
        {
            return CustomsItem.Create(Fixture.BasicCustomsItem);
        }

        [TestMethod]
        public void TestCreate()
        {
            CustomsItem customsItem = CreateBasicCustomsItem();

            Assert.IsInstanceOfType(customsItem, typeof(CustomsItem));
            Assert.IsTrue(customsItem.id.StartsWith("cstitem_"));
            Assert.AreEqual(23.0, customsItem.value);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            CustomsItem customsItem = CreateBasicCustomsItem();

            CustomsItem retrievedCustomsItem = CustomsItem.Retrieve(customsItem.id);

            Assert.IsInstanceOfType(retrievedCustomsItem, typeof(CustomsItem));
            Assert.AreEqual(customsItem.id, retrievedCustomsItem.id);
        }
    }
}
