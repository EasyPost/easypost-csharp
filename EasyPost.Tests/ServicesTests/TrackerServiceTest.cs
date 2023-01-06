using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
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

            Assert.True(trackerCollection.HasMore);
            Assert.True(trackers.Count <= Fixtures.PageSize);
            foreach (Tracker tracker in trackers)
            {
                Assert.IsType<Tracker>(tracker);
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
