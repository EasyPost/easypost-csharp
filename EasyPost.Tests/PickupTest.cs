using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;

namespace EasyPost.Tests
{
    public class PickupTest : UnitTest
    {
        public PickupTest() : base("pickup")
        {
        }

        [Fact]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Pickup pickup = await CreateBasicPickup();

            pickup = await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.id);
            Assert.NotNull(pickup.confirmation);
            Assert.Equal("scheduled", pickup.status);
        }

        [Fact]
        public async Task TestCancel()
        {
            UseVCR("cancel");

            Pickup pickup = await CreateBasicPickup();

            pickup = await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            pickup = await pickup.Cancel();

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.id);
            Assert.Equal("canceled", pickup.status);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            Pickup pickup = await CreateBasicPickup();

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.id);
            Assert.NotNull(pickup.pickup_rates);
        }

        [Fact]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Pickup pickup = await CreateBasicPickup();

            // test lowest rate with no filters
            Rate lowestRate = pickup.LowestRate();
            Assert.Equal("NextDay", lowestRate.service);
            Assert.Equal("0.00", lowestRate.rate);
            Assert.Equal("USPS", lowestRate.carrier);

            // test lowest rate with service filter (should error due to bad service)
            List<string> services = new List<string>
            {
                "BAD_SERVICE"
            };
            Assert.Throws<Exception>(() => pickup.LowestRate(null, services));

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string>
            {
                "BAD_CARRIER"
            };
            Assert.Throws<Exception>(() => pickup.LowestRate(carriers));
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Pickup pickup = await CreateBasicPickup();

            Pickup retrievedPickup = await Client.Pickup.Retrieve(pickup.id);

            Assert.IsType<Pickup>(retrievedPickup);
            Assert.Equal(pickup.id, retrievedPickup.id);
        }

        private async Task<Pickup> CreateBasicPickup()
        {
            Shipment shipment = await Client.Shipment.Create(Fixture.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixture.BasicPickup;
            pickupData["shipment"] = shipment;
            return await Client.Pickup.Create(pickupData);
        }
    }
}
