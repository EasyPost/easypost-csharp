using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class PickupTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "pickup", true);
        }

        private static async Task<Pickup> CreateBasicPickup()
        {
            Shipment shipment = await Shipment.Create(Fixture.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixture.BasicPickup;
            pickupData["shipment"] = shipment;
            return await Pickup.Create(pickupData);
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Pickup pickup = await CreateBasicPickup();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.pickup_rates);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            Pickup pickup = await CreateBasicPickup();

            Pickup retrievedPickup = await Pickup.Retrieve(pickup.id);

            Assert.IsInstanceOfType(retrievedPickup, typeof(Pickup));
            Assert.AreEqual(pickup.id, retrievedPickup.id);
        }

        [TestMethod]
        public async Task TestBuy()
        {
            VCR.Replay("buy");

            //use "TestCreate"
            Pickup pickup = await CreateBasicPickup();

            await pickup.Buy(Fixture.Usps, Fixture.NextDayService);

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.confirmation);
            Assert.AreEqual("scheduled", pickup.status);
        }

        [TestMethod]
        public async Task TestCancel()
        {
            VCR.Replay("cancel");

            //use "TestCreate"
            Pickup pickup = await CreateBasicPickup();

            await pickup.Buy(Fixture.Usps, Fixture.NextDayService);

            await pickup.Cancel();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.AreEqual("canceled", pickup.status);
        }
    }
}
