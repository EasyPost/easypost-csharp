using EasyPost.Clients;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{

    public class EasyPostTest
    {
        private const string FakeApikey = "fake_api_key";

        [Fact]
        public void TestTimeout()
        {
            V2Client client = new V2Client(FakeApikey);
            client.ConnectTimeoutMilliseconds = 5000;
            client.RequestTimeoutMilliseconds = 5000;

            Assert.AreEqual(5000, client.ConnectTimeoutMilliseconds);
            Assert.AreEqual(5000, client.RequestTimeoutMilliseconds);
        }
    }
}
