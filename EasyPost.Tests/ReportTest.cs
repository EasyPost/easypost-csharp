using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        private static async Task<Report> CreateBasicReport(string reportType)
        {
            return await Report.Create(reportType, new Dictionary<string, object>
            {
                {
                    "start_date", Fixture.ReportStartDate
                },
                {
                    "end_date", Fixture.ReportEndDate
                }
            });
        }

        private static async Task TestCreateReport(string cassetteName, string reportType, string idPrefix)
        {
            VCR.Replay(cassetteName);

            Report report = await CreateBasicReport(reportType);

            Assert.IsInstanceOfType(report, typeof(Report));
            Assert.IsTrue(report.id.StartsWith(idPrefix));
        }

        private static async Task TestRetrieveReport(string cassetteName, string reportType)
        {
            VCR.Replay(cassetteName);


            Report report = await CreateBasicReport(reportType);

            Report retrievedReport = await Report.Retrieve(report.id);

            Assert.IsInstanceOfType(report, typeof(Report));
            Assert.AreEqual(report.start_date, retrievedReport.start_date);
            Assert.AreEqual(report.end_date, retrievedReport.end_date);
        }

        [TestMethod]
        public async Task TestCreatePaymentLogReport()
        {
            await TestCreateReport("create_payment_log_report", "payment_log", "plrep_");
        }

        [TestMethod]
        public async Task TestCreateRefundReport()
        {
            await TestCreateReport("create_refund_report", "refund", "refrep_");
        }

        [TestMethod]
        public async Task TestCreateShipmentReport()
        {
            await TestCreateReport("create_shipment_report", "shipment", "shprep_");
        }

        [TestMethod]
        public async Task TestCreateShipmentInvoiceReport()
        {
            await TestCreateReport("create_shipment_invoice_report", "shipment_invoice", "shpinvrep_");
        }

        [TestMethod]
        public async Task TestCreateTrackerReport()
        {
            await TestCreateReport("create_tracker_report", "tracker", "trkrep_");
        }

        [TestMethod]
        public async Task TestRetrievePaymentLogReport()
        {
            await TestRetrieveReport("retrieve_payment_log_report", "payment_log");
        }

        [TestMethod]
        public async Task TestRetrieveRefundReport()
        {
            await TestRetrieveReport("retrieve_refund_report", "refund");
        }

        [TestMethod]
        public async Task TestRetrieveShipmentReport()
        {
            await TestRetrieveReport("retrieve_shipment_report", "shipment");
        }

        [TestMethod]
        public async Task TestRetrieveShipmentInvoiceReport()
        {
            await TestRetrieveReport("retrieve_shipment_invoice_report", "shipment_invoice");
        }

        [TestMethod]
        public async Task TestRetrieveTrackerReport()
        {
            await TestRetrieveReport("retrieve_tracker_report", "tracker");
        }

        [TestMethod]
        public async Task TestAll()
        {
            VCR.Replay("all");

            ReportCollection reportCollection = await Report.All("shipment", new Dictionary<string, object>
            {
                {
                    "page_size", Fixture.PageSize
                }
            });

            List<Report> reports = reportCollection.reports;

            Assert.IsTrue(reports.Count <= Fixture.PageSize);
            Assert.IsNotNull(reportCollection.has_more);
            foreach (var report in reports)
            {
                Assert.IsInstanceOfType(report, typeof(Report));
            }
        }
    }
}
