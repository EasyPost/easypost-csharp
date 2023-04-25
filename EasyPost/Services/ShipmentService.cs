using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;
using EasyPost.Utilities.Internal.Extensions;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ShipmentService : EasyPostService
    {
        internal ShipmentService(EasyPostClient client)
            : base(client)
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
        ///     Create a <see cref="ScanForm"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.ScanForms.Create"/> parameter set.</param>
        /// <returns><see cref="ScanForm"/> instance.</returns>
        [CrudOperations.Create]
        public async Task<Shipment> Create(BetaFeatures.Parameters.Shipments.Create parameters)
        {
            // Because the normal Create method does wrapping internally, we can't simply pass the parameters object to it, otherwise it will wrap the parameters twice.
            return await Request<Shipment>(Method.Post, "shipments", parameters.ToDictionary());
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
            ShipmentCollection shipmentCollection = await Request<ShipmentCollection>(Method.Get,"shipments", parameters);
            shipmentCollection.Purchased = parameters?.GetOrNullBoolean("purchased");
            shipmentCollection.IncludeChildren = parameters?.GetOrNullBoolean("include_children");
            return shipmentCollection;
        }

        /// <summary>
        ///     List all <see cref="Shipment"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.All"/> parameter set.</param>
        /// <returns><see cref="ShipmentCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<ShipmentCollection> All(BetaFeatures.Parameters.Shipments.All parameters)
        {
            ShipmentCollection shipmentCollection = await Request<ShipmentCollection>(Method.Get,"shipments", parameters.ToDictionary());
            shipmentCollection.Purchased = parameters.Purchased;
            shipmentCollection.IncludeChildren = parameters.IncludeChildren;
            return shipmentCollection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="ShipmentCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="ShipmentCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="ShipmentCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<ShipmentCollection> GetNextPage(ShipmentCollection collection, int? pageSize = null) => await collection.GetNextPage<ShipmentCollection, BetaFeatures.Parameters.Shipments.All>(async parameters => await All(parameters), collection.Shipments, pageSize);

        /// <summary>
        ///     Retrieve a Shipment from its id.
        /// </summary>
        /// <param name="id">String representing a Shipment. Starts with "shp_".</param>
        /// <returns>An EasyPost.Shipment instance.</returns>
        [CrudOperations.Read]
        public async Task<Shipment> Retrieve(string id) => await Request<Shipment>(Method.Get, $"shipments/{id}");
        
        /// <summary>
        ///     Get the SmartRates for this shipment.
        /// </summary>
        /// <returns>A list of EasyPost.Smartrate instances.</returns>
        [CrudOperations.Read]
        public async Task<List<Smartrate>> GetSmartrates(string id)
        {
            return await Request<List<Smartrate>>(Http.Method.Get, $"shipments/{id}/smartrate", null, "result");
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rateId">The id of the rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The id of the end shipper to use for this purchase.</param>
        /// <returns>The task to buy this Shipment.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Buy(string id, string? rateId, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null)
        {
            // TODO: Should this function return the updated Shipment like Order.Buy?
            if (rateId == null)
            {
                throw new MissingParameterError("rateId");
            }

            Dictionary<string, object> parameters = new()
            {
                { "rate", new Dictionary<string, object> { { "id", rateId } } },
                { "insurance", insuranceValue ?? string.Empty },
                { "carbon_offset", withCarbonOffset },
            };

            if (endShipperId != null)
            {
                parameters.Add("end_shipper", endShipperId);
            }

            return await Request<Shipment>(Http.Method.Post, $"shipments/{id}/buy", parameters);
        }

        /// <summary>
        ///     Purchase a label for this shipment with the given rate.
        /// </summary>
        /// <param name="rate">The Rate to purchase the shipment with.</param>
        /// <param name="insuranceValue">The value to insure the shipment for.</param>
        /// <param name="withCarbonOffset">Whether to apply carbon offset to this purchase.</param>
        /// <param name="endShipperId">The id of the end shipper to use for this purchase.</param>
        /// <returns>The task to buy this Shipment.</returns>
        [CrudOperations.Update]
        public async Task Buy(string id, Rate rate, string? insuranceValue = null, bool withCarbonOffset = false, string? endShipperId = null)
        {
            if (rate == null)
            {
                throw new MissingParameterError("rate");
            }

            await Buy(id, rate.Id, insuranceValue, withCarbonOffset, endShipperId);
        }

        /// <summary>
        ///     Purchase a label for this <see cref="Shipment"/> with the given <see cref="Rate"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.Buy"/> parameters set.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Buy(string id, BetaFeatures.Parameters.Shipments.Buy parameters)
        {
            return await Request<Shipment>(Http.Method.Post, $"shipments/{id}/buy", parameters.ToDictionary());
        }

        /// <summary>
        ///     Generate a postage label for this shipment.
        /// </summary>
        /// <param name="fileFormat">Format to generate the label in. Valid formats: "pdf", "zpl" and "epl2".</param>
        /// <returns>The updated Shipment.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> GenerateLabel(string id, string fileFormat)
        {
            Dictionary<string, object> parameters = new() { { "file_format", fileFormat } };

            return await Request<Shipment>(Http.Method.Get, $"shipments/{id}/label", parameters);
        }

        /// <summary>
        ///     Generate a postage label for this <see cref="Shipment"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.GenerateLabel"/> parameter set.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> GenerateLabel(string id, BetaFeatures.Parameters.Shipments.GenerateLabel parameters)
        {
            return await Request<Shipment>(Http.Method.Get, $"shipments/{id}/label", parameters.ToDictionary());
        }

        /// <summary>
        ///     Insure shipment for the given amount.
        /// </summary>
        /// <param name="amount">The amount to insure the shipment for. Currency is provided when creating a shipment.</param>
        /// <returns>The updated Shipment.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Insure(string id, double amount)
        {
            Dictionary<string, object> parameters = new() { { "amount", amount } };

            return await Request<Shipment>(Http.Method.Post, $"shipments/{id}/insure", parameters);
        }

        /// <summary>
        ///     Insure this <see cref="Shipment"/> for the given amount.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.Insure"/> parameters set.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Insure(string id, BetaFeatures.Parameters.Shipments.Insure parameters)
        {
            return await Request<Shipment>(Http.Method.Post, $"shipments/{id}/insure", parameters.ToDictionary());
        }

        /// <summary>
        ///     Send a refund request to the carrier the shipment was purchased from.
        /// </summary>
        /// <returns>The updated Shipment.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> Refund(string id)
        {
            return await Request<Shipment>(Http.Method.Post, $"shipments/{id}/refund");
        }

        /// <summary>
        ///     Refresh the rates for this Shipment.
        /// </summary>
        /// <param name="parameters">Optional dictionary of parameters for the API request.</param>
        /// <param name="withCarbonOffset">Whether to use carbon offset when re-rating the shipment.</param>
        /// <returns>The task to regenerate this Shipment's rates.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> RegenerateRates(string id, Dictionary<string, object>? parameters = null, bool withCarbonOffset = false)
        {
            parameters ??= new Dictionary<string, object>();

            parameters.Add("carbon_offset", withCarbonOffset);

            return await Request<Shipment>(Http.Method.Post, $"shipments/{id}/rerate", parameters);
        }

        /// <summary>
        ///     Refresh <see cref="Rates"/> for this <see cref="Shipment"/>.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Shipments.RegenerateRates"/> parameter set.</param>
        /// <returns>This updated <see cref="Shipment"/> instance.</returns>
        [CrudOperations.Update]
        public async Task<Shipment> RegenerateRates(string id, BetaFeatures.Parameters.Shipments.RegenerateRates parameters)
        {
            return await Request<Shipment>(Http.Method.Post, $"shipments/{id}/rerate", parameters.ToDictionary());
        }

        #endregion

        /// <summary>
        ///     Get the lowest Smart Rate from a list of Smart Rates.
        ///
        ///     Deprecated. Use <see cref="EasyPost.Utilities.Rates.GetLowestSmartRate(IEnumerable{EasyPost.Models.API.Smartrate}, int, EasyPost.Models.API.SmartrateAccuracy)"/> instead.
        /// </summary>
        /// <param name="smartrates">List of smartrates to filter.</param>
        /// <param name="deliveryDays">Delivery days restriction to use when filtering.</param>
        /// <param name="deliveryAccuracy">Delivery days accuracy restriction to use when filtering.</param>
        /// <returns>Lowest EasyPost.Smartrate object instance.</returns>
        [Obsolete("This method is deprecated. Please use EasyPost.Utilities.Rates.GetLowestSmartRate() instead. This method will be removed in a future version.", false)]
        public static Smartrate GetLowestSmartrate(IEnumerable<Smartrate> smartrates, int deliveryDays, SmartrateAccuracy deliveryAccuracy) => Utilities.Rates.GetLowestSmartRate(smartrates, deliveryDays, deliveryAccuracy);
    }
}
