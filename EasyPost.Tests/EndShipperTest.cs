using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API.Beta;
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
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", endShipper.Street1);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            List<EndShipper> endShippers = await Client.Beta.EndShipper.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });
            Assert.True(endShippers.Count <= Fixtures.PageSize);
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

            EndShipper retrievedEndShipper = await Client.Beta.EndShipper.Retrieve(endShipper.Id);

            Assert.IsType<EndShipper>(retrievedEndShipper);
            Assert.Equal(endShipper.Street1, retrievedEndShipper.Street1);
        }

        [Fact]
        [CrudOperations.Update]
        public async Task TestUpdate()
        {
            UseVCR("update");

            EndShipper endShipper = await CreateBasicEndShipper();

            string testName = "NEW NAME";

            Dictionary<string, object> endShipperData = Fixtures.CaAddress1;
            endShipperData["name"] = testName;

            endShipper = await endShipper.Update(endShipperData);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal(testName, endShipper.Name);
        }

        #endregion

        private async Task<EndShipper> CreateBasicEndShipper() => await Client.Beta.EndShipper.Create(Fixtures.CaAddress1);
    }
}
