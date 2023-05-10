using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Parameters.Shipment;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;
using All = EasyPost.Parameters.Pickup.All;
using Buy = EasyPost.Parameters.Pickup.Buy;

namespace EasyPost.Tests.ServicesTests.WithParameters
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

            Create shipmentParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentParameters);

            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Parameters.Pickup.Create pickupParameters = Fixtures.Parameters.Pickups.Create(pickupData);

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

            All parameters = Fixtures.Parameters.Pickups.All(data);

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

            Create shipmentCreateParameters = Fixtures.Parameters.Shipments.Create(shipmentData);

            Shipment shipment = await Client.Shipment.Create(shipmentCreateParameters);

            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Parameters.Pickup.Create pickupCreateParameters = Fixtures.Parameters.Pickups.Create(pickupData);

            Pickup pickup = await Client.Pickup.Create(pickupCreateParameters);

            Buy pickupBuyParameters = new Buy(Fixtures.Usps, Fixtures.PickupService);

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
