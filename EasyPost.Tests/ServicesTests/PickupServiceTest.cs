using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class PickupServiceTests : UnitTest
    {
        public PickupServiceTests() : base("pickup_service")
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

            Pickup pickup = await CreateBasicPickup();

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.PickupRates);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Pickup pickup = await CreateBasicPickup();

            Pickup retrievedPickup = await Client.Pickup.Retrieve(pickup.Id);

            Assert.IsType<Pickup>(retrievedPickup);
            Assert.Equal(pickup.Id, retrievedPickup.Id);
        }

        #endregion

        #endregion

        private async Task<Pickup> CreateBasicPickup()
        {
            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;
            return await Client.Pickup.Create(pickupData);
        }
    }
}
