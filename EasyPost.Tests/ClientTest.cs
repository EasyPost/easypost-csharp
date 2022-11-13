using System.Threading;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class ClientTests : UnitTest
    {
        public ClientTests() : base("client")
        {
        }

        #region Tests

        [Fact]
        [Testing.Function]
        public void TestClient()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            // we specifically want to test the getters/setters
            Client client = new(FakeApikey);
            client.Configuration.ConnectTimeoutMilliseconds = 5000;
            client.Configuration.RequestTimeoutMilliseconds = 5000;

            Assert.Equal(5000, client.Configuration.ConnectTimeoutMilliseconds);
            Assert.Equal(5000, client.Configuration.RequestTimeoutMilliseconds);
        }

        [Fact]
        [Testing.Logic]
        public void TestThreadSafety()
        {
            const string key1 = "key1";
            const string key2 = "key2";
            const string key3 = "key3";

            Client client1 = new(key1);
            Client client2 = new(key2);
            Client client3 = new(key3);

            static void Thread(Client client, string keyToMatch)
            {
                Assert.Equal(keyToMatch, client.Configuration.ApiKey);
            }

            Thread thread1 = new(() => Thread(client1, key1));
            Thread thread2 = new(() => Thread(client2, key2));
            Thread thread3 = new(() => Thread(client3, key3));

            // Start all threads, purposely out of order
            thread2.Start();
            thread3.Start();
            thread1.Start();
        }

        #endregion

        private const string FakeApikey = "fake_api_key";

        [Fact]
        public void TestBaseUrlOverride()
        {
            Client normalClient = new(FakeApikey);
            Client overrideClient = new(FakeApikey, "https://www.example.com");

            Assert.Equal("https://api.easypost.com", normalClient.Configuration.ApiBase);
            Assert.Equal("https://www.example.com", overrideClient.Configuration.ApiBase);
        }
    }
}
