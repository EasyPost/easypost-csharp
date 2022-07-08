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
            UseVCR("buy", ApiVersion.Latest);

            Pickup pickup = await CreateBasicPickup();

            pickup = await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.Id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.Confirmation);
            Assert.AreEqual("scheduled", pickup.Status);
        }

        [Fact]
        public async Task TestCancel()
        {
            UseVCR("cancel", ApiVersion.Latest);

            Pickup pickup = await CreateBasicPickup();

            pickup = await pickup.Buy(Fixture.Usps, Fixture.PickupService);

            pickup = await pickup.Cancel();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.Id.StartsWith("pickup_"));
            Assert.AreEqual("canceled", pickup.Status);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Pickup pickup = await CreateBasicPickup();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.Id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.PickupRates);
        }

        [Fact]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate", ApiVersion.Latest);

            Pickup pickup = await CreateBasicPickup();

            // test lowest rate with no filters
            Rate lowestRate = pickup.LowestRate();
            Assert.AreEqual("NextDay", lowestRate.Service);
            Assert.AreEqual("0.00", lowestRate.Price);
            Assert.AreEqual("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (should error due to bad service)
            List<string> services = new List<string>
            {
                "BAD_SERVICE"
            };
            Assert.ThrowsException<FilterFailure>(() => pickup.LowestRate(null, services));

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string>
            {
                "BAD_CARRIER"
            };
            Assert.ThrowsException<FilterFailure>(() => pickup.LowestRate(carriers));
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Pickup pickup = await CreateBasicPickup();

            Pickup retrievedPickup = await Client.Pickups.Retrieve(pickup.Id);

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
    }
}
