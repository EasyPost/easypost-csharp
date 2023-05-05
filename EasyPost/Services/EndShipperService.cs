using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#endshipper">EndShipper-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EndShipperService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EndShipperService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal EndShipperService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create an <see cref="EndShipper"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-an-endshipper">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="EndShipper"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="EndShipper"/> object.</returns>
        [CrudOperations.Create]
        public async Task<EndShipper> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("address");
            return await RequestAsync<EndShipper>(Method.Post, "end_shippers", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create an <see cref="EndShipper"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-an-endshipper">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="EndShipper"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="EndShipper"/> object.</returns>
        [CrudOperations.Create]
        public async Task<EndShipper> Create(BetaFeatures.Parameters.EndShippers.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<EndShipper>(Method.Post, "end_shippers", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all EndShipper objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an EndShipper ID. Starts with "es_". Only retrieve EndShippers created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an EndShipper ID. Starts with "es". Only retrieve EndShippers created
        ///     after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve EndShippers created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve EndShippers created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An EasyPost.EndShipperCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<EndShipperCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            EndShipperCollection collection = await RequestAsync<EndShipperCollection>(Method.Get, "end_shippers", cancellationToken, parameters);
            collection.Filters = BetaFeatures.Parameters.EndShippers.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="EndShipper"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.EndShippers.All"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="EndShipperCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<EndShipperCollection> All(BetaFeatures.Parameters.EndShippers.All parameters, CancellationToken cancellationToken = default)
        {
            EndShipperCollection collection = await RequestAsync<EndShipperCollection>(Method.Get, "end_shippers", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        // TODO: Add GetNextPage function when "before_id" available for EndShipper All endpoint.

        /// <summary>
        ///     Retrieve an EndShipper from its id.
        /// </summary>
        /// <param name="id">String representing an EndShipper. Starts with "es_".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>EasyPost.EndShipper instance.</returns>
        [CrudOperations.Read]
        public async Task<EndShipper> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<EndShipper>(Method.Get, $"end_shippers/{id}", cancellationToken);

        /// <summary>
        ///     Update an <see cref="EndShipper"/>.
        /// </summary>
        /// <param name="parameters">Data to update <see cref="EndShipper"/> with.</param>
        /// <param name="id">ID of the <see cref="EndShipper"/> to update.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="EndShipper"/>.</returns>
        [CrudOperations.Update]
        public async Task<EndShipper> Update(string id, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("address");

            return await RequestAsync<EndShipper>(Method.Put, $"end_shippers/{id}", cancellationToken, parameters);
        }

        /// <summary>
        ///     Update an <see cref="EndShipper"/>.
        /// </summary>
        /// <param name="parameters">Data to update <see cref="EndShipper"/> with.</param>
        /// <param name="id">ID of the <see cref="EndShipper"/> to update.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="CarrierAccount"/>.</returns>
        [CrudOperations.Update]
        public async Task<EndShipper> Update(string id, BetaFeatures.Parameters.EndShippers.Update parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<EndShipper>(Method.Put, $"end_shippers/{id}", cancellationToken, parameters.ToDictionary());
        }

        #endregion
    }
}
