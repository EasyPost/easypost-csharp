
using Xunit;

namespace EasyPost.Tests
{
    public class EasyPostTest
    {
        private const string FakeApikey = "fake_api_key";

        [Fact]
        public void TestTimeout()
        {
            Client client = new Client(FakeApikey);
            client.ConnectTimeoutMilliseconds = 5000;
            client.RequestTimeoutMilliseconds = 5000;

            Assert.Equal(5000, client.ConnectTimeoutMilliseconds);
            Assert.Equal(5000, client.RequestTimeoutMilliseconds);
        }
    }
}
