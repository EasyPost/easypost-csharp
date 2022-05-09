using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ErrorTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("error");
        }

        [TestMethod]
        public async Task TestError()
        {
            _vcr.SetUpTest("error");

            try
            {
                var _ = await Shipment.Create();
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
