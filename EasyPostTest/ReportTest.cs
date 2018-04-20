using EasyPost;

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace EasyPostTest {
    [TestClass]
    public class ReportTest {
        [TestInitialize]
        public void Initialize() {
            ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");
        }

        [TestMethod]
        public void TestCreateAndRetrieve() {
            Dictionary<string, object> parameters = new Dictionary<string, object>() { };
            Report report = Report.Create("shipment", parameters);
            Assert.IsNotNull(report.id);

            Report retrieved = Report.Retrieve("shipment", report.id);
            Assert.AreEqual(report.id, retrieved.id);
        }

        [TestMethod]
        public void TestList() {
            ReportList reportList = Report.List("shipment", new Dictionary<string, object>() { { "page_size", 1 } });
            Assert.AreNotEqual(0, reportList.reports.Count);

            ReportList nextReportList = reportList.Next();
            Assert.AreNotEqual(reportList.reports[0].id, nextReportList.reports[0].id);
        }
    }
}
