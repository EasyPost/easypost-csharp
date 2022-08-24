using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace EasyPost.Tests
{
    public class ErrorTest : UnitTest
    {
        public ErrorTest() : base("error")
        {
        }

        [Fact]
        public async Task TestBadParameters()
        {
            UseVCR("bad_parameters");

            try
            {
                var _ = await Client.Shipment.Create(new Dictionary<string, object>());
            }
            catch (HttpException error)
            {
                Assert.Equal(422, error.StatusCode);
                Assert.Equal("PARAMETER.REQUIRED", error.Code);
                Assert.Equal("Missing required parameter.", error.Message);
                Assert.True(error.Errors.Count == 1);
            }
        }

        [Fact(Skip = "Test is no longer valid")]
        public async Task TestEmptyApiKey()
        {
            // No longer possible to have an empty API key
        }
    }
}
