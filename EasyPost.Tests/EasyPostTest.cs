using System.Dynamic;
using System.Threading.Tasks;
using EasyPost.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace EasyPost.Tests
{
    [TestClass]
    public class EasyPostTest
    {
        private const string FakeApikey = "fake_api_key";
        private const string FakeApiUrl = "https://fake.api.com";
        private const string HttpBinUrl = "https://httpbin.org/get";

        [TestMethod]
        public void TestApiKeyConstructor()
        {
            ClientConfiguration config = GetBasicClientConfiguration();

            Assert.AreEqual(FakeApikey, config.ApiKey);
            Assert.AreEqual("https://api.easypost.com/v2", config.ApiBase);
        }

        [TestMethod]
        public void TestApiKeyPlusBaseUrlConstructor()
        {
            ClientConfiguration config = new ClientConfiguration(FakeApikey, FakeApiUrl);

            Assert.AreEqual(FakeApikey, config.ApiKey);
            Assert.AreEqual(FakeApiUrl, config.ApiBase);
        }

        [TestMethod]
        public async Task TestClientManagerGetCurrent()
        {
            ClientManager.SetCurrent(delegate { return new Client(new ClientConfiguration(FakeApikey, HttpBinUrl)); });

            // Client should now be configured to hit httpbin.org instead of EasyPost's API

            Request request = new Request("", Method.Get);
            ExpandoObject response = await request.Execute<ExpandoObject>();
            Assert.AreEqual(HttpBinUrl, JsonSerialization.GetValueOfExpandoObjectProperty(response, "url")?.ToString());
        }

        [TestMethod]
        public void TestTimeout()
        {
            Client client = new Client(GetBasicClientConfiguration());
            client.ConnectTimeoutMilliseconds = 5000;
            client.RequestTimeoutMilliseconds = 5000;

            Assert.AreEqual(5000, client.ConnectTimeoutMilliseconds);
            Assert.AreEqual(5000, client.RequestTimeoutMilliseconds);
        }

        private static ClientConfiguration GetBasicClientConfiguration() => new ClientConfiguration(FakeApikey);
    }
}
