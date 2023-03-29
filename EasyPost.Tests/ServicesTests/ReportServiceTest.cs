using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
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

            Report report = await Client.Report.Create(Fixtures.ReportType, new Dictionary<string, object>
            {
                { "start_date", Fixtures.ReportDate },
                { "end_date", Fixtures.ReportDate }
            });

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

            Dictionary<string, object> parameters = new()
            {
                { "additional_columns", additionalColumns },
                { "start_date", Fixtures.ReportDate },
                { "end_date", Fixtures.ReportDate },
            };
            Report report = await Client.Report.Create("shipment", parameters);

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

            Dictionary<string, object> parameters = new()
            {
                { "columns", columns },
                { "start_date", Fixtures.ReportDate },
                { "end_date", Fixtures.ReportDate },
            };
            Report report = await Client.Report.Create("shipment", parameters);

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

            Assert.True(reports.Count <= Fixtures.PageSize);
            foreach (Report report in reports)
            {
                Assert.IsType<Report>(report);
            }
        }

        /// <summary>
        ///     This test confirms that the parameters used to filter the results of the All() method are passed through to the resulting collection object.
        /// </summary>
        [Fact]
        [CrudOperations.Read]
        [Testing.Parameters]
        public async Task TestAllParameterHandOff()
        {
            UseVCR("all_parameter_hand_off");

            const string type = "shipment";

            Dictionary<string, object> filters = new Dictionary<string, object> { { "page_size", Fixtures.PageSize } };

            ReportCollection reportCollection = await Client.Report.All(type, filters);

            Assert.Equal(type, reportCollection.Type);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestGetNextPage()
        {
            UseVCR("get_next_page");

            ReportCollection collection = await Client.Report.All("shipment", new Dictionary<string, object> { { "page_size", Fixtures.PageSize } });

            try
            {
                ReportCollection nextPageCollection = await Client.Report.GetNextPage(collection);

                // If the first ID in the next page is the same as the first ID in the current page, then we didn't get the next page
                Assert.NotEqual(collection.Reports[0].Id, nextPageCollection.Reports[0].Id);
            }
            catch (EndOfPaginationError e) // There's no second page, that's not a failure
            {
                Assert.True(true);
            }
            catch // Any other exception is a failure
            {
                Assert.True(false);
            }
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Function]
        public async Task TestRetrieve()
        {
            UseVCR("retrieve");

            Report report = await Client.Report.Create(Fixtures.ReportType, new Dictionary<string, object>
            {
                { "start_date", Fixtures.ReportDate },
                { "end_date", Fixtures.ReportDate }
            });

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
    }
}
