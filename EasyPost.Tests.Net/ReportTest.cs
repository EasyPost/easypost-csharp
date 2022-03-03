using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests.Net
{
    [TestClass]
    public class ReportTest
    {
        [TestInitialize]
        public void Initialize()
        {
            VCR.SetUp(VCRApiKey.Test, "report", true);
        }

        private static Report CreateBasicReport(string reportType)
        {
            return Report.Create(reportType, new Dictionary<string, object>
            {
                {
                    "start_date", Fixture.ReportStartDate
                },
                {
                    "end_date", Fixture.ReportEndDate
                }
            });
        }

        private static void TestCreateReport(string cassetteName, string reportType, string idPrefix)
        {
            VCR.Replay(cassetteName);

            Report report = CreateBasicReport(reportType);

            Assert.IsInstanceOfType(report, typeof(Report));
            Assert.IsTrue(report.id.StartsWith(idPrefix));
        }

        private static void TestRetrieveReport(string cassetteName, string reportType)
        {
            VCR.Replay(cassetteName);


            Report report = CreateBasicReport(reportType);

            Report retrievedReport = Report.Retrieve(report.id);

            Assert.IsInstanceOfType(report, typeof(Report));
            Assert.AreEqual(report.start_date, retrievedReport.start_date);
            Assert.AreEqual(report.end_date, retrievedReport.end_date);
        }

        [TestMethod]
        public void TestCreatePaymentLogReport()
        {
            TestCreateReport("create_payment_log_report", "payment_log", "plrep_");
        }

        [TestMethod]
        public void TestCreateRefundReport()
        {
            TestCreateReport("create_refund_report", "refund", "refrep_");
        }

        [TestMethod]
        public void TestCreateShipmentReport()
        {
            TestCreateReport("create_shipment_report", "shipment", "shprep_");
        }

        [TestMethod]
        public void TestCreateShipmentInvoiceReport()
        {
            TestCreateReport("create_shipment_invoice_report", "shipment_invoice", "shpinvrep_");
        }

        [TestMethod]
        public void TestCreateTrackerReport()
        {
            TestCreateReport("create_tracker_report", "tracker", "trkrep_");
        }

        [TestMethod]
        public void TestRetrievePaymentLogReport()
        {
            TestRetrieveReport("retrieve_payment_log_report", "payment_log");
        }

        [TestMethod]
        public void TestRetrieveRefundReport()
        {
            TestRetrieveReport("retrieve_refund_report", "refund");
        }

        [TestMethod]
        public void TestRetrieveShipmentReport()
        {
            TestRetrieveReport("retrieve_shipment_report", "shipment");
        }

        [TestMethod]
        public void TestRetrieveShipmentInvoiceReport()
        {
            TestRetrieveReport("retrieve_shipment_invoice_report", "shipment_invoice");
        }

        [TestMethod]
        public void TestRetrieveTrackerReport()
        {
            TestRetrieveReport("retrieve_tracker_report", "tracker");
        }

        [TestMethod]
        public void TestAll()
        {
            VCR.Replay("all");

            ReportList reportList = Report.All("shipment", new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Report> reports = reportList.reports;

            Assert.IsTrue(reports.Count <= Fixture.PageSize);
            Assert.IsNotNull(reportList.has_more);
            foreach (var report in reports)
            {
                Assert.IsInstanceOfType(report, typeof(Report));
            }
        }

        [TestMethod]
        public void TestCreateNoType()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Report.Create(""));
        }

        [TestMethod]
        public void TestAllNoType()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Report.All(""));
        }
    }
}
