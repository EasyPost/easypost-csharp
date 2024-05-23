using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Parameters.SmartRate;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services.Beta
{
    /// <summary>
    ///     Class representing a set of SmartRate-related beta functionality.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SmartRateService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SmartRateService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal SmartRateService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Get the <see cref="SmartRate"/>s for a <see cref="Shipment"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-time-in-transit-statistics-across-all-rates-for-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="shipmentId">The ID of the <see cref="Shipment"/> to get rates for.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="SmartRate"/>s.</returns>
        [CrudOperations.Read]
        public async Task<List<SmartRate>> GetSmartRates(string shipmentId, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<SmartRate>>(Method.Get, $"shipments/{shipmentId}/smartrate", cancellationToken, rootElement: "result");
        }

        /// <summary>
        ///     Retrieve the estimated delivery date of each rate for a <see cref="Shipment"/> via the SmartRates API, based on a specific ship date.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-estimated-delivery-date-and-total-transit-days-across-all-rates-for-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="shipmentId">The ID of the <see cref="Shipment"/> to get rate estimates for.</param>
        /// <param name="shipDate">The specific ship date to use for the rate estimates.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="RateWithTimeInTransitDetailsByShipDate"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<RateWithTimeInTransitDetailsByShipDate>> EstimateDeliveryDateByShipDate(string shipmentId, string shipDate, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                { "planned_ship_date", shipDate },
            };
            return await RequestAsync<List<RateWithTimeInTransitDetailsByShipDate>>(Method.Get, $"shipments/{shipmentId}/smartrate/delivery_date", cancellationToken, parameters, "rates");
        }

        /// <summary>
        ///     Retrieve the estimated delivery date of each rate for a <see cref="Shipment"/> via the SmartRates API, based on a specific ship date.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-estimated-delivery-date-and-total-transit-days-across-all-rates-for-a-shipment">Related API documentation</a>.
        /// </summary>
        /// <param name="shipmentId">The ID of the <see cref="Shipment"/> to get rate estimates for.</param>
        /// <param name="parameters">The <see cref="Parameters.SmartRate.EstimateDeliveryDateByShipDate"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="RateWithTimeInTransitDetailsByShipDate"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<RateWithTimeInTransitDetailsByShipDate>> EstimateDeliveryDateByShipDate(string shipmentId, EstimateDeliveryDateByShipDate parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<RateWithTimeInTransitDetailsByShipDate>>(Method.Get, $"shipments/{shipmentId}/smartrate/delivery_date", cancellationToken, parameters.ToDictionary(), "rates");
        }

        /// <summary>
        ///     Retrieve a recommended ship date for a <see cref="Shipment"/> via the SmartRates API, based on a specific desired delivery date.
        /// </summary>
        /// <param name="shipmentId">The ID of the <see cref="Shipment"/> to get rate estimates for.</param>
        /// <param name="desiredDeliveryDate">The specific desired delivery date to use for the rate estimates.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="RateWithTimeInTransitDetailsByDeliveryDate"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<RateWithTimeInTransitDetailsByDeliveryDate>> RecommendShipDateByDeliveryDate(string shipmentId, string desiredDeliveryDate, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                { "desired_delivery_date", desiredDeliveryDate },
            };
            return await RequestAsync<List<RateWithTimeInTransitDetailsByDeliveryDate>>(Method.Get, $"shipments/{shipmentId}/smartrate/precision_shipping", cancellationToken, parameters, "rates", ApiVersion.Beta);
        }

        /// <summary>
        ///     Retrieve a recommended ship date for a <see cref="Shipment"/> via the SmartRates API, based on a specific desired delivery date.
        /// </summary>
        /// <param name="shipmentId">The ID of the <see cref="Shipment"/> to get rate estimates for.</param>
        /// <param name="parameters">The <see cref="Parameters.SmartRate.RecommendShipDateByDeliveryDate"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="RateWithTimeInTransitDetailsByDeliveryDate"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<RateWithTimeInTransitDetailsByDeliveryDate>> RecommendShipDateByDeliveryDate(string shipmentId, RecommendShipDateByDeliveryDate parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<RateWithTimeInTransitDetailsByDeliveryDate>>(Method.Get, $"shipments/{shipmentId}/smartrate/precision_shipping", cancellationToken, parameters.ToDictionary(), "rates", ApiVersion.Beta);
        }

        #endregion
    }
}
