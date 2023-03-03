using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Internal.Annotations;
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

        [Fact]
        [Testing.Function]
        public void TestGetLowestRate()
        {
            UseVCR("get_lowest_rate");

            List<Rate> rates = new()
            {
                new Rate { Price = "100.00" },
                new Rate { Price = "1.00" },
            };

            Rate lowestRate = Client.Rate.GetLowestRate(rates);
            Assert.Equal("1.00", lowestRate.Price);
        }

        #endregion
    }
}
