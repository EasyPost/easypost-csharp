using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing ship date recommendations for carrier-route-service level combinations.
    /// </summary>
    public class RecommendShipDateForZipPairResult : EphemeralEasyPostObject
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
        public string? FromZip { get; set; }

        /// <summary>
        ///     The destination postal code used for the estimates.
        /// </summary>
        [JsonProperty("to_zip")]
        public string? ToZip { get; set; }

        /// <summary>
        ///     Whether potential Saturday delivery dates are included in the estimates.
        /// </summary>
        [JsonProperty("saturday_delivery")]
        public bool? SaturdayDelivery { get; set; }

        /// <summary>
        ///     The delivery date used for the estimates.
        /// </summary>
        [JsonProperty("desired_delivery_date")]
        public string? DesiredDeliveryDate { get; set; }

        /// <summary>
        ///     The estimate results.
        /// </summary>
        [JsonProperty("results")]
        public List<ShipDateForZipPairRecommendation>? Results { get; set; }

        #endregion
    }
}
