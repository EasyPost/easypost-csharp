using Xunit;

namespace EasyPost.Tests
{
    public class EasyPostTest
    {
        private const string FakeApikey = "fake_api_key";

        [Fact]
        public void TestThreadSafety()
        {
            Client client1 = new Client(TestUtils.GetApiKey(TestUtils.ApiKey.Test));
            Client client2 = new Client(TestUtils.GetApiKey(TestUtils.ApiKey.Production));

            // TODO: How do we prove thread safety?
        }

        [Fact]
        public void TestTimeout()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            // we specifically want to test the getters/setters
            Client client = new Client(FakeApikey);
            client.ConnectTimeoutMilliseconds = 5000;
            client.RequestTimeoutMilliseconds = 5000;

            Assert.Equal(5000, client.ConnectTimeoutMilliseconds);
            Assert.Equal(5000, client.RequestTimeoutMilliseconds);
        }
    }
}
