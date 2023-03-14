using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class EndShipperServiceTests : UnitTest
    {
        public EndShipperServiceTests() : base("end_shipper_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Dictionary<string, object> data = Fixtures.CaAddress1;

            BetaFeatures.Parameters.EndShippers.Create parameters = Fixtures.Parameters.EndShippers.Create(data);

            EndShipper endShipper = await Client.EndShipper.Create(parameters);

            Assert.IsType<EndShipper>(endShipper);
            Assert.StartsWith("es_", endShipper.Id);
            Assert.Equal("388 TOWNSEND ST APT 20", endShipper.Street1);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.EndShippers.All parameters = Fixtures.Parameters.EndShippers.All(data);

            EndShipperCollection endShipperCollection = await Client.EndShipper.All(parameters);
            List<EndShipper> endShippers = endShipperCollection.EndShippers;

            Assert.True(endShippers.Count <= Fixtures.PageSize);
            foreach (EndShipper item in endShippers)
            {
                Assert.IsType<EndShipper>(item);
            }
        }

        #endregion

        #endregion
    }
}
