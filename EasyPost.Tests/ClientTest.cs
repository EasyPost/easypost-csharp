using System;
using System.Net.Http;
using System.Threading;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
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
            const string apiBase = "https://www.example.com";
            TimeSpan timeout = TimeSpan.FromMilliseconds(5000);
            HttpClient httpClient = new();

            Client client = new(new ClientConfiguration(FakeApikey)
            {
                ApiBase = apiBase,
                Timeout = timeout,
                CustomHttpClient = httpClient,
            });

            Assert.Equal(FakeApikey, client.ApiKeyInUse);
            Assert.Equal(apiBase, client.ApiBaseInUse);
            Assert.Equal(timeout, client.Timeout);
            Assert.Equal(httpClient, client.CustomHttpClient);
        }

        [Fact]
        [Testing.Logic]
        public void TestThreadSafety()
        {
            const string key1 = "key1";
            const string key2 = "key2";
            const string key3 = "key3";

            Client client1 = new(new ClientConfiguration(key1));
            Client client2 = new(new ClientConfiguration(key2));
            Client client3 = new(new ClientConfiguration(key3));

            static void Thread(Client client, string keyToMatch) => Assert.Equal(keyToMatch, client.ApiKeyInUse);

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
            Client normalClient = new(new ClientConfiguration(FakeApikey));
            Client overrideClient = new(new ClientConfiguration(FakeApikey)
            {
                ApiBase = "https://www.example.com",
            });

            Assert.Equal("https://api.easypost.com", normalClient.ApiBaseInUse);
            Assert.Equal("https://www.example.com", overrideClient.ApiBaseInUse);
        }

        [Fact]
        public void TestTimeoutOverride()
        {
            Client normalClient = new(new ClientConfiguration(FakeApikey));
            Client overrideClient = new(new ClientConfiguration(FakeApikey)
            {
                Timeout = TimeSpan.FromMilliseconds(1),
            });

            Assert.Equal(TimeSpan.FromSeconds(60), normalClient.Timeout);
            Assert.Equal(TimeSpan.FromMilliseconds(1), overrideClient.Timeout);
        }

        [Fact]
        public void TestHttpClientOverride()
        {
            Client normalClient = new(new ClientConfiguration(FakeApikey));

            HttpClient httpClient = new();
            Client overrideClient = new(new ClientConfiguration(FakeApikey)
            {
                CustomHttpClient = httpClient,
            });

            Assert.NotEqual(httpClient, normalClient.CustomHttpClient);
            Assert.Equal(httpClient, overrideClient.CustomHttpClient);
        }
    }
}
