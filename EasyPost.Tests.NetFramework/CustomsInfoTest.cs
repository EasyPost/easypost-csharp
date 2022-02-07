using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class CustomsInfoTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        private static CustomsInfo CreateBasicCustomsInfo()
        {
            return CustomsInfo.Create(Fixture.BasicCustomsInfo);
        }

        [TestMethod]
        public void TestCreate()
        {
            CustomsInfo customsInfo = CreateBasicCustomsInfo();

            Assert.IsInstanceOfType(customsInfo, typeof(CustomsInfo));
            Assert.IsTrue(customsInfo.id.StartsWith("cstinfo_"));
            Assert.AreEqual("NOEEI 30.37(a)", customsInfo.eel_pfc);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            CustomsInfo customsInfo = CreateBasicCustomsInfo();

            CustomsInfo retrievedCustomsInfo = CustomsInfo.Retrieve(customsInfo.id);

            Assert.IsInstanceOfType(retrievedCustomsInfo, typeof(CustomsInfo));
            Assert.AreEqual(customsInfo.id, retrievedCustomsInfo.id);
        }
    }
}
