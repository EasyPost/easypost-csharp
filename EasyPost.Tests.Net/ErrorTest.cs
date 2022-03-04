using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class ErrorTest
    {
        private string _error;

        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "error", true);
        }

        [TestMethod]
        public void TestError()
        {
            VCR.Replay("error");

            try
            {
                var _ = Shipment.Create();
            }
            catch (ApiException error)
            {
                Assert.AreEqual(422, error.StatusCode);
                Assert.AreEqual("SHIPMENT.INVALID_PARAMS", error.Code);
                Assert.AreEqual("Unable to create shipment, one or more parameters were invalid.", error.Message);
                Assert.IsTrue(error.ApiError.suggestions.Count == 2);
            }
        }
    }
}
