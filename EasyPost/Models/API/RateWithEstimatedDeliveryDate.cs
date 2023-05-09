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
        ///     The <see cref="Models.API.SmartRate"/> object.
        /// </summary>
        [JsonProperty("rate")]
        public Rate? Rate { get; set; }

        /// <summary>
        ///     Estimated <see cref="TimeInTransitDetails"/> for the <see cref="Rate"/>.
        /// </summary>
        [JsonProperty("easypost_time_in_transit_data")]
        public TimeInTransitDetails? EasyPostTimeInTransitData { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="RateWithEstimatedDeliveryDate"/> class.
        /// </summary>
        internal RateWithEstimatedDeliveryDate()
        {
        }
    }

    /// <summary>
    ///     Class representing estimated transit times for a <see cref="RateWithEstimatedDeliveryDate"/>.
    /// </summary>
    public class TimeInTransitDetails
    {
        #region JSON Properties

        /// <summary>
        ///     Confidence levels for days in transit estimates.
        /// </summary>
        [JsonProperty("days_in_transit")]
        public TimeInTransit? DaysInTransit { get; set; }

        /// <summary>
        ///     EasyPost's estimated delivery date for the associated <see cref="RateWithEstimatedDeliveryDate"/>.
        /// </summary>
        [JsonProperty("easypost_time_in_transit_data")]
        public string? EasyPostEstimatedDeliveryDate { get; set; }

        /// <summary>
        ///     The planned departure date for the shipment.
        /// </summary>
        [JsonProperty("planned_ship_date")]
        public DateTime? PlannedShipDate { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="TimeInTransitDetails"/> class.
        /// </summary>
        internal TimeInTransitDetails()
        {
        }
    }
}
