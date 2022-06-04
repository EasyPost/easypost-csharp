using System.Collections.Generic;
using System.Threading.Tasks;
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
            UseVCR("buy");

            Pickup pickup = await CreateBasicPickup();

            await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.confirmation);
            Assert.AreEqual("scheduled", pickup.status);
        }

        [Fact]
        public async Task TestCancel()
        {
            UseVCR("cancel");

            Pickup pickup = await CreateBasicPickup();

            await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            await pickup.Cancel();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.AreEqual("canceled", pickup.status);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            Pickup pickup = await CreateBasicPickup();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.pickup_rates);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Pickup pickup = await CreateBasicPickup();

            Pickup retrievedPickup = await V2Client.Pickups.Retrieve(pickup.id);

            Assert.IsInstanceOfType(retrievedPickup, typeof(Pickup));
            Assert.AreEqual(pickup, retrievedPickup);
        }

        private async Task<Pickup> CreateBasicPickup()
        {
            Shipment shipment = await V2Client.Shipments.Create(Fixture.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixture.BasicPickup;
            pickupData["shipment"] = shipment;
            return await V2Client.Pickups.Create(pickupData);
        }
    }
}
