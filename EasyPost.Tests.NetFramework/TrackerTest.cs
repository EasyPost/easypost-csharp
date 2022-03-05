using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.NetFramework
{
    [TestClass]
    public class TrackerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            TestSuite.SetUp(TestSuiteApiKey.Test);
        }

        private static Tracker CreateBasicTracker()
        {
            return Tracker.Create(Fixture.Usps, "EZ1000000001");
        }

        [TestMethod]
        public void TestCreate()
        {
            Tracker tracker = CreateBasicTracker();

            Assert.IsInstanceOfType(tracker, typeof(Tracker));
            Assert.IsTrue(tracker.id.StartsWith("trk_"));
            Assert.AreEqual("pre_transit", tracker.status);
        }

        [TestMethod]
        public void TestRetrieve()
        {
            // Test trackers cycle through their "dummy" statuses automatically, the created and retrieved objects may differ
            Tracker tracker = CreateBasicTracker();

            Tracker retrievedTracker = Tracker.Retrieve(tracker.id);

            Assert.IsInstanceOfType(retrievedTracker, typeof(Tracker));
            Assert.AreEqual(tracker.id, retrievedTracker.id);
        }

        [TestMethod]
        public void TestAll()
        {
            TrackerCollection trackerCollection = Tracker.All(new Dictionary<string, object>
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
        public void TestCreateList()
        {
            bool success = Tracker.CreateList(new Dictionary<string, object>
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
