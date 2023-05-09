using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class RateServiceTests : UnitTest
    {
        public RateServiceTests() : base("rate_service")
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

            Shipment shipment = await Client.Shipment.Create(Fixtures.BasicShipment);

            Rate rate = await Client.Rate.Retrieve(shipment.Rates[0].Id);

            Assert.IsType<Rate>(rate);
            Assert.StartsWith("rate_", rate.Id);
            Assert.Equal(shipment.Rates[0], rate);
        }

        #endregion

        #endregion
    }
}
