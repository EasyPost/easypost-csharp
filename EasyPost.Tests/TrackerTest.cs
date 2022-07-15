using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
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
            UseVCR("all", ApiVersion.Latest);

            TrackerCollection trackerCollection = await Client.Trackers.All(new All
            {
                PageSize = Fixture.PageSize
            });

            List<Tracker> trackers = trackerCollection.Trackers;

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
            UseVCR("create", ApiVersion.Latest);

            Tracker tracker = await Client.CreateBasicTracker();

            Assert.IsInstanceOfType(tracker, typeof(Tracker));
            Assert.IsTrue(tracker.Id.StartsWith("trk_"));
            Assert.AreEqual("pre_transit", tracker.Status);
        }

        [Fact]
        public async Task TestCreateList()
        {
            UseVCR("create_list", ApiVersion.Latest);

            bool success = await Client.Trackers.CreateList(new Trackers.CreateList
            {
                Trackers = new List<Tracker>
                {
                    new Tracker
                    {
                        TrackingCode = "EZ1000000001"
                    },
                    new Tracker
                    {
                        TrackingCode = "EZ1000000002"
                    },
                    new Tracker
                    {
                        TrackingCode = "EZ1000000003"
                    }
                }
            });

            // This endpoint returns nothing so we assert the function returns true
            Assert.IsTrue(success);
        }

        [Fact]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve", ApiVersion.Latest);

            // Test trackers cycle through their "dummy" statuses automatically, the created and retrieved objects may differ
            Tracker tracker = await Client.CreateBasicTracker();

            Tracker retrievedTracker = await Client.Trackers.Retrieve(tracker.Id);

            Assert.IsInstanceOfType(retrievedTracker, typeof(Tracker));
            // Must compare IDs because other elements of objects may be different
            Assert.AreEqual(tracker.Id, retrievedTracker.Id);
        }
    }
}
