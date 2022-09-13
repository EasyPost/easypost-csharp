using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class PickupTest : UnitTest
    {
        public PickupTest() : base("pickup")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            Pickup pickup = await CreateBasicPickup();

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.PickupRates);
        }

        [Fact]
        [CrudOperations.Read] // not really a Read operation, but most logical attribute to maintain CRUD placement
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Pickup pickup = await CreateBasicPickup();

            // test lowest rate with no filters
            Rate lowestRate = pickup.LowestRate();
            Assert.Equal("NextDay", lowestRate.Service);
            Assert.Equal("0.00", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (should error due to bad service)
            List<string> services = new List<string> { "BAD_SERVICE" };
            Assert.Throws<Exception>(() => pickup.LowestRate(null, services));

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string> { "BAD_CARRIER" };
            Assert.Throws<Exception>(() => pickup.LowestRate(carriers));
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Pickup pickup = await CreateBasicPickup();

            Pickup retrievedPickup = await Client.Pickup.Retrieve(pickup.Id);

            Assert.IsType<Pickup>(retrievedPickup);
            Assert.Equal(pickup.Id, retrievedPickup.Id);
        }

        [Fact(Skip = "USPS test server issues. Re-enable ASAP.")]
        [CrudOperations.Update]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Pickup pickup = await CreateBasicPickup();

            pickup = await pickup.Buy(Fixtures.Usps, Fixtures.PickupService);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.Confirmation);
            Assert.Equal("scheduled", pickup.Status);
        }

        [Fact(Skip = "USPS test server issues. Re-enable ASAP.")]
        [CrudOperations.Update]
        public async Task TestCancel()
        {
            UseVCR("cancel");

            Pickup pickup = await CreateBasicPickup();

            pickup = await pickup.Buy(Fixtures.Usps, Fixtures.PickupService);

            pickup = await pickup.Cancel();

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.Equal("canceled", pickup.Status);
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
