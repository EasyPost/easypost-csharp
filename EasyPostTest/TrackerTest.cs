using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest {
    [TestClass]
    public class TrackerTest {
        [TestInitialize]
        public void Initialize() {
            Client.apiKey = "cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi";
        }

        [TestMethod]
        public void TestCreate() {
            string carrier = "USPS";
            string trackingCode = "EZ1000000001";

            Tracker tracker = Tracker.Create(carrier, trackingCode);
            Assert.AreEqual(tracker.tracking_code, trackingCode);
        }
    }
}