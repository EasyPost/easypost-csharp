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
        public PickupServiceTests() : base("pickup_service_with_parameters")
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

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            BetaFeatures.Parameters.Pickups.Create pickupParameters = Fixtures.Parameters.Pickups.Create(pickupData);

            Pickup pickup = await Client.Pickup.Create(pickupParameters);

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

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Dictionary<string, object> shipmentData = Fixtures.OneCallBuyShipment;

            BetaFeatures.Parameters.Shipments.Create shipmentCreateParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentCreateParameters);

            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            BetaFeatures.Parameters.Pickups.Create pickupCreateParameters = Fixtures.Parameters.Pickups.Create(pickupData);

            Pickup pickup = await Client.Pickup.Create(pickupCreateParameters);

            BetaFeatures.Parameters.Pickups.Buy pickupBuyParameters = new BetaFeatures.Parameters.Pickups.Buy(Fixtures.Usps, Fixtures.PickupService);

            pickup = await Client.Pickup.Buy(pickup.Id, pickupBuyParameters);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.Confirmation);
            Assert.Equal("scheduled", pickup.Status);
        }

        #endregion

        #endregion
    }
}
