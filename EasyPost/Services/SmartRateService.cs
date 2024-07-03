using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Parameters.SmartRate;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of SmartRate-related functionality.
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
        ///     Retrieve the estimated delivery date of each rate for an existing <see cref="Shipment"/> via the Delivery Date Estimator API, based on a specific ship date.
        /// </summary>
        /// <param name="shipmentId">The ID of the <see cref="Shipment"/> to get rate estimates for.</param>
        /// <param name="parameters">The <see cref="Parameters.SmartRate.EstimateDeliveryDateForShipment"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="EstimateDeliveryDateForShipmentResult"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<EstimateDeliveryDateForShipmentResult>> EstimateDeliveryDateForShipment(string shipmentId, EstimateDeliveryDateForShipment parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<EstimateDeliveryDateForShipmentResult>>(Method.Get, $"shipments/{shipmentId}/smartrate/delivery_date", cancellationToken, parameters.ToDictionary(), "rates");
        }

        /// <summary>
        ///     Retrieve the estimated delivery date of each carrier-service level combination via the Smart Deliver On API, based on a specific ship date and origin-destination postal code pair.
        ///     Unlike the <see cref="EstimateDeliveryDateForShipment"/> method, this method does not require a <see cref="Shipment"/> ID.
        /// </summary>
        /// <param name="parameters">The <see cref="Parameters.SmartRate.EstimateDeliveryDateForRoute"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="EstimateDeliveryDateForRouteResult"/> object.</returns>
        [CrudOperations.Read]
        public async Task<EstimateDeliveryDateForRouteResult> EstimateDeliveryDateForRoute(EstimateDeliveryDateForRoute parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<EstimateDeliveryDateForRouteResult>(Method.Post, "smartrate/deliver_by", cancellationToken, parameters.ToDictionary());
        }

        /// <summary>
        ///     Retrieve a recommended ship date for an existing <see cref="Shipment"/> via the Precision Shipping API, based on a specific desired delivery date.
        /// </summary>
        /// <param name="shipmentId">The ID of the <see cref="Shipment"/> to get rate estimates for.</param>
        /// <param name="parameters">The <see cref="Parameters.SmartRate.RecommendShipDateForShipment"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="RecommendShipDateForShipmentResult"/> objects.</returns>
        [CrudOperations.Read]
        public async Task<List<RecommendShipDateForShipmentResult>> RecommendShipDateForShipment(string shipmentId, RecommendShipDateForShipment parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<List<RecommendShipDateForShipmentResult>>(Method.Get, $"shipments/{shipmentId}/smartrate/precision_shipping", cancellationToken, parameters.ToDictionary(), "rates");
        }

        /// <summary>
        ///     Retrieve a recommended ship date for each carrier-service level combination via the Smart Deliver By API, based on a specific ship date and origin-destination postal code pair.
        ///     Unlike the <see cref="RecommendShipDateForShipment"/> method, this method does not require a <see cref="Shipment"/> ID.
        /// </summary>
        /// <param name="parameters">The <see cref="Parameters.SmartRate.RecommendShipDateForRoute"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="RecommendShipDateForRouteResult"/> object.</returns>
        [CrudOperations.Read]
        public async Task<RecommendShipDateForRouteResult> RecommendShipDateForRoute(RecommendShipDateForRoute parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<RecommendShipDateForRouteResult>(Method.Post, "smartrate/deliver_on", cancellationToken, parameters.ToDictionary());
        }

        #endregion
    }
}
