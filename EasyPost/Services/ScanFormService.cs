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
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ScanFormService : EasyPostService
    {
        internal ScanFormService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a ScanForm.
        /// </summary>
        /// <param name="shipments">Shipments to be associated with the ScanForm. Only id is required.</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        [CrudOperations.Create]
        public async Task<ScanForm> Create(List<Shipment> shipments, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "shipments", shipments } };
            return await RequestAsync<ScanForm>(Method.Post, "scan_forms", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="ScanForm"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.ScanForms.Create"/> parameter set.</param>
        /// <returns><see cref="ScanForm"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<ScanForm> Create(BetaFeatures.Parameters.ScanForms.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<ScanForm>(Method.Post, "scan_forms", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Get a paginated list of scan forms.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing a ScanForm ID. Starts with "sf_". Only retrieve ScanForms created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve ScanForms created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.ScanFormCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<ScanFormCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            ScanFormCollection collection = await RequestAsync<ScanFormCollection>(Method.Get, "scan_forms", cancellationToken, parameters);
            collection.Filters = BetaFeatures.Parameters.ScanForms.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="ScanForm"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.ScanForms.All"/> parameter set.</param>
        /// <returns><see cref="ScanFormCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ScanFormCollection> All(BetaFeatures.Parameters.ScanForms.All parameters, CancellationToken cancellationToken = default)
        {
            ScanFormCollection collection = await RequestAsync<ScanFormCollection>(Method.Get, "scan_forms", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ScanFormCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="ScanFormCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="ScanFormCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<ScanFormCollection> GetNextPage(ScanFormCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<ScanFormCollection, BetaFeatures.Parameters.ScanForms.All>(async parameters => await All(parameters, cancellationToken), collection.ScanForms, pageSize);

        /// <summary>
        ///     Retrieve a ScanForm from its id.
        /// </summary>
        /// <param name="id">String representing a scan form, starts with "sf_".</param>
        /// <returns>EasyPost.ScanForm instance.</returns>
        [CrudOperations.Read]
        public async Task<ScanForm> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<ScanForm>(Method.Get, $"scan_forms/{id}", cancellationToken);

        #endregion
    }
}
