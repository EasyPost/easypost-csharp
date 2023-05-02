using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PickupService : EasyPostService
    {
        internal PickupService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Create a Pickup.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the batch with. Valid pairs:
        ///     * {"is_account_address", bool}
        ///     * {"min_datetime", DateTime}
        ///     * {"max_datetime", DateTime}
        ///     * {"reference", string}
        ///     * {"instructions", string}
        ///     * {"carrier_accounts", List&lt;CarrierAccount&gt;}
        ///     * {"address", Address}
        ///     * {"shipment", Shipment}
        ///     * {"batch", Batch}
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Pickup instance.</returns>
        [CrudOperations.Create]
        public async Task<Pickup> Create(Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            parameters = parameters.Wrap("pickup");
            return await RequestAsync<Pickup>(Method.Post, "pickups", cancellationToken, parameters);
        }

        /// <summary>
        ///     Create a <see cref="Pickup"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Pickups.Create"/> parameter set.</param>
        /// <returns><see cref="Pickup"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Pickup> Create(BetaFeatures.Parameters.Pickups.Create parameters, CancellationToken cancellationToken = default)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await RequestAsync<Pickup>(Method.Post, "pickups", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        [CrudOperations.Read]
        public async Task<Pickup> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Pickup>(Method.Get, $"pickups/{id}", cancellationToken);

        /// <summary>
        ///     Get a paginated list of <see cref="Pickup"/>s.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing a Shipment. Starts with "shp_". Only retrieve shipments created before
        ///     this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing a Shipment. Starts with "shp_". Only retrieve shipments created after
        ///     this id.
        ///     * {"start_datetime", DateTime} Starting time for the search.
        ///     * {"end_datetime", DateTime} Ending time for the search.
        ///     * {"page_size", int} Size of page. Default to 20.
        ///     * {"purchased", bool} If true only display purchased shipments.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An <see cref="PickupCollection"/> object.</returns>
        [CrudOperations.Read]
        public async Task<PickupCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            PickupCollection collection = await RequestAsync<PickupCollection>(Method.Get, "pickups", cancellationToken, parameters);
            collection.Filters = BaseAllParameters.FromDictionary<BetaFeatures.Parameters.Pickups.All>(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Pickup"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Pickups.All"/> parameter set.</param>
        /// <returns><see cref="PickupCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<PickupCollection> All(BetaFeatures.Parameters.Pickups.All parameters, CancellationToken cancellationToken = default)
        {
            PickupCollection collection = await RequestAsync<PickupCollection>(Method.Get, "pickups", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="PickupCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="PickupCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="PickupCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<PickupCollection> GetNextPage(PickupCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<PickupCollection, BetaFeatures.Parameters.Pickups.All>(async parameters => await All(parameters, cancellationToken), collection.Pickups, pageSize);

        /// <summary>
        ///     Purchase this pickup.
        /// </summary>
        /// <param name="withCarrier">The name of the carrier to purchase with.</param>
        /// <param name="withService">The name of the service to purchase.</param>
        /// <returns>The updated Pickup.</returns>
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
        ///     Purchase this <see cref="Pickup"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Pickups.Buy"/> parameters set.</param>
        /// <returns>This updated <see cref="Pickup"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Pickup> Buy(string id, BetaFeatures.Parameters.Pickups.Buy parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Pickup>(Method.Post, $"pickups/{id}/buy", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Cancel this pickup.
        /// </summary>
        /// <returns>The updated Pickup.</returns>
        [CrudOperations.Update]
        public async Task<Pickup> Cancel(string id, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<Pickup>(Method.Post, $"pickups/{id}/cancel", cancellationToken);
        }

        #endregion
    }
}
