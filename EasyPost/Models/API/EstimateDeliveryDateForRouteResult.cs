using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing delivery date estimates for carrier-route-service level combinations.
    /// </summary>
    public class EstimateDeliveryDateForRouteResult : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     A list of carrier names that do not have estimated delivery dates.
        /// </summary>
        [JsonProperty("carriers_without_tint_estimates")]
        public List<string>? CarriersWithoutEstimates { get; set; }

        /// <summary>
        ///     The origin postal code used for the estimates.
        /// </summary>
        [JsonProperty("from_zip")]
        public string? OriginPostalCode { get; set; }

        /// <summary>
        ///     The destination postal code used for the estimates.
        /// </summary>
        [JsonProperty("to_zip")]
        public string? DestinationPostalCode { get; set; }

        /// <summary>
        ///     Whether potential Saturday delivery dates are included in the estimates.
        /// </summary>
        [JsonProperty("saturday_delivery")]
        public bool? SaturdayDelivery { get; set; }

        /// <summary>
        ///     The ship date used for the estimates.
        /// </summary>
        [JsonProperty("planned_ship_date")]
        public string? PlannedShipDate { get; set; }

        /// <summary>
        ///     The estimate results.
        /// </summary>
        [JsonProperty("results")]
        public List<DeliveryDateForRouteEstimate>? Estimates { get; set; }

        #endregion
    }
}
