using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
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

            Tracker tracker = Tracker.Create(carrier, trackingCode);
            Assert.AreEqual(tracker.tracking_code, trackingCode);
            Assert.IsNotNull(tracker.est_delivery_date);
            Assert.IsNotNull(tracker.carrier);
            Assert.IsNotNull(tracker.public_url);

            Assert.AreEqual(Tracker.Retrieve(tracker.id).id, tracker.id);
        }

        [TestMethod]
        public void TestCreateTrackerList()
        {
            string[] trackingCodes =
            {
                "EZ1000000001", "EZ1000000002", "EZ1000000003"
            };
            Dictionary<string, object> trackers = new Dictionary<string, object>();

            for (int i = 0; i < trackingCodes.Length; i++)
            {
                trackers.Add(i.ToString(), new Dictionary<string, object>
                {
                    {
                        "tracking_code", trackingCodes[i]
                    },
                    {
                        "carrier", "USPS"
                    }
                });
            }

            bool response = Tracker.CreateList(trackers);
            Assert.IsTrue(response);
        }

        [TestMethod]
        public void TestAll()
        {
            TrackerList trackerList = Tracker.All(new Dictionary<string, object>
            {
                {
                    "page_size", 1
                }
            });
            Assert.AreNotEqual(0, trackerList.trackers.Count);

            TrackerList nextTrackerList = trackerList.Next();
            Assert.AreNotEqual(trackerList.trackers[0].id, nextTrackerList.trackers[0].id);
        }
    }
}
