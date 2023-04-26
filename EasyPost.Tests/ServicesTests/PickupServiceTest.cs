using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class PickupServiceTests : UnitTest
    {
        public PickupServiceTests() : base("pickup_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.PickupRates);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);

            Pickup retrievedPickup = await Client.Pickup.Retrieve(pickup.Id);

            Assert.IsType<Pickup>(retrievedPickup);
            Assert.Equal(pickup.Id, retrievedPickup.Id);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            PickupCollection pickupCollection = await Client.Pickup.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Pickup> pickups = pickupCollection.Pickups;

            Assert.True(pickups.Count <= Fixtures.PageSize);
            foreach (Pickup pickup in pickups)
            {
                Assert.IsType<Pickup>(pickup);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            PickupCollection collection = await Client.Pickup.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                PickupCollection nextPageCollection = await Client.Pickup.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Pickups[0].Id, nextPageCollection.Pickups[0].Id);
            }
            catch (EndOfPaginationError e) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
            }
        }
        
        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestBuy()
        {
            UseVCR("buy");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);

            pickup = await Client.Pickup.Buy(pickup.Id, Fixtures.Usps, Fixtures.PickupService);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.NotNull(pickup.Confirmation);
            Assert.Equal("scheduled", pickup.Status);
        }

        [Fact]
        [CrudOperations.Update]
        [Testing.Function]
        public async Task TestCancel()
        {
            UseVCR("cancel");

            Shipment shipment = await Client.Shipment.Create(Fixtures.OneCallBuyShipment);
            Dictionary<string, object> pickupData = Fixtures.BasicPickup;
            pickupData["shipment"] = shipment;

            Pickup pickup = await Client.Pickup.Create(pickupData);

            pickup = await Client.Pickup.Buy(pickup.Id,Fixtures.Usps, Fixtures.PickupService);

            pickup = await Client.Pickup.Cancel(pickup.Id);

            Assert.IsType<Pickup>(pickup);
            Assert.StartsWith("pickup_", pickup.Id);
            Assert.Equal("canceled", pickup.Status);
        }

        [Fact]
        [Testing.Function]
        public void TestLowestRate()
        {
            // Mock rates since these can change from the API and we want to test the local filtering logic, not the API call
            List<PickupRate> rates = new List<PickupRate>
            {
                new PickupRate
                {
                    Service = "Priority",
                    Carrier = "USPS",
                    Price = "7.58",
                },
                new PickupRate
                {
                    Service = "First",
                    Carrier = "USPS",
                    Price = "6.07",
                },
            };
            Pickup pickup = new Pickup
            {
                PickupRates = rates,
            };

            // test lowest rate with no filters
            Rate lowestRate = pickup.LowestRate();
            Assert.Equal("First", lowestRate.Service);
            Assert.Equal("6.07", lowestRate.Price);
            Assert.Equal("USPS", lowestRate.Carrier);

            // test lowest rate with service filter (should error due to bad service)
            List<string> services = new() { "BAD_SERVICE" };
            Assert.Throws<FilteringError>(() => pickup.LowestRate(null, services));

            // test lowest rate with carrier filter (should error due to bad carrier)
            List<string> carriers = new() { "BAD_CARRIER" };
            Assert.Throws<FilteringError>(() => pickup.LowestRate(carriers));
        }

        #endregion

        #endregion
    }
}
