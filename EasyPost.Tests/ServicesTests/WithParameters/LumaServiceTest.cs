using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters
{
    public class LumaServiceTests : UnitTest
    {
        public LumaServiceTests() : base("luma_service_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [Testing.Function]
        public async Task TestGetPromise()
        {
            UseVCR("get_promise");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;
            Parameters.Luma.GetPromise parameters = Fixtures.Parameters.Luma.GetPromise(shipmentData);

            LumaInfo response = await Client.Luma.GetPromise(parameters);

            Assert.NotNull(response.LumaSelectedRate);
        }

        #endregion

        #endregion
    }
}
