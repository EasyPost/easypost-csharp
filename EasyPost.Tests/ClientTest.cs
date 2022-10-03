using System.Threading;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Tests
{
    public class ClientTest : UnitTest
    {
        private const string FakeApikey = "fake_api_key";

        public ClientTest() : base("client")
        {
        }

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

        [Fact]
        public async Task TestClientPassing()
        {
            UseVCR("client_passing");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);

            // shipment should exist
            Assert.NotNull(shipment);
            // shipment client should have been populated
            Assert.NotNull(shipment.Client);
            // shipment client should be the same as the client used to create the shipment
            Assert.Equal(Client, shipment.Client);

            // shipment from address should exist
            Assert.NotNull(shipment.FromAddress);
            // shipment from address client should have been populated
            Assert.NotNull(shipment.FromAddress.Client);
            // shipment from address client should be the same as the client used to create the shipment
            Assert.Equal(Client, shipment.FromAddress.Client);

            // shipment to address should exist
            Assert.NotNull(shipment.ToAddress);
            // shipment to address client should have been populated
            Assert.NotNull(shipment.ToAddress.Client);
            // shipment to address client should be the same as the client used to create the shipment
            Assert.Equal(Client, shipment.ToAddress.Client);

            // shipment parcel should exist
            Assert.NotNull(shipment.Parcel);
            // shipment parcel client should have been populated
            Assert.NotNull(shipment.Parcel.Client);
            // shipment parcel client should be the same as the client used to create the shipment
            Assert.Equal(Client, shipment.Parcel.Client);
        }
    }
}
