using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class TrackerTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("tracker");
        }

        private static async Task<Tracker> CreateBasicTracker()
        {
            return await Tracker.Create(Fixture.Usps, "EZ1000000001");
        }

        [TestMethod]
        public async Task TestCreate()
        {
            _vcr.SetUpTest("create");

            Tracker tracker = await CreateBasicTracker();

            Assert.IsInstanceOfType(tracker, typeof(Tracker));
            Assert.IsTrue(tracker.id.StartsWith("trk_"));
            Assert.AreEqual("pre_transit", tracker.status);
        }

        [TestMethod]
        public async Task TestRetrieve()
        {
            _vcr.SetUpTest("retrieve");


            // Test trackers cycle through their "dummy" statuses automatically, the created and retrieved objects may differ
            Tracker tracker = await CreateBasicTracker();

            Tracker retrievedTracker = await Tracker.Retrieve(tracker.id);

            Assert.IsInstanceOfType(retrievedTracker, typeof(Tracker));
            // Must compare IDs because other elements of objects may be different
            Assert.AreEqual(tracker.id, retrievedTracker.id);
        }

        [TestMethod]
        public async Task TestAll()
        {
            _vcr.SetUpTest("all");

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
            _vcr.SetUpTest("create_list");

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
