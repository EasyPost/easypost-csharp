using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Beta;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class EndShipperTest
    {
        private TestUtils.VCR _vcr;

        // This (and all Beta features) cannot use VCR because of a known conflict between using a VCR/custom HttpClient and re-constructing RestSharp.

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("end_shipper", TestUtils.ApiKey.Production);
        }

        [Ignore]
        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

            List<EndShipper> endShippers = await EndShipper.All(new Dictionary<string, object>
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

        [Ignore]
        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            EndShipper endShipper = await CreateBasicEndShipper();

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.id.StartsWith("es_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", endShipper.street1);
        }

        [Ignore]
        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");

            EndShipper endShipper = await CreateBasicEndShipper();

            EndShipper retrievedEndShipper = await EndShipper.Retrieve(endShipper.id);

            Assert.IsInstanceOfType(retrievedEndShipper, typeof(EndShipper));
            Assert.AreEqual(endShipper.street1, retrievedEndShipper.street1);
        }

        [Ignore]
        [TestMethod]
        public async Task TestUpdate()
        {
            _vcr.SetUpTest("update");

            EndShipper endShipper = await CreateBasicEndShipper();

            string newPhoneNumber = "9999999999";

            Dictionary<string, object> endShipperData = Fixture.EndShipperAddress;
            endShipperData["phone"] = newPhoneNumber;

            await endShipper.Update(endShipperData);

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.id.StartsWith("es_"));
            Assert.AreEqual(newPhoneNumber, endShipper.phone);
        }

        private static async Task<EndShipper> CreateBasicEndShipper()
        {
            return await EndShipper.Create(Fixture.EndShipperAddress);
        }
    }
}
