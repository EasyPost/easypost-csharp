using System.Threading;
using Xunit;

namespace EasyPost.Tests
{
    public class ClientTest
    {
        private const string FakeApikey = "fake_api_key";

        [Fact]
        public void TestBaseUrlOverride()
        {
            Client normalClient = new Client(FakeApikey);
            Client overrideClient = new Client(FakeApikey, "https://www.example.com");

            Assert.Equal("https://api.easypost.com", normalClient.Configuration.ApiBase);
            Assert.Equal("https://www.example.com", overrideClient.Configuration.ApiBase);
        }

        [Fact]
        public void TestThreadSafety()
        {
            const string key1 = "key1";
            const string key2 = "key2";
            const string key3 = "key3";

            Client client1 = new Client(key1);
            Client client2 = new Client(key2);
            Client client3 = new Client(key3);

            static void TestThread(Client client, string keyToMatch)
            {
                Assert.Equal(keyToMatch, client.Configuration.ApiKey);
            }

            Thread thread1 = new Thread(() => TestThread(client1, key1));
            Thread thread2 = new Thread(() => TestThread(client2, key2));
            Thread thread3 = new Thread(() => TestThread(client3, key3));

            // Start all threads, purposely out of order
            thread2.Start();
            thread3.Start();
            thread1.Start();
        }

        [Fact]
        public void TestTimeout()
        {
            // ReSharper disable once UseObjectOrCollectionInitializer
            // we specifically want to test the getters/setters
            Client client = new Client(FakeApikey);
            client.Configuration.ConnectTimeoutMilliseconds = 5000;
            client.Configuration.RequestTimeoutMilliseconds = 5000;

            Assert.Equal(5000, client.Configuration.ConnectTimeoutMilliseconds);
            Assert.Equal(5000, client.Configuration.RequestTimeoutMilliseconds);
        }
    }
}
