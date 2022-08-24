using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests
{
    public class TrackerTest : UnitTest
    {
        public TrackerTest() : base("tracker")
        {
        }

        #region CRUD Operations

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreate()
        {
            UseVCR("create");

            Tracker tracker = await CreateBasicTracker();

            Assert.IsType<Tracker>(tracker);
            Assert.StartsWith("trk_", tracker.id);
            Assert.Equal("pre_transit", tracker.status);
        }

        [Fact]
        [CrudOperations.Create]
        public async Task TestCreateList()
        {
            UseVCR("create_list");

            var possibleException = await Record.ExceptionAsync(async () => await Client.Tracker.CreateList(new Dictionary<string, object>
            {
                {
                    "0", new Dictionary<string, object>
                    {
                        {
                            "tracking_code", "EZ1000000001"
                        }
                    }
                },
                {
                    "1", new Dictionary<string, object>
                    {
                        {
                            "tracking_code", "EZ1000000002"
                        }
                    }
                },
                {
                    "2", new Dictionary<string, object>
                    {
                        {
                            "tracking_code", "EZ1000000003"
                        }
                    }
                }
            }));

            Assert.Null(possibleException);
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestAll()
        {
            UseVCR("all");

            TrackerCollection trackerCollection = await Client.Tracker.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Tracker> trackers = trackerCollection.trackers;

            Assert.True(trackerCollection.has_more);
            Assert.True(trackers.Count <= Fixture.PageSize);
            foreach (Tracker tracker in trackers)
            {
                Assert.IsType<Tracker>(tracker);
            }
        }

        [Fact]
        [CrudOperations.Read]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            // Test trackers cycle through their "dummy" statuses automatically, the created and retrieved objects may differ
            Tracker tracker = await CreateBasicTracker();

            Tracker retrievedTracker = await Client.Tracker.Retrieve(tracker.id);

            Assert.IsType<Tracker>(retrievedTracker);
            // Must compare IDs because other elements of objects may be different
            Assert.Equal(tracker.id, retrievedTracker.id);
        }

        #endregion

        private async Task<Tracker> CreateBasicTracker() => await Client.Tracker.Create(Fixture.Usps, "EZ1000000001");
    }
}
