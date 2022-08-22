using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class EndShipperTest : UnitTest
    {
        public EndShipperTest() : base("end_shipper", TestUtils.ApiKey.Production)
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            EndShipper endShipper = await CreateBasicEndShipper();

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.id);
            Assert.Equal("388 TOWNSEND ST APT 20", endShipper.street1);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            List<EndShipper> endShippers = await Client.EndShipper.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });
            Assert.True(endShippers.Count <= Fixture.PageSize);
            foreach (EndShipper item in endShippers)
            {
                Assert.IsType<EndShipper>(item);
            }
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            EndShipper endShipper = await CreateBasicEndShipper();

            EndShipper retrievedEndShipper = await Client.EndShipper.Retrieve(endShipper.id);

            Assert.IsType<EndShipper>(retrievedEndShipper);
            Assert.Equal(endShipper.street1, retrievedEndShipper.street1);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestUpdate()
        {
            UseVCR("update");

            EndShipper endShipper = await CreateBasicEndShipper();

            string testName = "NEW NAME";

            Dictionary<string, object> endShipperData = Fixture.EndShipperAddress;
            endShipperData["name"] = testName;

            endShipper = await endShipper.Update(endShipperData);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.id);
            Assert.Equal(testName, endShipper.name);
        }

        #endregion

        private async Task<EndShipper> CreateBasicEndShipper() => await Client.EndShipper.Create(Fixture.EndShipperAddress);
    }
}
