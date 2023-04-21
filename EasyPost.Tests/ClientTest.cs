using System.Threading;
using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Models.API;
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

            static void Thread(Client client, string keyToMatch) => Assert.Equal(keyToMatch, client.Configuration.ApiKey);

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
        [Testing.Parameters]
        public void TestBaseUrlOverride()
        {
            Client normalClient = new(FakeApikey);
            Client overrideClient = new(new ClientConfiguration(FakeApikey)
            {
                ApiBase = "https://www.example.com",
            });

            Assert.Equal("https://api.easypost.com", normalClient.ApiBaseInUse);
            Assert.Equal("https://www.example.com", overrideClient.ApiBaseInUse);
        }

        [Fact]
        [Testing.Parameters]
        public void TestConnectionTimeoutOverride()
        {
            Client normalClient = new(FakeApikey);
            Client overrideClient = new(new ClientConfiguration(FakeApikey)
            {
                ConnectTimeoutMilliseconds = 50,
            });

            Assert.Equal(30000, normalClient.ConnectTimeoutMillisecondsInUse); // 30 second default
            Assert.Equal(50, overrideClient.ConnectTimeoutMillisecondsInUse);
        }
    }
}
