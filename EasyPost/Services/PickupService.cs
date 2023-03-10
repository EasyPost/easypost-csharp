using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
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
        public async Task<Pickup> Create(Dictionary<string, object> parameters)
        {
            parameters = parameters.Wrap("pickup");
            return await Create<Pickup>("pickups", parameters);
        }

        [CrudOperations.Create]
        public async Task<Pickup> Create(BetaFeatures.Parameters.Pickups.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Create<Pickup>("pickups", parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        [CrudOperations.Read]
        public async Task<Pickup> Retrieve(string id) => await Get<Pickup>($"pickups/{id}");

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
        public async Task<PickupCollection> All(Dictionary<string, object>? parameters = null)
        {
            PickupCollection pickupCollection = await List<PickupCollection>("pickups", parameters);
            return pickupCollection;
        }

        [CrudOperations.Read]
        public async Task<PickupCollection> All(BetaFeatures.Parameters.Pickups.All parameters)
        {
            return await All(parameters.ToDictionary());
        }

        #endregion
    }
}
