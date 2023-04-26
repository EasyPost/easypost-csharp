using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ModelsTests
{
#pragma warning disable xUnit1004
    public class PickupTests : UnitTest
    {
        public PickupTests() : base("pickup")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);

            pickup = await pickup.Buy(Fixtures.Usps, Fixtures.PickupService);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.Confirmation);
            Assert.Equal("scheduled", pickup.Status);
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestBuyWithNoId()
        {
            UseVCR("buy_with_no_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);
            pickup.Id = null;

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await pickup.Buy(Fixtures.Usps, Fixtures.UspsService));
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestCancel()
        {
            UseVCR("cancel");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);

            pickup = await pickup.Buy(Fixtures.Usps, Fixtures.PickupService);

            pickup = await pickup.Cancel();

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.Equal("canceled", pickup.Status);
        }

        [Fact(Skip = "TO BE REMOVED.")]
        [CrudOperations.Update]
        [Testing.Parameters]
        public async Task TestCancelWithNoId()
        {
            UseVCR("cancel_with_no_id");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);
            pickup.Id = null;

            // We don't need to buy the pickup first, since this should fail before actually hitting the API.

            await Assert.ThrowsAsync<MissingPropertyError>(async () => await pickup.Cancel());
        }

        #endregion

        [Fact(Skip = "TO BE REMOVED.")]
        [Testing.Function]
        public async Task TestLowestRate()
        {
            UseVCR("lowest_rate");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);

            // test lowest rate with no filters
            Rate lowestRate = pickup.LowestRate();
            Assert.Equal("NextDay", lowestRate.Service);
            Assert.Equal("0.00", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (should error due to bad service)
            List<string> services = new() { "BAD_SERVICE" };
            Assert.Throws<FilteringError>(() => pickup.LowestRate(null, services));

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new() { "BAD_CARRIER" };
            Assert.Throws<FilteringError>(() => pickup.LowestRate(carriers));
        }

        #endregion
    }
}
