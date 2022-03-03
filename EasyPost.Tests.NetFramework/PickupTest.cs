using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class PickupTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
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
            Pickup pickup = CreateBasicPickup();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.pickup_rates);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            Pickup pickup = CreateBasicPickup();

            Pickup retrievedPickup = Pickup.Retrieve(pickup.id);

            Assert.IsInstanceOfType(retrievedPickup, typeof(Pickup));
            Assert.AreEqual(pickup.id, retrievedPickup.id);
        }

        [TestMethod]
        public void TestBuy()
        {
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
