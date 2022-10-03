using System;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarrierDetail
    {
        #region JSON Properties

        [JsonProperty("alternate_identifier")]
        public string? AlternateIdentifier { get; set; }
        [JsonProperty("container_type")]
        public string? ContainerType { get; set; }
        [JsonProperty("destination_location")]
        public string? DestinationLocation { get; set; }
        [JsonProperty("est_delivery_date_local")]
        public string? EstDeliveryDateLocal { get; set; }
        [JsonProperty("est_delivery_time_local")]
        public string? EstDeliveryTimeLocal { get; set; }
        [JsonProperty("guaranteed_delivery_date")]
        public DateTime? GuaranteedDeliveryDate { get; set; }
        [JsonProperty("initial_delivery_attempt")]
        public DateTime? InitialDeliveryAttempt { get; set; }
        [JsonProperty("origin_location")]
        public string? OriginLocation { get; set; }
        [JsonProperty("service")]
        public string? Service { get; set; }

        #endregion

        internal CarrierDetail()
        {
        }
    }
}
