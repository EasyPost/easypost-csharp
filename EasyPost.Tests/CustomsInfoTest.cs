using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class CustomsInfoTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("customs_info");
        }

        private static async Task<CustomsInfo> CreateBasicCustomsInfo(V2Client client)
        {
            return await client.CustomsInfo.Create(Fixture.BasicCustomsInfo);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("create");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo(client);

            Assert.IsInstanceOfType(customsInfo, typeof(CustomsInfo));
            Assert.IsTrue(customsInfo.id.StartsWith("cstinfo_"));
            Assert.AreEqual("NOEEI 30.37(a)", customsInfo.eel_pfc);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("retrieve");

            CustomsInfo customsInfo = await CreateBasicCustomsInfo(client);

            CustomsInfo retrievedCustomsInfo = await client.CustomsInfo.Retrieve(customsInfo.id);

            Assert.IsInstanceOfType(retrievedCustomsInfo, typeof(CustomsInfo));
            Assert.AreEqual(customsInfo, retrievedCustomsInfo);
        }
    }
}
