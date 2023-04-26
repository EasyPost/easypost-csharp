using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ReportService : EasyPostService
    {
        internal ReportService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Report.
        /// </summary>
        /// <param name="type">
        ///     The type of report, e.g. "shipment", "tracker", "payment_log", etc.
        /// </param>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"start_date", string} Date to start the report at.
        ///     * {"end_date", string} Date to end the report at.
        ///     * {"include_children", string} Whether or not to include child objects in the report.
        ///     * {"send_email", string} Whether or not to send the report via email.
        ///     * {"columns", List&lt;string&gt;} Specify the exact columns you want in your report.
        ///     * {"additional_columns", List&lt;string&gt;} Request additional columns (if any) outside of the defaults.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Report instance.</returns>
        [CrudOperations.Create]
        public async Task<Report> Create(string type, Dictionary<string, object>? parameters = null) => await Request<Report>(Method.Post, $"reports/{type}", parameters);

        [CrudOperations.Create]
        public async Task<Report> Create(string type, BetaFeatures.Parameters.Reports.Create parameters) => await Request<Report>(Method.Post, $"reports/{type}", parameters.ToDictionary());

        /// <summary>
        ///     Get a paginated list of reports.
        /// </summary>
        /// <param name="type">The type of report, e.g. "shipment", "tracker", "payment_log", etc.</param>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a Report ID. Only retrieve ScanForms created before this id. Takes
        ///     precedence over after_id.
        ///     * {"after_id", string} String representing a Report ID. Only retrieve ScanForms created after this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<ReportCollection> All(string type, Dictionary<string, object>? parameters = null)
        {
            ReportCollection reportCollection = await Request<ReportCollection>(Method.Get, $"reports/{type}", parameters);
            reportCollection.Type = type;
            return reportCollection;
        }

        [CrudOperations.Read]
        public async Task<ReportCollection> All(string type, BetaFeatures.Parameters.Reports.All parameters)
        {
            ReportCollection reportCollection = await Request<ReportCollection>(Method.Get, $"reports/{type}", parameters.ToDictionary());
            reportCollection.Type = type;
            return reportCollection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ReportCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="ReportCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="ReportCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<ReportCollection> GetNextPage(ReportCollection collection, int? pageSize = null) => await collection.GetNextPage<ReportCollection, BetaFeatures.Parameters.Reports.All>(async parameters => await All(collection.Type!, parameters), collection.Reports, pageSize);

        /// <summary>
        ///     Retrieve a Report from its id.
        /// </summary>
        /// <param name="id">String representing a report.</param>
        /// <returns>EasyPost.Report instance.</returns>
        [CrudOperations.Read]
        public async Task<Report> Retrieve(string id) => await Request<Report>(Method.Get, $"reports/{id}");

        /// <summary>
        ///     Retrieve a Report from its id and type.
        /// </summary>
        /// <param name="type">Type of report, e.g. shipment, tracker, payment_log, etc.</param>
        /// <param name="id">String representing a report.</param>
        /// <returns>EasyPost.Report instance.</returns>
        [CrudOperations.Read]
        public async Task<Report> Retrieve(string type, string id) => await Request<Report>(Method.Get, $"reports/{type}/{id}");

        #endregion
    }
}
