using System.Collections.Generic;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using EasyPost;

namespace EasyPostTest {
    [TestClass]
    public class ReportTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("cueqNZUb3ldeWTNX7MU3Mel8UXtaAMUi");
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() {
                { "include_children", true },
                { "end_date", "2016-06-01" }
            };
            Report report = Report.Create("shipment", parameters);
            Assert.IsNotNull(report.id);
            Assert.IsTrue(report.include_children);

            Report retrieved = Report.Retrieve("shipment", report.id);
            Assert.AreEqual(report.id, retrieved.id);
        }
    }
}
