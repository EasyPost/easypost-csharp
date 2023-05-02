using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Tests._Utilities;
using EasyPost.Tests._Utilities.Attributes;
using EasyPost.Utilities.Internal.Attributes;
using Xunit;

namespace EasyPost.Tests.BetaFeaturesTests.ServicesTests
{
    public class ReportServiceTests : UnitTest
    {
        public ReportServiceTests() : base("report_service_with_parameters")
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

            Dictionary<string, object> data = new Dictionary<string, object>
            {
                { "start_date", Fixtures.ReportDate },
                { "end_date", Fixtures.ReportDate },
                { "type", Fixtures.ReportType },
            };

            BetaFeatures.Parameters.Reports.Create parameters = Fixtures.Parameters.Reports.Create(data);

            Report report = await Client.Report.Create(parameters);

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

            Dictionary<string, object> data = new()
            {
                { "additional_columns", additionalColumns },
                { "start_date", Fixtures.ReportDate },
                { "end_date", Fixtures.ReportDate },
                { "type", "shipment" },
            };

            BetaFeatures.Parameters.Reports.Create parameters = Fixtures.Parameters.Reports.Create(data);

            Report report = await Client.Report.Create(parameters);

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

            Dictionary<string, object> data = new()
            {
                { "columns", columns },
                { "start_date", Fixtures.ReportDate },
                { "end_date", Fixtures.ReportDate },
                { "type", "shipment" },
            };

            BetaFeatures.Parameters.Reports.Create parameters = Fixtures.Parameters.Reports.Create(data);

            Report report = await Client.Report.Create(parameters);

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

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize }, { "type", "shipment" } };

            BetaFeatures.Parameters.Reports.All parameters = Fixtures.Parameters.Reports.All(data);

            ReportCollection reportCollection = await Client.Report.All(parameters);

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

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize }, { "type", type } };

            BetaFeatures.Parameters.Reports.All parameters = Fixtures.Parameters.Reports.All(data);

            ReportCollection reportCollection = await Client.Report.All(parameters);

            Assert.Equal(type, ((BetaFeatures.Parameters.Reports.All)reportCollection.Filters!).Type);
        }

        [Fact]
        [CrudOperations.Read]
        [Testing.Exception]
        public async Task TestMissingTypeWhenCallingAll()
        {
            UseVCR("missing_type_when_calling_all");

            Dictionary<string, object> data = new Dictionary<string, object>() { { "page_size", Fixtures.PageSize } }; // missing type

            BetaFeatures.Parameters.Reports.All parameters = Fixtures.Parameters.Reports.All(data);

            await Assert.ThrowsAsync<MissingParameterError>(() => Client.Report.All(parameters));
        }

        #endregion

        #endregion
    }
}
