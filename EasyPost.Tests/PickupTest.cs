using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.V2;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

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
            UseVCR("buy", ApiVersion.V2);

            Pickup pickup = await CreateBasicPickup();

            pickup = await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.confirmation);
            Assert.AreEqual("scheduled", pickup.status);
        }

        [Fact]
        public async Task TestCancel()
        {
            UseVCR("cancel", ApiVersion.V2);

            Pickup pickup = await CreateBasicPickup();

            pickup = await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            pickup = await pickup.Cancel();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.AreEqual("canceled", pickup.status);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.V2);

            Pickup pickup = await CreateBasicPickup();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.pickup_rates);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.V2);

            Pickup pickup = await CreateBasicPickup();

            Pickup retrievedPickup = await Client.Pickups.Retrieve(pickup.id);

            Assert.IsInstanceOfType(retrievedPickup, typeof(Pickup));
            Assert.AreEqual(pickup, retrievedPickup);
        }

        private async Task<Pickup> CreateBasicPickup()
        {
            Shipment shipment = await Client.Shipments.Create(Fixture.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixture.BasicPickup;
            pickupData["shipment"] = shipment;
            return await Client.Pickups.Create(pickupData);
        }

        [Fact]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate", ApiVersion.V2);

            Pickup pickup = await CreateBasicPickup();

            // test lowest rate with no filters
            Rate lowestRate = pickup.LowestRate();
            Assert.AreEqual("NextDay", lowestRate.service);
            Assert.AreEqual("0.00", lowestRate.rate);
            Assert.AreEqual("USPS", lowestRate.carrier);

            // test lowest rate with service filter (should error due to bad service)
            List<string> services = new List<string>
            {
                "BAD_SERVICE"
            };
            Assert.ThrowsException<FilterFailure>(() => pickup.LowestRate(null, services, null, null));

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string>
            {
                "BAD_CARRIER"
            };
            Assert.ThrowsException<FilterFailure>(() => pickup.LowestRate(carriers, null, null, null));
        }
    }
}
