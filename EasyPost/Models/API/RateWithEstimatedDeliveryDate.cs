using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class RateWithEstimatedDeliveryDate
    {
        #region JSON Properties

        [JsonProperty("rate")]
        public Rate? Rate { get; set; }
        [JsonProperty("easypost_time_in_transit_data")]
        public TimeInTransitDetails? EasyPostTimeInTransitData { get; set; }

        #endregion

        internal RateWithEstimatedDeliveryDate()
        {
        }
    }

    public class TimeInTransitDetails
    {
        #region JSON Properties

        [JsonProperty("days_in_transit")]
        public TimeInTransit? DaysInTransit { get; set; }
        [JsonProperty("easypost_time_in_transit_data")]
        public string? EasyPostEstimatedDeliveryDate { get; set; }
        [JsonProperty("planned_ship_date")]
        public DateTime? PlannedShipDate { get; set; }

        #endregion
        
        internal TimeInTransitDetails()
        {
        }
    }
}
