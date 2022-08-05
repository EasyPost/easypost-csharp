using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class TrackerTest : UnitTest
    {
        public TrackerTest() : base("tracker")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all");

            TrackerCollection trackerCollection = await Client.Tracker.All(new Dictionary<string, object?>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Tracker> trackers = trackerCollection.trackers;

            Assert.IsTrue(trackers.Count <= Fixture.PageSize);
            Assert.IsNotNull(trackerCollection.HasMore);
            foreach (Tracker tracker in trackers)
            {
                Assert.IsInstanceOfType(tracker, typeof(Tracker));
            }
        }

        [Fact]
        public async Task TestCreate()
        {
            UseVCR("create");

            Tracker tracker = await CreateBasicTracker();

            Assert.IsInstanceOfType(tracker, typeof(Tracker));
            Assert.IsTrue(tracker.id.StartsWith("trk_"));
            Assert.AreEqual("pre_transit", tracker.status);
        }

        [Fact]
        public async Task TestCreateList()
        {
            UseVCR("create_list");

            await Client.Tracker.CreateList(new Dictionary<string, object?>
            {
                {
                    "0", new Dictionary<string, object?>
                    {
                        {
                            "tracking_code", "EZ1000000001"
                        }
                    }
                },
                {
                    "1", new Dictionary<string, object?>
                    {
                        {
                            "tracking_code", "EZ1000000002"
                        }
                    }
                },
                {
                    "2", new Dictionary<string, object?>
                    {
                        {
                            "tracking_code", "EZ1000000003"
                        }
                    }
                }
            });

            // TODO: Assert something here
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            // Test trackers cycle through their "dummy" statuses automatically, the created and retrieved objects may differ
            Tracker tracker = await CreateBasicTracker();

            Tracker retrievedTracker = await Client.Tracker.Retrieve(tracker.id);

            Assert.IsInstanceOfType(retrievedTracker, typeof(Tracker));
            // Must compare IDs because other elements of objects may be different
            Assert.AreEqual(tracker.id, retrievedTracker.id);
        }

        private async Task<Tracker> CreateBasicTracker() => await Client.Tracker.Create(Fixture.Usps, "EZ1000000001");
    }
}
