﻿using System.Collections.Generic;
using System.Threading.Tasks;
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
            _vcr.SetUpTest("create");

            Pickup pickup = await CreateBasicPickup();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.pickup_rates);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");


            Pickup pickup = await CreateBasicPickup();

            Pickup retrievedPickup = await Pickup.Retrieve(pickup.id);

            Assert.IsInstanceOfType(retrievedPickup, typeof(Pickup));
            Assert.AreEqual(pickup, retrievedPickup);
        }

        [TestMethod]
        public async Task TestBuy()
        {
            _vcr.SetUpTest("buy");

            //use "TestCreate"
            Pickup pickup = await CreateBasicPickup();

            await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.confirmation);
            Assert.AreEqual("scheduled", pickup.status);
        }

        [TestMethod]
        public async Task TestCancel()
        {
            _vcr.SetUpTest("cancel");

            //use "TestCreate"
            Pickup pickup = await CreateBasicPickup();

            await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            await pickup.Cancel();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.id.StartsWith("pickup_"));
            Assert.AreEqual("canceled", pickup.status);
        }
    }
}
