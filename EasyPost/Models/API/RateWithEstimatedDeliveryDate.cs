using System;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing a <see cref="Rate"/> with an <a href="https://www.easypost.com/docs/api#retrieve-estimated-delivery-date-and-total-transit-days-across-all-rates-for-a-shipment">estimated delivery date</a>.
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
        ///     Estimated <see cref="TimeInTransitDetails"/> for the <see cref="Rate"/>.
        ///     Deprecated: Use <see cref="TimeInTransitDetails"/> instead.
        /// </summary>
        [Obsolete("This property will be removed in a future version and replaced with TimeInTransitDetails.")]
        public TimeInTransitDetails? EasyPostTimeInTransitData => this.TimeInTransitDetails?.AsDeprecatedTimeInTransitDetails();

        /// <summary>
        ///     Estimated <see cref="TimeInTransitDetailsForDeliveryDateEstimate"/> for the <see cref="Rate"/>.
        /// </summary>
        [JsonProperty("easypost_time_in_transit_data")]
        public TimeInTransitDetailsForDeliveryDateEstimate? TimeInTransitDetails { get; set; }

        #endregion
    }
}
