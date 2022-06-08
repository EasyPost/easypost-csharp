using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.Beta;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class EndShipperTest : UnitTest
    {
        public EndShipperTest() : base("end_shipper", TestUtils.ApiKey.Production)
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Beta);

            List<EndShipper> endShippers = await Client.EndShippers.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            Assert.IsTrue(endShippers.Count <= Fixture.PageSize);
            foreach (EndShipper item in endShippers)
            {
                Assert.IsInstanceOfType(item, typeof(EndShipper));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Beta);

            EndShipper endShipper = await CreateBasicEndShipper();

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.id.StartsWith("es_"));
            Assert.AreEqual("388 TOWNSEND ST APT 20", endShipper.street1);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Beta);

            EndShipper endShipper = await CreateBasicEndShipper();

            EndShipper retrievedEndShipper = await Client.EndShippers.Retrieve(endShipper.id);

            Assert.IsInstanceOfType(retrievedEndShipper, typeof(EndShipper));
            Assert.AreEqual(endShipper.street1, retrievedEndShipper.street1);
        }

        [Fact]
        public async Task TestUpdate()
        {
            UseVCR("update", ApiVersion.Beta);

            EndShipper endShipper = await CreateBasicEndShipper();

            string testName = "NEW NAME";

            Dictionary<string, object> endShipperData = Fixture.EndShipperAddress;
            endShipperData["name"] = testName;

            await endShipper.Update(endShipperData);

            Assert.IsInstanceOfType(endShipper, typeof(EndShipper));
            Assert.IsTrue(endShipper.id.StartsWith("es_"));
            Assert.AreEqual(testName, endShipper.name);
        }

        private async Task<EndShipper> CreateBasicEndShipper() => await Client.EndShippers.Create(Fixture.EndShipperAddress);
    }
}
