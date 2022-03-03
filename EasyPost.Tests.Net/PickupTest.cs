using System.Collections.Generic;
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

        private static Pickup CreateBasicPickup()
        {
            Shipment shipment = Shipment.Create(Fixture.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixture.BasicPickup;
            pickupData["shipment"] = shipment;
            return Pickup.Create(pickupData);
        }

        [TestMethod]
        public void TestCreate()
        {
            VCR.Replay("create");

            Pickup pickup = CreateBasicPickup();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.pickup_rates);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            VCR.Replay("retrieve");


            Pickup pickup = CreateBasicPickup();

            Pickup retrievedPickup = Pickup.Retrieve(pickup.id);

            Assert.IsInstanceOfType(retrievedPickup, typeof(Pickup));
            Assert.AreEqual(pickup.id, retrievedPickup.id);
        }

        [TestMethod]
        public void TestBuy()
        {
            VCR.Replay("buy");

            //use "TestCreate"
            Pickup pickup = CreateBasicPickup();

            pickup.Buy(Fixture.Usps, Fixture.NextDayService);

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.confirmation);
            Assert.AreEqual("scheduled", pickup.status);
        }

        [TestMethod]
        public void TestCancel()
        {
            VCR.Replay("cancel");

            //use "TestCreate"
            Pickup pickup = CreateBasicPickup();

            pickup.Buy(Fixture.Usps, Fixture.NextDayService);

            pickup.Cancel();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.AreEqual("canceled", pickup.status);
        }
    }
}
