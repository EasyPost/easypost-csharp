using System;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing a <see cref="Rate"/> with an <a href="https://docs.easypost.com/docs/shipments/shipping-smartrate#shipping-smartrate-1">estimated delivery date</a>.
    /// </summary>
    public class RateWithEstimatedDeliveryDate
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Models.API.Rate"/> object.
        /// </summary>
        [JsonProperty("rate")]
        public Rate? Rate { get; set; }

        /// <summary>
        ///     Estimated <see cref="TimeInTransitDetailsForDeliveryDateEstimate"/> for the <see cref="Rate"/>.
        /// </summary>
        [JsonProperty("easypost_time_in_transit_data")]
        public TimeInTransitDetailsForDeliveryDateEstimate? TimeInTransitDetails { get; set; }

        #endregion
    }
}
