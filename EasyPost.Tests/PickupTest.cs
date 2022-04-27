using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class PickupTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("pickup");
        }

        private static async Task<Pickup> CreateBasicPickup(V2Client v2Client)
        {
            Shipment shipment = await v2Client.Shipments.Create(Fixture.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixture.BasicPickup;
            pickupData["shipment"] = shipment;
            return await v2Client.Pickups.Create(pickupData);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            V2Client v2Client = _vcr.SetUpTest("create");

            Pickup pickup = await CreateBasicPickup(v2Client);

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.pickup_rates);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            V2Client v2Client = _vcr.SetUpTest("retrieve");

            Pickup pickup = await CreateBasicPickup(v2Client);

            Pickup retrievedPickup = await v2Client.Pickups.Retrieve(pickup.id);

            Assert.IsInstanceOfType(retrievedPickup, typeof(Pickup));
            Assert.AreEqual(pickup, retrievedPickup);
        }

        [TestMethod]
        public async Task TestBuy()
        {
            V2Client v2Client = _vcr.SetUpTest("buy");

            //use "TestCreate"
            Pickup pickup = await CreateBasicPickup(v2Client);

            await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.confirmation);
            Assert.AreEqual("scheduled", pickup.status);
        }

        [TestMethod]
        public async Task TestCancel()
        {
            V2Client v2Client = _vcr.SetUpTest("cancel");

            //use "TestCreate"
            Pickup pickup = await CreateBasicPickup(v2Client);

            await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            await pickup.Cancel();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.AreEqual("canceled", pickup.status);
        }
    }
}
