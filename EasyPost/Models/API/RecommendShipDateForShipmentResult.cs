using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing a <see cref="Rate"/> with time-in-transit details based on a desired delivery date.
    /// </summary>
    public class RecommendShipDateForShipmentResult : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Models.API.SmartRate"/> object.
        /// </summary>
        [JsonProperty("rate")]
        public Rate? Rate { get; set; }

        /// <summary>
        ///     Estimated <see cref="TimeInTransitDetailsForDeliveryDate"/> for the <see cref="Rate"/>.
        /// </summary>
        [JsonProperty("easypost_time_in_transit_data")]
        public TimeInTransitDetailsForDeliveryDate? TimeInTransitDetails { get; set; }

        #endregion
    }
}
