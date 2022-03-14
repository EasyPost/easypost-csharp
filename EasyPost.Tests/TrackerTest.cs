using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class TrackerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "tracker", true);
        }

        private static async Task<Tracker> CreateBasicTracker()
        {
            return await Tracker.Create(Fixture.Usps, "EZ1000000001");
        }

        [TestMethod]
        public async Task TestCreate()
        {
            VCR.Replay("create");

            Tracker tracker = await CreateBasicTracker();

            Assert.IsInstanceOfType(tracker, typeof(Tracker));
            Assert.IsTrue(tracker.id.StartsWith("trk_"));
            Assert.AreEqual("pre_transit", tracker.status);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            VCR.Replay("retrieve");


            // Test trackers cycle through their "dummy" statuses automatically, the created and retrieved objects may differ
            Tracker tracker = await CreateBasicTracker();

            Tracker retrievedTracker = await Tracker.Retrieve(tracker.id);

            Assert.IsInstanceOfType(retrievedTracker, typeof(Tracker));
            Assert.AreEqual(tracker.id, retrievedTracker.id);
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            TrackerCollection trackerCollection = await Tracker.All(new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Tracker> trackers = trackerCollection.trackers;

            Assert.IsTrue(trackers.Count <= Fixture.PageSize);
            Assert.IsNotNull(trackerCollection.has_more);
            foreach (var tracker in trackers)
            {
                Assert.IsInstanceOfType(tracker, typeof(Tracker));
            }
        }

        [TestMethod]
        public async Task TestCreateList()
        {
            VCR.Replay("create_list");

            bool success = await Tracker.CreateList(new Dictionary<string, object>
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
                },
            });

            // This endpoint returns nothing so we assert the function returns true
            Assert.IsTrue(success);
        }
    }
}
