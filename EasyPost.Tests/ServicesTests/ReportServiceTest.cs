using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Annotations;
using EasyPost.Utilities.Annotations;
using Xunit;

namespace EasyPost.Tests.ServicesTests
{
    public class ReportServiceTests : UnitTest
    {
        public ReportServiceTests() : base("report_service")
        {
        }

        #region Tests

        #region Test CRUD Operations

        [Fact]
        [CrudOperations.Create]
        [Testing.Function]
        public async Task TestCreate()
        {
            UseVCR("create");

            Report report = await CreateBasicReport(Fixtures.ReportType);

            Assert.IsType<Report>(report);
            Assert.StartsWith(Fixtures.ReportIdPrefix, report.Id);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateWithAdditionalColumns()
        {
            UseVCR("create_with_additional_columns");

            List<string> additionalColumns = new()
            {
                "from_name",
                "from_company"
            };
            Report report = await CreateAdvancedReport("shipment", new Dictionary<string, object> { { "additional_columns", additionalColumns } });

            // verify parameters by checking VCR cassette for correct URL
            // Some reports take a long time to generate, so we won't be able to consistently pull the report
            // There's unfortunately no way to check if the columns were included in the final report without parsing the CSV
            // so we assume, if we haven't gotten an error by this point, we've made the API calls correctly
            // any failure at this point is a server-side issue
            Assert.IsType<Report>(report);
        }

        [Fact]
        [CrudOperations.Create]
        [Testing.Parameters]
        public async Task TestCreateWithColumns()
        {
            UseVCR("create_with_columns");

            List<string> columns = new() { "usps_zone" };
            Report report = await CreateAdvancedReport("shipment", new Dictionary<string, object> { { "columns", columns } });

            // verify parameters by checking VCR cassette for correct URL
            // Some reports take a long time to generate, so we won't be able to consistently pull the report
            // There's unfortunately no way to check if the columns were included in the final report without parsing the CSV
            // so we assume, if we haven't gotten an error by this point, we've made the API calls correctly
            // any failure at this point is a server-side issue
            Assert.IsType<Report>(report);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestAll()
        {
            UseVCR("all");

            ReportCollection reportCollection = await Client.Report.All("shipment", new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            List<Report> reports = reportCollection.Reports;

            Assert.True(reportCollection.HasMore);
            Assert.True(reports.Count <= Fixtures.PageSize);
            foreach (Report report in reports)
            {
                Assert.IsType<Report>(report);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Report report = await CreateBasicReport(Fixtures.ReportType);

            // retrieve by ID
            Report retrievedReport = await Client.Report.Retrieve(report.Id);

            Assert.IsType<Report>(retrievedReport);
            Assert.Equal(report.StartDate, retrievedReport.StartDate);
            Assert.Equal(report.EndDate, retrievedReport.EndDate);

            // retrieve by ID and type
            retrievedReport = await Client.Report.Retrieve(Fixtures.ReportType, report.Id);

            Assert.IsType<Report>(retrievedReport);
            Assert.Equal(report.StartDate, retrievedReport.StartDate);
            Assert.Equal(report.EndDate, retrievedReport.EndDate);
        }

        #endregion

        #endregion

        private async Task<Report> CreateAdvancedReport(string reportType, Dictionary<string, object> parameters)
        {
            parameters["start_date"] = Fixtures.ReportDate;
            parameters["end_date"] = Fixtures.ReportDate;
            return await Client.Report.Create(reportType, parameters);
        }

        private async Task<Report> CreateBasicReport(string reportType) =>
            await Client.Report.Create(reportType, new Dictionary<string, object>
            {
                { "start_date", Fixtures.ReportDate },
                { "end_date", Fixtures.ReportDate }
            });
    }
}
