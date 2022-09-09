using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Calculation;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Services
{
    public class ShipmentService : EasyPostService
    {
        internal ShipmentService(EasyPostClient client) : base(client)
        {
        }

        #region CRUD Operations

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
        /// <param name="withCarbonOffset">Whether to use carbon offset when creating the shipment.</param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        [CrudOperations.Create]
        public async Task<Shipment> Create(Dictionary<string, object> parameters, bool withCarbonOffset = false)
        {
            parameters = parameters.Wrap("shipment");
            parameters.Add("carbon_offset", withCarbonOffset);
            return await Create<Shipment>("shipments", parameters);
        }

        /// <summary>
        ///     Create a return shipment for a shipment.
        ///     Uses the same To and From addresses as the original shipment by default.
        ///     Users can override the To and From addresses by passing in new addresses.
        /// </summary>
        /// <param name="shipment">Shipment to return.</param>
        /// <param name="overrideToAddress">Override the To address for the return.</param>
        /// <param name="overrideFromAddress">Override the From address for the return.</param>
        /// <returns>A return shipment.</returns>
        [CrudOperations.Create]
        public async Task<Shipment> CreateReturn(Shipment shipment, Address? overrideToAddress = null, Address? overrideFromAddress = null)
        {
            if (shipment.Id == null)
            {
                throw new Exception("Shipment.id is null.");
            }

            overrideToAddress ??= shipment.FromAddress; // Use override address if provided, otherwise use original shipment From address as return To address.
            overrideFromAddress ??= shipment.ToAddress; // Use override address if provided, otherwise use original shipment To address as return From address.

            if (Nullability.AnyAreNull(overrideToAddress, overrideFromAddress, shipment.Parcel))
            {
                throw new Exception("Shipment missing required elements.");
            }

            return await Create(
                new Dictionary<string, object>
                {
                    { "to_address", overrideToAddress! },
                    { "from_address", overrideFromAddress! },
                    { "parcel", shipment.Parcel! },
                    { "is_return", true },
                });
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
        [CrudOperations.Read]
        public async Task<ShipmentCollection> All(Dictionary<string, object>? parameters = null)
        {
            ShipmentCollection shipmentCollection = await List<ShipmentCollection>("shipments", parameters);
            shipmentCollection.Client = Client;
            return shipmentCollection;
        }

        /// <summary>
        ///     Retrieve a Shipment from its id.
        /// </summary>
        /// <param name="id">String representing a Shipment. Starts with "shp_".</param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        [CrudOperations.Read]
        public async Task<Shipment> Retrieve(string id)
        {
            return await Get<Shipment>($"shipments/{id}");
        }

        #endregion

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
