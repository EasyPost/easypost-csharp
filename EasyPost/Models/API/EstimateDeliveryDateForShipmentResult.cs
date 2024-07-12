using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing a <see cref="Rate"/> with time-in-transit details based on a planned ship date.
    /// </summary>
    public class EstimateDeliveryDateForShipmentResult : EphemeralEasyPostObject
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
