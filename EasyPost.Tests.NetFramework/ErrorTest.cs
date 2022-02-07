using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class ErrorTest
    {
        private string _error;

        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        [TestMethod]
        public void TestError()
        {
            try
            {
                var _ = Shipment.Create();
            }
            catch (HttpException error)
            {
                Assert.AreEqual(422, error.StatusCode);
                Assert.AreEqual("SHIPMENT.INVALID_PARAMS", error.Code);
                Assert.AreEqual("Unable to create shipment, one or more parameters were invalid.", error.Message);
                Assert.IsTrue(error.Errors.Count == 2);
            }
        }
    }
}
