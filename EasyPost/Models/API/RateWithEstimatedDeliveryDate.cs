using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class RateWithEstimatedDeliveryDate
    {
        #region JSON Properties

        [JsonProperty("rate")]
        public Rate? Rate { get; internal set; }
        [JsonProperty("easypost_time_in_transit_data")]
        public TimeInTransitDetails? EasyPostTimeInTransitData { get; internal set; }

        #endregion

        internal RateWithEstimatedDeliveryDate()
        {
        }
    }

    public class TimeInTransitDetails
    {
        #region JSON Properties

        [JsonProperty("days_in_transit")]
        public TimeInTransit? DaysInTransit { get; internal set; }
        [JsonProperty("easypost_time_in_transit_data")]
        public string? EasyPostEstimatedDeliveryDate { get; internal set; }
        [JsonProperty("planned_ship_date")]
        public DateTime? PlannedShipDate { get; internal set; }

        #endregion

        internal TimeInTransitDetails()
        {
        }
    }
}
