using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities;
using EasyPost.Utilities.Internal.Attributes;
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
        public async Task TestRetrieveStatelessRates()
        {
            UseVCR("retrieve_stateless_rates");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(shipmentData);

            Assert.NotNull(rates);
            Assert.NotEmpty(rates);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieveStatelessRatesWithCarrierAccount()
        {
            UseVCR("retrieve_stateless_rates_with_carrier_account");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;

            shipmentData.Add("carrier_accounts", new List<string> { Fixtures.UspsCarrierAccountId });

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(shipmentData);

            Assert.NotNull(rates);
            Assert.NotEmpty(rates);
        }

        #endregion

        [Fact]
        [Testing.Function]
        public async Task TestGetLowestRateStaticFunction()
        {
            UseVCR("get_lowest_rate_static_function");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(shipmentData);

            StatelessRate lowestStatelessRate = Rates.GetLowestStatelessRate(rates);

            Assert.Equal("GroundAdvantage", lowestStatelessRate.Service);
        }

        [Fact]
        [Testing.Function]
        public async Task TestGetLowestRateExtensionFunction()
        {
            UseVCR("get_lowest_rate_extension_function");

            Dictionary<string, object> shipmentData = Fixtures.BasicShipment;

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(shipmentData);

            StatelessRate lowestStatelessRate = rates.GetLowest();

            Assert.Equal("GroundAdvantage", lowestStatelessRate.Service);
        }

        #endregion
    }
}
