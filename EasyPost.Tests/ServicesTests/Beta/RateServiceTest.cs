using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Calculation;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests.Beta
{
    public class RateServiceTests : UnitTest
    {
        public RateServiceTests() : base("beta_rate_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Dictionary<string, object> data = Fixtures.BasicShipment;

            List<Rate> rates = await Client.Beta.Rate.RetrieveStatelessRates(data);

            Assert.IsType<List<Rate>>(rates);
        }

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestGetLowestRateStaticFunction()
        {
            UseVCR("get_lowest_rate_static_function");

            Dictionary<string, object> data = Fixtures.BasicShipment;

            List<Rate> rates = await Client.Beta.Rate.RetrieveStatelessRates(data);

            Rate lowestRate = Client.Beta.Rate.GetLowestEphemeralRate(rates);

            Assert.Equal("First", lowestRate.Service);
        }

        [Fact]
        [Testing.Function]
        public async Task TestGetLowestRateExtensionFunction()
        {
            UseVCR("get_lowest_rate_extension_function");

            Dictionary<string, object> data = Fixtures.BasicShipment;

            List<Rate> rates = await Client.Beta.Rate.RetrieveStatelessRates(data);

            Rate lowestRate = rates.GetLowest();

            Assert.Equal("First", lowestRate.Service);
        }

        #endregion
    }
}
