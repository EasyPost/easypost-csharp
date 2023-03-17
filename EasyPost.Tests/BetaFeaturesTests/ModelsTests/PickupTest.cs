using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ModelsTests
{
    public class PickupTests : UnitTest
    {
        public PickupTests() : base("pickup_with_parameters")
        {
        }

        #region Tests

        #region Test CRUD Operations

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

            pickup = await pickup.Buy(pickupBuyParameters);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.Confirmation);
            Assert.Equal("scheduled", pickup.Status);
        }

        #endregion

        #endregion
    }
}
