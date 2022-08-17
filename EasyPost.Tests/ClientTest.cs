using System;
using System.Net;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Tests
{
    public class ClientTest
    {
        private const string FakeApikey = "fake_api_key";


        [Fact(Skip = "Test requires credentials. Test locally only.")]
        public async Task TestProxy()
        {
            string apiKey = TestUtils.GetApiKey(TestUtils.ApiKey.Test);

            WebProxy proxy = new WebProxy
            {
                Address = new Uri("socks5://109.201.152.174:1080"), // the Private Internet Access proxy: https://helpdesk.privateinternetaccess.com/kb/articles/do-you-offer-a-socks5-proxy
                Credentials = null // add your credentials here in a NetworkCredential object
            };

            Client client = new Client(apiKey, proxy);

            AddressCollection addressCollection = await client.Address.All();
            Assert.NotNull(addressCollection);
        }

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
