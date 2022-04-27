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
            V2Client v2Client = new V2Client(FakeApikey);
            v2Client.ConnectTimeoutMilliseconds = 5000;
            v2Client.RequestTimeoutMilliseconds = 5000;

            Assert.AreEqual(5000, v2Client.ConnectTimeoutMilliseconds);
            Assert.AreEqual(5000, v2Client.RequestTimeoutMilliseconds);
        }
    }
}
