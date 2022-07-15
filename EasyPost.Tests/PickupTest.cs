using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.API;
using EasyPost.Parameters;
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

            Pickup pickup = await Client.CreateBasicPickup();

            pickup = await pickup.Buy(new Pickups.Buy
            {
                Carrier = Fixture.Usps,
                Service = Fixture.PickupService
            });

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.Id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.Confirmation);
            Assert.AreEqual("scheduled", pickup.Status);
        }

        [Fact]
        public async Task TestCancel()
        {
            UseVCR("cancel", ApiVersion.Latest);

            Pickup pickup = await Client.CreateBasicPickup();

            pickup = await pickup.Buy(new Pickups.Buy
            {
                Carrier = Fixture.Usps,
                Service = Fixture.PickupService
            });

            pickup = await pickup.Cancel();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.Id.StartsWith("pickup_"));
            Assert.AreEqual("canceled", pickup.Status);
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create", ApiVersion.Latest);

            Pickup pickup = await Client.CreateBasicPickup();

            Assert.IsInstanceOfType(pickup, typeof(Pickup));
            Assert.IsTrue(pickup.Id.StartsWith("pickup_"));
            Assert.IsNotNull(pickup.PickupRates);
        }

        [Fact]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate", ApiVersion.Latest);

            Pickup pickup = await Client.CreateBasicPickup();

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
            Assert.ThrowsException<FilterFailureException>(() => pickup.LowestRate(null, services));

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new List<string>
            {
                "BAD_CARRIER"
            };
            Assert.ThrowsException<FilterFailureException>(() => pickup.LowestRate(carriers));
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            Pickup pickup = await Client.CreateBasicPickup();

            Pickup retrievedPickup = await Client.Pickups.Retrieve(pickup.Id);

            Assert.IsInstanceOfType(retrievedPickup, typeof(Pickup));
            Assert.AreEqual(pickup, retrievedPickup);
        }
    }
}
