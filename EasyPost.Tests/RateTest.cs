using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class RateTest : UnitTest
    {
        public RateTest() : base("rate")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Read]
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
    }
}
