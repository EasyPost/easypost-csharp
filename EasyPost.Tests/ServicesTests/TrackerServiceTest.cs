using System;
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
    public class TrackerServiceTests : UnitTest
    {
        public TrackerServiceTests() : base("tracker_service")
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

            Tracker tracker = await Client.Tracker.Create(Fixtures.Usps, "EZ1000000001");

            Assert.IsType<Tracker>(tracker);
            Assert.StartsWith("trk_", tracker.Id);
            Assert.Equal("pre_transit", tracker.Status);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreateList()
        {
            UseVCR("create_list");

            Exception? possibleException = await Record.ExceptionAsync(async () => await Client.Tracker.CreateList(new Dictionary<string, object>
            {
                { "0", new Dictionary<string, object> { { "tracking_code", "EZ1000000001" } } },
                { "1", new Dictionary<string, object> { { "tracking_code", "EZ1000000002" } } },
                { "2", new Dictionary<string, object> { { "tracking_code", "EZ1000000003" } } }
            }));

            Assert.Null(possibleException);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            TrackerCollection trackerCollection = await Client.Tracker.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Tracker> trackers = trackerCollection.Trackers;

            Assert.True(trackers.Count <= Fixtures.PageSize);
            foreach (Tracker tracker in trackers)
            {
                Assert.IsType<Tracker>(tracker);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            TrackerCollection collection = await Client.Tracker.All(new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                TrackerCollection nextPageCollection = await Client.Tracker.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Trackers[0].Id, nextPageCollection.Trackers[0].Id);
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
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            // Test trackers cycle through their "dummy" statuses automatically, the created and retrieved objects may differ
            Tracker tracker = await Client.Tracker.Create(Fixtures.Usps, "EZ1000000001");

            Tracker retrievedTracker = await Client.Tracker.Retrieve(tracker.Id);

            Assert.IsType<Tracker>(retrievedTracker);
            // Must compare IDs because other elements of objects may be different
            Assert.Equal(tracker.Id, retrievedTracker.Id);
        }

        #endregion

        #endregion
    }
}
