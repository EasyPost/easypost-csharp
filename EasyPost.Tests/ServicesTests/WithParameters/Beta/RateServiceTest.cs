using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API.Beta;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests.WithParameters.Beta
{
    public class RateServiceTests : UnitTest
    {
        public RateServiceTests() : base("beta_rate_service_with_parameters")
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

            Dictionary<string, object> data = Fixtures.BasicShipment;

            Parameters.Beta.Rate.Retrieve parameters = Fixtures.Parameters.Rates.RetrieveBeta(data);

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(parameters);

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

            Parameters.Beta.Rate.Retrieve parameters = Fixtures.Parameters.Rates.RetrieveBeta(shipmentData);

            parameters.CarrierAccounts = new List<Models.API.CarrierAccount>
            {
                new Models.API.CarrierAccount
                {
                    Id = Fixtures.UspsCarrierAccountId,
                }
            };

            List<StatelessRate> rates = await Client.Beta.Rate.RetrieveStatelessRates(parameters);

            Assert.NotNull(rates);
            Assert.NotEmpty(rates);
        }

        #endregion

        #endregion
    }
}
