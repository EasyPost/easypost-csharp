using EasyPost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPostTest
{
    [TestClass]
    public class TrackerTest
    {
        [TestInitialize]
        public void Initialize()
        {
            Client.apiKey = "cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi";
        }

        [TestMethod]
        public void TestCanRetrieveTrackerInformation()
        {
            string trackingCode = "1ZA57R94YN1145456285";
            Tracker t = Tracker.Create("fedex", trackingCode);
            Assert.AreEqual(t.tracking_code, trackingCode);
        }
    }
}