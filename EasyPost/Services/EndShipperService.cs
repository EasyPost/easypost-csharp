using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Parameters.EndShippers;
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
        public async Task<EndShipper> Create(Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<EndShipper>(Method.Post, "end_shippers", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     List all <see cref="EndShipper"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-endshippers">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">A dictionary containing parameters to filter the list with. See <see cref="Parameters.EndShippers.All"/> for valid keys.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="EndShipperCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<EndShipperCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            EndShipperCollection collection = await RequestAsync<EndShipperCollection>(Method.Get, "end_shippers", cancellationToken, parameters);
            collection.Filters = Parameters.EndShippers.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="EndShipper"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-endshippers">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.EndShippers.All"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="EndShipperCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<EndShipperCollection> All(All parameters, CancellationToken cancellationToken = default)
        {
            EndShipperCollection collection = await RequestAsync<EndShipperCollection>(Method.Get, "end_shippers", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        // TODO: Add GetNextPage function when "before_id" available for EndShipper All endpoint.

        /// <summary>
        ///     Retrieve an <see cref="EndShipper"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-an-endshipper">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="EndShipper"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The <see cref="EndShipper"/>.</returns>
        [CrudOperations.Read]
        public async Task<EndShipper> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<EndShipper>(Method.Get, $"end_shippers/{id}", cancellationToken);

        /// <summary>
        ///     Update an <see cref="EndShipper"/>.
        ///     <a href="https://www.easypost.com/docs/api#update-an-endshipper">Related API documentation</a>.
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
        ///     <a href="https://www.easypost.com/docs/api#update-an-endshipper">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to update <see cref="EndShipper"/> with.</param>
        /// <param name="id">ID of the <see cref="EndShipper"/> to update.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The updated <see cref="EndShipper"/>.</returns>
        [CrudOperations.Update]
        public async Task<EndShipper> Update(string id, Update parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<EndShipper>(Method.Put, $"end_shippers/{id}", cancellationToken, parameters.ToDictionary());
        }

        #endregion
    }
}
