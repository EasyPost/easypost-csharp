// TrackerTest.cs
// See LICENSE for licensing info.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class TrackerTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

        [Ignore]
        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            const string carrier = "USPS";
            const string trackingCode = "EZ1000000001";

            var tracker = Tracker.Create(carrier, trackingCode);
            Assert.AreEqual(tracker.tracking_code, trackingCode);
            Assert.IsNotNull(tracker.est_delivery_date);
            Assert.IsNotNull(tracker.carrier);
            Assert.IsNotNull(tracker.public_url);

            Assert.AreEqual(Tracker.Retrieve(tracker.id).id, tracker.id);
        }

        [TestMethod]
        public void TestList()
        {
            var trackerList = Tracker.List(new Dictionary<string, object>
            {
                {
                    "page_size", 1
                }
            });
            Assert.AreNotEqual(0, trackerList.trackers.Count);

            var nextTrackerList = trackerList.Next();
            Assert.AreNotEqual(trackerList.trackers[0].id, nextTrackerList.trackers[0].id);
        }
    }
}
