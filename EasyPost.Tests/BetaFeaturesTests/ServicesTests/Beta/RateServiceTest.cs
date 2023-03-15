using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
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
        public async Task TestGetRates()
        {
            UseVCR("get_rates");

            Dictionary<string, object> data = Fixtures.BasicShipment;

            BetaFeatures.Parameters.Beta.Rates.Retrieve parameters = Fixtures.Parameters.Rates.RetrieveBeta(data);

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(parameters);

            Assert.NotNull(rates);
            Assert.NotEmpty(rates);
        }

        #endregion
    }
}
