using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Models.API;
using EasyPost.Parameters;
using Xunit;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace EasyPost.Tests
{
    public class ReportTest : UnitTest
    {
        public ReportTest() : base("report")
        {
        }

        [Fact]
        public async Task TestAll()
        {
            UseVCR("all", ApiVersion.Latest);

            ReportCollection reportCollection = await Client.Reports.All("shipment", new All
            {
                PageSize = Fixture.PageSize
            });

            List<Report> reports = reportCollection.Reports;

            Assert.IsTrue(reports.Count <= Fixture.PageSize);
            Assert.IsNotNull(reportCollection.HasMore);
            foreach (Report report in reports)
            {
                Assert.IsInstanceOfType(report, typeof(Report));
            }
        }

        [Fact]
        public async Task TestCreateReport()
        {
            UseVCR("create_report", ApiVersion.Latest);

            Report report = await CreateBasicReport(Fixture.ReportType);

            Assert.IsInstanceOfType(report, typeof(Report));
            Assert.IsTrue(report.Id.StartsWith(Fixture.ReportIdPrefix));
        }

        [Fact]
        public async Task TestCreateReportWithAdditionalColumns()
        {
            UseVCR("create_report_with_additional_columns", ApiVersion.Latest);

            List<string> additionalColumns = new List<string>()
            {
                "from_name",
                "from_company"
            };
            Report report = await CreateAdvancedReport("shipment", new Dictionary<string, object>
            {
                {
                    "additional_columns", additionalColumns
                }
            });

            // verify parameters by checking VCR cassette for correct URL
            // Some reports take a long time to generate, so we won't be able to consistently pull the report
            // There's unfortunately no way to check if the columns were included in the final report without parsing the CSV
            // so we assume, if we haven't gotten an error by this point, we've made the API calls correctly
            // any failure at this point is a server-side issue
            Assert.IsInstanceOfType(report, typeof(Report));
        }

        [Fact]
        public async Task TestCreateReportWithColumns()
        {
            UseVCR("create_report_with_columns", ApiVersion.Latest);

            List<string> columns = new List<string>()
            {
                "usps_zone"
            };
            Report report = await CreateAdvancedReport("shipment", new Dictionary<string, object>
            {
                {
                    "columns", columns
                }
            });

            // verify parameters by checking VCR cassette for correct URL
            // Some reports take a long time to generate, so we won't be able to consistently pull the report
            // There's unfortunately no way to check if the columns were included in the final report without parsing the CSV
            // so we assume, if we haven't gotten an error by this point, we've made the API calls correctly
            // any failure at this point is a server-side issue
            Assert.IsInstanceOfType(report, typeof(Report));
        }

        [Fact]
        public async Task TestRetrieveReport()
        {
            UseVCR("retrieve_report", ApiVersion.Latest);

            Report report = await CreateBasicReport(Fixture.ReportType);

            Report retrievedReport = await Client.Reports.Retrieve(report.Id);

            Assert.IsInstanceOfType(retrievedReport, typeof(Report));
            Assert.AreEqual(report.StartDate, retrievedReport.StartDate);
            Assert.AreEqual(report.EndDate, retrievedReport.EndDate);
        }

        private async Task<Report> CreateAdvancedReport(string reportType, Dictionary<string, object> parameters)
        {
            return await Client.Reports.Create(reportType, new Reports.Create(parameters)
            {
                StartDate = Fixture.ReportDate,
                EndDate = Fixture.ReportDate,
            });
        }

        private async Task<Report> CreateBasicReport(string reportType) =>
            await Client.Reports.Create(reportType, new Reports.Create
            {
                StartDate = Fixture.ReportDate,
                EndDate = Fixture.ReportDate,
            });
    }
}
