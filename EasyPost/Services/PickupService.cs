using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#pickups">pickup-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PickupService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="PickupService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal PickupService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a <see cref="Pickup"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-pickup">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Pickup"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Pickup"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Pickup> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("pickup");
            return await RequestAsync<Pickup>(Method.Post, "pickups", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Pickup"/>.
        ///     <a href="https://www.easypost.com/docs/api#create-a-pickup">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Data to use to create the <see cref="Pickup"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Pickup"/> object.</returns>
        [CrudOperations.Create]
        public async Task<Pickup> Create(Parameters.Pickup.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Pickup>(Method.Post, "pickups", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a <see cref="Pickup"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-pickup">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Pickup"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Pickup"/> object.</returns>
        [CrudOperations.Read]
        public async Task<Pickup> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Pickup>(Method.Get, $"pickups/{id}", cancellationToken);

        /// <summary>
        ///     List all <see cref="Pickup"/> objects.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-pickups">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">The parameters to filter the list of <see cref="Pickup"/> objects by.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="PickupCollection"/> object.</returns>
        [CrudOperations.Read]
        public async Task<PickupCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            PickupCollection collection = await RequestAsync<PickupCollection>(Method.Get, "pickups", cancellationToken, parameters);
            collection.Filters = Parameters.Pickup.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Pickup"/> objects.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-pickups">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">The parameters to filter the list of <see cref="Pickup"/> objects by.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="PickupCollection"/> object.</returns>
        [CrudOperations.Read]
        public async Task<PickupCollection> All(Parameters.Pickup.All parameters, CancellationToken cancellationToken = default)
        {
            PickupCollection collection = await RequestAsync<PickupCollection>(Method.Get, "pickups", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="PickupCollection"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-pickups">Related API documentation</a>.
        /// </summary>
        /// <param name="collection">The <see cref="PickupCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="PickupCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<PickupCollection> GetNextPage(PickupCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<PickupCollection, Parameters.Pickup.All>(async parameters => await All(parameters, cancellationToken), collection.Pickups, pageSize);

        /// <summary>
        ///     Purchase a <see cref="Pickup"/>.
        ///     <a href="https://www.easypost.com/docs/api#buy-a-pickup">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Pickup"/> to purchase.</param>
        /// <param name="withCarrier">The name of the carrier to purchase the <see cref="Pickup"/> with.</param>
        /// <param name="withService">The name of the service to purchase the <see cref="Pickup"/> with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Pickup"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Pickup> Buy(string id, string withCarrier, string withService, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                { "carrier", withCarrier },
                { "service", withService },
            };

            return await RequestAsync<Pickup>(Method.Post, $"pickups/{id}/buy", cancellationToken, parameters);
        }

        /// <summary>
        ///     Purchase a <see cref="Pickup"/>.
        ///     <a href="https://www.easypost.com/docs/api#buy-a-pickup">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Pickup"/> to purchase.</param>
        /// <param name="parameters">The parameters to purchase the <see cref="Pickup"/> with.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Pickup"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Pickup> Buy(string id, Parameters.Pickup.Buy parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Pickup>(Method.Post, $"pickups/{id}/buy", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Cancel a <see cref="Pickup"/>.
        ///     <a href="https://www.easypost.com/docs/api#cancel-a-pickup">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Pickup"/> to cancel.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An updated <see cref="Pickup"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Pickup> Cancel(string id, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Pickup>(Method.Post, $"pickups/{id}/cancel", cancellationToken);
        }

        #endregion
    }
}
