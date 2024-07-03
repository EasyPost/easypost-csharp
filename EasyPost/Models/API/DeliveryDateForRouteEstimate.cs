using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing time-in-transit estimates for a specific carrier-route-service level combination in a <see cref="EstimateDeliveryDateForRouteResult"/>.
    /// </summary>
    public class DeliveryDateForRouteEstimate : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The carrier associated with the estimate.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The service level associated with the estimate.
        /// </summary>
        [JsonProperty("service")]
        public string? Service { get; set; }

        /// <summary>
        ///     Estimated <see cref="TimeInTransitDetailsForShipDate"/> for the carrier-service level combination.
        /// </summary>
        [JsonProperty("easypost_time_in_transit_data")]
        public TimeInTransitDetailsForShipDate? EasyPostTimeInTransitData { get; set; }

        #endregion
    }
}
