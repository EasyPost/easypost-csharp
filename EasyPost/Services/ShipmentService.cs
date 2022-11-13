using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Calculation;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;
using RestSharp;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
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
            return await Request<Shipment>(Method.Post, "shipments", parameters);
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
            return await Request<ShipmentCollection>(Method.Get, "shipments", parameters);
        }

        /// <summary>
        ///     Retrieve a Shipment from its id.
        /// </summary>
        /// <param name="id">String representing a Shipment. Starts with "shp_".</param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        [CrudOperations.Read]
        public async Task<Shipment> Retrieve(string id)
        {
            return await Request<Shipment>(Method.Get, $"shipments/{id}");
        }

        /// <summary>
        ///     Get the Smartrates for this shipment.
        /// </summary>
        /// <returns>A list of EasyPost.Smartrate instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Smartrate>> GetSmartrates(string id)
        {
            return await Request<List<Smartrate>>(Method.Get, $"shipments/{id}/smartrate", null, "result");
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rateId">The id of the rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The id of the end shipper to use for this purchase.</param>
        [CrudOperations.Update]
        public async Task<Shipment> Buy(string id, string rateId, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null)
        {
            if (rateId == null)
            {
                throw new MissingParameterError("rateId");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>
            {
                { "rate", new Dictionary<string, object> { { "id", rateId } } },
                { "insurance", insuranceValue ?? string.Empty },
                { "carbon_offset", withCarbonOffset }
            };

            if (endShipperId != null)
            {
                parameters.Add("end_shipper", endShipperId);
            }

            return await Request<Shipment>(Method.Post, $"shipments/{id}/buy", parameters);
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">The Rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The id of the end shipper to use for this purchase.</param>
        [CrudOperations.Update]
        public async Task<Shipment> Buy(string id, Rate rate, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null)
        {
            if (rate == null)
            {
                throw new MissingParameterError("rate");
            }
            if (rate.Id == null)
            {
                throw new MissingPropertyError(rate, "Id");
            }
            return await Buy(id, rate.Id, insuranceValue, withCarbonOffset, endShipperId);
        }

        /// <summary>
        ///     Generate a postage label for this shipment.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        [CrudOperations.Update]
        public async Task<Shipment> GenerateLabel(string id, string fileFormat)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "file_format", fileFormat } };

            return await Request<Shipment>(Method.Get, $"shipments/{id}/label", parameters);
        }

        /// <summary>
        ///     Insure shipment for the given amount.
        /// </summary>
        /// <param name="amount">The amount to insure the shipment for. Currency is provided when creating a shipment.</param>
        [CrudOperations.Update]
        public async Task<Shipment> Insure(string id, double amount)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object> { { "amount", amount } };

            return await Request<Shipment>(Method.Post, $"shipments/{id}/insure", parameters);
        }

        /// <summary>
        ///     Send a refund request to the carrier the shipment was purchased from.
        /// </summary>
        [CrudOperations.Update]
        public async Task<Shipment> Refund(string id)
        {
            return await Request<Shipment>(Method.Post, $"shipments/{id}/refund");
        }

        /// <summary>
        ///     Refresh the rates for this Shipment.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters for the API request.</param>
        /// <param name="withCarbonOffset">Whether to use carbon offset when re-rating the shipment.</param>
        [CrudOperations.Update]
        public async Task<Shipment> RegenerateRates(string id, Dictionary<string, object>? parameters = null, bool withCarbonOffset = false)
        {
            parameters ??= new Dictionary<string, object>();

            parameters.Add("carbon_offset", withCarbonOffset);

            return await Request<Shipment>(Method.Post, $"shipments/{id}/rerate", parameters);
        }

        #endregion

        /// <summary>
        ///     Get the lowest smartrate from a list of smartrates.
        /// </summary>
        /// <param name="smartrates">List of smartrates to filter.</param>
        /// <param name="deliveryDays">Delivery days restriction to use when filtering.</param>
        /// <param name="deliveryAccuracy">Delivery days accuracy restriction to use when filtering.</param>
        /// <returns>Lowest EasyPost.Smartrate object instance.</returns>
        public Smartrate GetLowestSmartrate(IEnumerable<Smartrate> smartrates, int deliveryDays, SmartrateAccuracy deliveryAccuracy)
        {
            return Rates.GetLowestShipmentSmartrate(smartrates, deliveryDays, deliveryAccuracy);
        }

        /// <summary>
        ///     Get the lowest rate for this Shipment.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.Rate object instance.</returns>
        public Rate GetLowestRate(IEnumerable<Rate> rates, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            return Calculation.Rates.GetLowestObjectRate(rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }

        /// <summary>
        ///     Get the lowest rate for this Shipment.
        /// </summary>
        /// <param name="includeCarriers">Carriers to include in the filter.</param>
        /// <param name="includeServices">Services to include in the filter.</param>
        /// <param name="excludeCarriers">Carriers to exclude in the filter.</param>
        /// <param name="excludeServices">Services to exclude in the filter.</param>
        /// <returns>Lowest EasyPost.Rate object instance.</returns>
        public Rate GetLowestRate(Shipment shipment, List<string>? includeCarriers = null, List<string>? includeServices = null, List<string>? excludeCarriers = null, List<string>? excludeServices = null)
        {
            if (shipment.Rates == null)
            {
                throw new MissingPropertyError(shipment, "Rates");
            }
            return Calculation.Rates.GetLowestObjectRate(shipment.Rates, includeCarriers, includeServices, excludeCarriers, excludeServices);
        }
    }
}
