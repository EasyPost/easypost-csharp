using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.Beta;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class EndShipperTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("end_shipper", TestUtils.ApiKey.Production);
        }

        private static async Task<EndShipper> CreateBasicEndShipper(BetaClient client)
        {
            return await client.EndShippers.Create(Fixture.EndShipperAddress);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            BetaClient client = (BetaClient)_vcr.SetUpTest("create", null, ClientVersion.Beta);

            EndShipper endShipper = await CreateBasicEndShipper(client);

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.id.StartsWith("es_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", endShipper.street1);
        }

        [TestMethod]
        public async Task TestAll()
        {
            BetaClient client = (BetaClient)_vcr.SetUpTest("all", null, ClientVersion.Beta);

            List<EndShipper> endShippers = await client.EndShippers.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            Assert.IsTrue(endShippers.Count <= Fixture.PageSize);
            foreach (var item in endShippers)
            {
                Assert.IsInstanceOfType(item, typeof(EndShipper));
            }
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            BetaClient client = (BetaClient)_vcr.SetUpTest("retrieve", null, ClientVersion.Beta);

            EndShipper endShipper = await CreateBasicEndShipper(client);

            EndShipper retrievedEndShipper = await client.EndShippers.Retrieve(endShipper.id);

            Assert.IsInstanceOfType(retrievedEndShipper, typeof(EndShipper));
            Assert.AreEqual(endShipper.street1, retrievedEndShipper.street1);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            BetaClient client = (BetaClient)_vcr.SetUpTest("update", null, ClientVersion.Beta);

            EndShipper endShipper = await CreateBasicEndShipper(client);

            string newPhoneNumber = "9999999999";

            Dictionary<string, object> endShipperData = Fixture.EndShipperAddress;
            endShipperData["phone"] = newPhoneNumber;

            await endShipper.Update(endShipperData);

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.id.StartsWith("es_"));
            Assert.AreEqual(newPhoneNumber, endShipper.phone);
        }
    }
}
