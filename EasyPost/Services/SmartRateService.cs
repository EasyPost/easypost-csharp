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
        ///     Retrieve the estimated delivery date of each carrier-service level combination via the Smart Deliver By API, based on a specific ship date and origin-destination postal code pair.
        ///     Unlike the <see cref="ShipmentService.EstimateDeliveryDate"/> method, this method does not require a <see cref="Shipment"/> ID.
        /// </summary>
        /// <param name="parameters">The <see cref="Parameters.SmartRate.EstimateDeliveryDateForZipPair"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="EstimateDeliveryDateForZipPairResult"/> object.</returns>
        [CrudOperations.Read]
        public async Task<EstimateDeliveryDateForZipPairResult> EstimateDeliveryDate(EstimateDeliveryDateForZipPair parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<EstimateDeliveryDateForZipPairResult>(Method.Post, "smartrate/deliver_by", cancellationToken, parameters.ToDictionary());
        }



        /// <summary>
        ///     Retrieve a recommended ship date for each carrier-service level combination via the Smart Deliver On API, based on a specific desired delivery date and origin-destination postal code pair.
        ///     Unlike the <see cref="ShipmentService.RecommendShipDate"/> method, this method does not require a <see cref="Shipment"/> ID.
        /// </summary>
        /// <param name="parameters">The <see cref="Parameters.SmartRate.RecommendShipDateForZipPair"/> parameters to include on the API call.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="RecommendShipDateForZipPairResult"/> object.</returns>
        [CrudOperations.Read]
        public async Task<RecommendShipDateForZipPairResult> RecommendShipDate(RecommendShipDateForZipPair parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<RecommendShipDateForZipPairResult>(Method.Post, "smartrate/deliver_on", cancellationToken, parameters.ToDictionary());
        }

        #endregion
    }
}
