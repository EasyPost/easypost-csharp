using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
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

            Dictionary<string, object> data = Fixtures.OneCallBuyShipment;

            Shipment shipment = await Client.Shipment.Create(data);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.PickupRates);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } };

            BetaFeatures.Parameters.Pickups.All parameters = Fixtures.Parameters.Pickups.All(data);

            PickupCollection pickupCollection = await Client.Pickup.All(parameters);

            List<Pickup> pickups = pickupCollection.Pickups;

            Assert.True(pickups.Count <= Fixtures.PageSize);
            foreach (Pickup pickup in pickups)
            {
                Assert.IsType<Pickup>(pickup);
            }
        }

        #endregion

        #endregion
    }
}
