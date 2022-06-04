using System.Threading.Tasks;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class ErrorTest : UnitTest
    {
        public ErrorTest() : base("error")
        {
        }

        [Fact]
        public async Task TestError()
        {
            UseVCR("error");

            try
            {
                Shipment _ = await V2Client.Shipments.Create();
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
