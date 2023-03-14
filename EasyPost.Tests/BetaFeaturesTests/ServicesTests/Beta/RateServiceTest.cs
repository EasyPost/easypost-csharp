using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests.Beta
{
    public class RateServiceTests : UnitTest
    {
        public RateServiceTests() : base("beta_rate_service_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestGetLowestRateStaticFunction()
        {
            UseVCR("get_lowest_rate_static_function");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(shipmentData);

            StatelessRate lowestStatelessRate = Utilities.Rates.GetLowestStatelessRate(rates);

            Assert.Equal("First", lowestStatelessRate.Service);
        }

        [Fact]
        [Testing.Function]
        public async Task TestGetLowestRateExtensionFunction()
        {
            UseVCR("get_lowest_rate_extension_function");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(shipmentData);

            StatelessRate lowestStatelessRate = rates.GetLowest();

            Assert.Equal("First", lowestStatelessRate.Service);
        }

        #endregion
    }
}
