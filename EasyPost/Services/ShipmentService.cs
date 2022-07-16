using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Calculation;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.API;
using EasyPost.Parameters;

namespace EasyPost.Services
{
    public class ShipmentService : EasyPostService
    {
        internal ShipmentService(Client client) : base(client)
        {
        }

        /// <summary>
        ///     Get a paginated list of shipments.
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
        /// <returns>An EasyPost.ShipmentCollection instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<ShipmentCollection> All(All? parameters = null)
        {
            ShipmentCollection shipmentCollection = await List<ShipmentCollection>("shipments", parameters);
            shipmentCollection.Filters = parameters;
            shipmentCollection.Client = Client;
            return shipmentCollection;
        }

        /// <summary>
        ///     Create a Shipment.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the shipment with. Valid pairs:
        ///     * {"from_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"to_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"buyer_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"return_address", Dictionary&lt;string, object&gt;} See Address.Create for a list of valid keys.
        ///     * {"parcel", Dictionary&lt;string, object&gt;} See Parcel.Create for list of valid keys.
        ///     * {"customs_info", Dictionary&lt;string, object&gt;} See CustomsInfo.Create for list of valid keys.
        ///     * {"options", Dictionary&lt;string, object&gt;} See https://www.easypost.com/docs/api#shipments for list of
        ///     options.
        ///     * {"is_return", bool}
        ///     * {"currency", string} Defaults to "USD".
        ///     * {"reference", string}
        ///     * {"carrier_accounts", List&lt;string&gt;} List of CarrierAccount.id to limit rating.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Shipment> Create(Shipments.Create parameters)
        {
            return await Create<Shipment>("shipments", parameters);
        }

        /// <summary>
        ///     Create a return shipment for a shipment.
        /// </summary>
        /// <param name="shipment">Shipment to return.</param>
        /// <returns>A return shipment.</returns>
        /// <exception cref="PropertyMissingException">Required properties of the object are missing.</exception>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Shipment> CreateReturn(Shipment shipment)
        {
            if (shipment.Id == null)
            {
                throw new PropertyMissingException("id");
            }

            return await Create(new Shipments.Create
            {
                ToAddress = shipment.FromAddress,
                FromAddress = shipment.ToAddress,
                Parcel = shipment.Parcel,
                IsReturn = true
            });
        }

        /// <summary>
        ///     Retrieve a Shipment from its id.
        /// </summary>
        /// <param name="id">String representing a Shipment. Starts with "shp_".</param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Shipment> Retrieve(string id)
        {
            return await Get<Shipment>($"shipments/{id}");
        }

        /// <summary>
        ///     Get the lowest smartrate from a list of smartrates.
        /// </summary>
        /// <param name="smartrates">List of smartrates to filter.</param>
        /// <param name="deliveryDays">Delivery days restriction to use when filtering.</param>
        /// <param name="deliveryAccuracy">Delivery days accuracy restriction to use when filtering.</param>
        /// <returns>Lowest EasyPost.Smartrate object instance.</returns>
        public static Smartrate GetLowestSmartrate(IEnumerable<Smartrate> smartrates, int deliveryDays, SmartrateAccuracy deliveryAccuracy)
        {
            return Rates.GetLowestShipmentSmartrate(smartrates, deliveryDays, deliveryAccuracy);
        }
    }
}
