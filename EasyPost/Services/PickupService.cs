using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using RestSharp;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class PickupService : EasyPostService
    {
        internal PickupService(EasyPostClient client) : base(client)
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
            return await Request<Pickup>(Method.Post, "pickups", parameters);
        }

        /// <summary>
        ///     Retrieve a Pickup from its id.
        /// </summary>
        /// <param name="id">String representing a Pickup. Starts with "pickup_".</param>
        /// <returns>EasyPost.Pickup instance.</returns>
        [CrudOperations.Read]
        public async Task<Pickup> Retrieve(string id)
        {
            return await Request<Pickup>(Method.Get, $"pickups/{id}");
        }

        /// <summary>
        ///     Purchase this pickup.
        /// </summary>
        /// <param name="withCarrier">The name of the carrier to purchase with.</param>
        /// <param name="withService">The name of the service to purchase.</param>
        [CrudOperations.Update]
        public async Task<Pickup> Buy(string id, string withCarrier, string withService)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "carrier", withCarrier },
                { "service", withService }
            };

            return await Request<Pickup>(Method.Post, $"pickups/{id}/buy", parameters);
        }

        /// <summary>
        ///     Cancel this pickup.
        /// </summary>
        [CrudOperations.Update]
        public async Task<Pickup> Cancel(string id)
        {
            return await Request<Pickup>(Method.Post, $"pickups/{id}/cancel");
        }

        /// <summary>
        ///     Get the lowest rate for this Pickup.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.PickupRate object instance.</returns>
        public PickupRate LowestRate(IEnumerable<Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            return (PickupRate)Calculation.Rates.GetLowestObjectRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }

        /// <summary>
        ///     Get the lowest rate for this Pickup.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.PickupRate object instance.</returns>
        public PickupRate LowestRate(Pickup pickup, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            if (pickup.PickupRates == null)
            {
                throw new MissingPropertyError(pickup, "PickupRates");
            }
            return LowestRate(pickup.PickupRates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }

        #endregion
    }
}
