using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class TrackerTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            string carrier = "USPS";
            string trackingCode = "EZ1000000001";

            Tracker tracker = Tracker.Create(carrier, trackingCode);
            Assert.AreEqual(tracker.tracking_code, trackingCode);
            Assert.IsNotNull(tracker.est_delivery_date);
            Assert.IsNotNull(tracker.carrier);

            Assert.AreEqual(Tracker.Retrieve(tracker.id).id, tracker.id);
        }

        [TestMethod]
        public void TestList() {
            TrackerList trackerList = Tracker.List();
            Assert.AreNotEqual(0, trackerList.trackers.Count);

            TrackerList nextTrackerList = trackerList.Next();
            Assert.AreNotEqual(trackerList.trackers[0].id, nextTrackerList.trackers[0].id);
        }
    }
}