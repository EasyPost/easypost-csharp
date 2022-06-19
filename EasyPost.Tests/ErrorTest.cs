using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
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
            UseVCR("error", ApiVersion.Latest);

            try
            {
                Shipment _ = await Client.Shipments.Create();
            }
            catch (ApiException error)
            {
                Assert.AreEqual(422, error.StatusCode);
                Assert.AreEqual("SHIPMENT.INVALID_PARAMS", error.ApiCode);
                Assert.AreEqual("Unable to create shipment, one or more parameters were invalid.", error.Message);
                Assert.IsTrue(error.ApiErrors != null && error.ApiErrors.Count == 2);
            }
        }
    }
}
