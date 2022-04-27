using System.Dynamic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Http;
using EasyPost.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace EasyPost.Tests
{
    [TestClass]
    public class EasyPostTest
    {
        private const string FakeApikey = "fake_api_key";

        [TestMethod]
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
