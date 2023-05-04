using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarrierDetail : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("alternate_identifier")]
        public string? AlternateIdentifier { get; internal set; }
        [JsonProperty("container_type")]
        public string? ContainerType { get; internal set; }
        [JsonProperty("destination_location")]
        public string? DestinationLocation { get; internal set; }
        [JsonProperty("est_delivery_date_local")]
        public string? EstDeliveryDateLocal { get; internal set; }
        [JsonProperty("est_delivery_time_local")]
        public string? EstDeliveryTimeLocal { get; internal set; }
        [JsonProperty("guaranteed_delivery_date")]
        public DateTime? GuaranteedDeliveryDate { get; internal set; }
        [JsonProperty("initial_delivery_attempt")]
        public DateTime? InitialDeliveryAttempt { get; internal set; }
        [JsonProperty("origin_location")]
        public string? OriginLocation { get; internal set; }
        [JsonProperty("service")]
        public string? Service { get; internal set; }

        #endregion

        internal CarrierDetail()
        {
        }
    }
}
