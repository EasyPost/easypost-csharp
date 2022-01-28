// ReportTest.cs
// See LICENSE for licensing info.

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ReportTest
    {
        [TestInitialize]
        public void Initialize() => ClientManager.SetCurrent("NvBX2hFF44SVvTPtYjF0zQ");

        [TestMethod]
        public void TestCreateAndRetrieve()
        {
            var parameters = new Dictionary<string, object>();
            var report = Report.Create("shipment", parameters);
            Assert.IsNotNull(report.id);

            var retrieved = Report.Retrieve("shipment", report.id);
            Assert.AreEqual(report.id, retrieved.id);

            retrieved = Report.Retrieve(report.id);
            Assert.AreEqual(report.id, retrieved.id);
        }

        [TestMethod]
        public void TestList()
        {
            var reportList = Report.List("shipment", new Dictionary<string, object>
            {
                {
                    "page_size", 1
                }
            });
            Assert.AreNotEqual(0, reportList.reports.Count);

            var nextReportList = reportList.Next();
            Assert.AreNotEqual(reportList.reports[0].id, nextReportList.reports[0].id);
        }
    }
}
