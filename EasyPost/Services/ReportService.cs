using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#reports">report-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ReportService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReportService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal ReportService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="Report"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-report">Related API documentation</a>.
        /// </summary>
        /// <param name="type">Type of <see cref="Report"/> to create.</param>
        /// <param name="parameters">Data to use to create the <see cref="Report"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Report"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<Report> Create(string type, Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default) => await RequestAsync<Report>(Method.Post, $"reports/{type}", cancellationToken, parameters);

        /// <summary>
        ///     Create a <see cref="Report"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-report">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Report"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Report"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<Report> Create(BetaFeatures.Parameters.Reports.Create parameters, CancellationToken cancellationToken = default)
        {
            if (parameters.Type == null)
            {
                throw new MissingParameterError("Type");
            }

            return await RequestAsync<Report>(Method.Post, $"reports/{parameters.Type}", cancellationToken, parameters.ToDictionary());
        }

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
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An EasyPost.ReportCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<ReportCollection> All(string type, Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            ReportCollection collection = await RequestAsync<ReportCollection>(Method.Get, $"reports/{type}", cancellationToken, parameters);
            // Copy the report type into the dictionary before we store the dictionary in the collection
            parameters ??= new Dictionary<string, object>();
            parameters["type"] = type;
            collection.Filters = BetaFeatures.Parameters.Reports.All.FromDictionary(parameters);
            return collection;
        }

        [CrudOperations.Read]
        public async Task<ReportCollection> All(BetaFeatures.Parameters.Reports.All parameters, CancellationToken cancellationToken = default)
        {
            if (parameters.Type == null)
            {
                throw new MissingParameterError("Type");
            }

            ReportCollection collection = await RequestAsync<ReportCollection>(Method.Get, $"reports/{parameters.Type}", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ReportCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="ReportCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="ReportCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        // Reuse the same report type as the current page of the collection (will not be null)
        public async Task<ReportCollection> GetNextPage(ReportCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<ReportCollection, BetaFeatures.Parameters.Reports.All>(async parameters => await All(parameters, cancellationToken), collection.Reports, pageSize);

        /// <summary>
        ///     Retrieve a Report from its id.
        /// </summary>
        /// <param name="id">String representing a report.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Report instance.</returns>
        [CrudOperations.Read]
        public async Task<Report> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Report>(Method.Get, $"reports/{id}", cancellationToken);

        /// <summary>
        ///     Retrieve a Report from its id and type.
        /// </summary>
        /// <param name="type">Type of report, e.g. shipment, tracker, payment_log, etc.</param>
        /// <param name="id">String representing a report.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.Report instance.</returns>
        [CrudOperations.Read]
        public async Task<Report> Retrieve(string type, string id, CancellationToken cancellationToken = default) => await RequestAsync<Report>(Method.Get, $"reports/{type}/{id}", cancellationToken);

        #endregion
    }
}
