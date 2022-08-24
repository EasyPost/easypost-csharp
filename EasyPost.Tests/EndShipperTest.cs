using System.Collections.Generic;
using System.Threading.Tasks;
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

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            EndShipperCollection endShipperCollection = await EndShipper.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<EndShipper> endShippers = endShipperCollection.end_shippers;

            Assert.IsTrue(endShippers.Count <= Fixture.PageSize);
            Assert.IsNotNull(endShipperCollection.has_more);
            foreach (var item in endShippers)
            {
                Assert.IsInstanceOfType(item, typeof(EndShipper));
            }
        }

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            EndShipper endShipper = await CreateBasicEndShipper();

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.id.StartsWith("es_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", endShipper.street1);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");

            EndShipper endShipper = await CreateBasicEndShipper();

            EndShipper retrievedEndShipper = await EndShipper.Retrieve(endShipper.id);

            Assert.IsInstanceOfType(retrievedEndShipper, typeof(EndShipper));
            Assert.AreEqual(endShipper.street1, retrievedEndShipper.street1);
        }

        [TestMethod]
        public async Task TestUpdate()
        {
            _vcr.SetUpTest("update");

            EndShipper endShipper = await CreateBasicEndShipper();

            string newName = "NEW NAME"; // purposely all caps since the API validation will capitalize it in the reponse

            Dictionary<string, object> endShipperData = Fixture.EndShipperAddress;
            endShipperData["name"] = newName;

            await endShipper.Update(endShipperData);

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.id.StartsWith("es_"));
            Assert.AreEqual(newName, endShipper.name);
        }

        private static async Task<EndShipper> CreateBasicEndShipper()
        {
            return await EndShipper.Create(Fixture.EndShipperAddress);
        }
    }
}
