using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
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

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Pickup pickup = await CreateBasicPickup();

            pickup = await Client.Pickup.Buy(pickup.Id, Fixtures.Usps, Fixtures.PickupService);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.Confirmation);
            Assert.Equal("scheduled", pickup.Status);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestCancel()
        {
            UseVCR("cancel");

            Pickup pickup = await CreateBasicPickup();

            pickup = await Client.Pickup.Buy(pickup.Id, Fixtures.Usps, Fixtures.PickupService);

            pickup = await Client.Pickup.Cancel(pickup.Id);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.Equal("canceled", pickup.Status);
        }

        [Fact]
        [Testing.Function]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Pickup pickup = await CreateBasicPickup();

            // test lowest rate with no filters
            Rate lowestRate = Client.Pickup.LowestRate(pickup);
            Assert.Equal("NextDay", lowestRate.Service);
            Assert.Equal("0.00", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (should error due to bad service)
            List<string> services = new() { "BAD_SERVICE" };
            Assert.Throws<FilteringError>(() => Client.Pickup.LowestRate(pickup, null, services));

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new() { "BAD_CARRIER" };
            Assert.Throws<FilteringError>(() => Client.Pickup.LowestRate(pickup, carriers));
        }

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
