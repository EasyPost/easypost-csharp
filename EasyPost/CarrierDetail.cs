using System;
using Newtonsoft.Json;

namespace EasyPost
{
    public class CarrierDetail
    {
        #region JSON Properties

        [JsonProperty("alternate_identifier")]
        public string alternate_identifier { get; set; }
        [JsonProperty("container_type")]
        public string container_type { get; set; }
        [JsonProperty("destination_location")]
        public string destination_location { get; set; }
        [JsonProperty("est_delivery_date_local")]
        public string est_delivery_date_local { get; set; }
        [JsonProperty("est_delivery_time_local")]
        public string est_delivery_time_local { get; set; }
        [JsonProperty("guaranteed_delivery_date")]
        public DateTime? guaranteed_delivery_date { get; set; }
        [JsonProperty("initial_delivery_attempt")]
        public DateTime? initial_delivery_attempt { get; set; }
        [JsonProperty("origin_location")]
        public string origin_location { get; set; }
        [JsonProperty("service")]
        public string service { get; set; }

        #endregion
    }
}
