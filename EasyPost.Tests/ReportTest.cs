using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EasyPost.Tests
{
    [TestClass]
    public class ReportTest
    {
        private TestUtils.VCR _vcr;

        [TestInitialize]
        public void Initialize()
        {
            _vcr = new TestUtils.VCR("report");
        }

        private static async Task<Report> CreateBasicReport(string reportType, V2Client client)
        {
            return await client.Reports.Create(reportType, new Dictionary<string, object>
            {
                {
                    "start_date", Fixture.ReportDate
                },
                {
                    "end_date", Fixture.ReportDate
                }
            });
        }

        private static async Task<Report> CreateAdvancedReport(string reportType, Dictionary<string, object> parameters, V2Client client)
        {
            parameters["start_date"] = Fixture.ReportDate;
            parameters["end_date"] = Fixture.ReportDate;
            return await client.Reports.Create(reportType, parameters);
        }

        [TestMethod]
        public async Task TestCreateReport()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("create_report");
            Report report = await CreateBasicReport(Fixture.ReportType, client);

            Assert.IsInstanceOfType(report, typeof(Report));
            Assert.IsTrue(report.id.StartsWith(Fixture.ReportIdPrefix));
        }

        [TestMethod]
        public async Task TestCreateReportWithColumns()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("create_report_with_columns");

            List<string> columns = new List<string>
            {
                "usps_zone"
            };
            Report report = await CreateAdvancedReport("shipment", new Dictionary<string, object>
            {
                {
                    "columns", columns
                }
            }, client);

            // verify parameters by checking VCR cassette for correct URL
            // Some reports take a long time to generate, so we won't be able to consistently pull the report
            // There's unfortunately no way to check if the columns were included in the final report without parsing the CSV
            // so we assume, if we haven't gotten an error by this point, we've made the API calls correctly
            // any failure at this point is a server-side issue
            Assert.IsInstanceOfType(report, typeof(Report));
        }

        [TestMethod]
        public async Task TestCreateReportWithAdditionalColumns()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("create_report_with_additional_columns");

            List<string> additionalColumns = new List<string>
            {
                "from_name",
                "from_company",
            };
            Report report = await CreateAdvancedReport("shipment", new Dictionary<string, object>
            {
                {
                    "additional_columns", additionalColumns
                }
            }, client);

            // verify parameters by checking VCR cassette for correct URL
            // Some reports take a long time to generate, so we won't be able to consistently pull the report
            // There's unfortunately no way to check if the columns were included in the final report without parsing the CSV
            // so we assume, if we haven't gotten an error by this point, we've made the API calls correctly
            // any failure at this point is a server-side issue
            Assert.IsInstanceOfType(report, typeof(Report));
        }

        [TestMethod]
        public async Task TestRetrieveReport()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("retrieve_report");

            Report report = await CreateBasicReport(Fixture.ReportType, client);

            Report retrievedReport = await client.Reports.Retrieve(report.id);

            Assert.IsInstanceOfType(retrievedReport, typeof(Report));
            Assert.AreEqual(report.start_date, retrievedReport.start_date);
            Assert.AreEqual(report.end_date, retrievedReport.end_date);
        }

        [TestMethod]
        public async Task TestAll()
        {
            V2Client client = (V2Client)_vcr.SetUpTest("all");

            ReportCollection reportCollection = await client.Reports.All("shipment", new Dictionary<string, object>
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
