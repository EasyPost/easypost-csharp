using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Parameters.ScanForms;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#scan-form">scan form-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ScanFormService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ScanFormService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal ScanFormService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="ScanForm"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-scanform">Related API documentation</a>.
        /// </summary>
        /// <param name="shipments"><see cref="Shipment"/>s to create a <see cref="ScanForm"/> for.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ScanForm"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<ScanForm> Create(List<Shipment> shipments, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new() { { "shipments", shipments } };
            return await RequestAsync<ScanForm>(Method.Post, "scan_forms", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="ScanForm"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-scanform">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="ScanForm"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ScanForm"/> objects.</returns>
        [CrudOperations.Create]
        public async Task<ScanForm> Create(Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<ScanForm>(Method.Post, "scan_forms", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all <see cref="ScanForm"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retreive-a-list-of-scanforms">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="ScanForm"/>s on.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ScanFormCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ScanFormCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            ScanFormCollection collection = await RequestAsync<ScanFormCollection>(Method.Get, "scan_forms", cancellationToken, parameters);
            collection.Filters = Parameters.ScanForms.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="ScanForm"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retreive-a-list-of-scanforms">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Parameters to filter the list of <see cref="ScanForm"/>s on.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="ScanFormCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ScanFormCollection> All(All parameters, CancellationToken cancellationToken = default)
        {
            ScanFormCollection collection = await RequestAsync<ScanFormCollection>(Method.Get, "scan_forms", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ScanFormCollection"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-the-next-page-of-a-list-of-scanforms">Related API documentation</a>.
        /// </summary>
        /// <param name="collection">The <see cref="ScanFormCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="ScanFormCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<ScanFormCollection> GetNextPage(ScanFormCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<ScanFormCollection, All>(async parameters => await All(parameters, cancellationToken), collection.ScanForms, pageSize);

        /// <summary>
        ///     Retrieve a <see cref="ScanForm"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-scanform">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="ScanForm"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The requested <see cref="ScanForm"/>.</returns>
        [CrudOperations.Read]
        public async Task<ScanForm> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<ScanForm>(Method.Get, $"scan_forms/{id}", cancellationToken);

        #endregion
    }
}
