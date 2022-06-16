using System;
using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Tracker : EasyPostObject
    {
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }
        [JsonProperty("carrier_detail")]
        public CarrierDetail? CarrierDetail { get; set; }
        [JsonProperty("est_delivery_date")]
        public DateTime? EstDeliveryDate { get; set; }
        [JsonProperty("public_url")]
        public string? PublicUrl { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
        [JsonProperty("signed_by")]
        public string? SignedBy { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }
        [JsonProperty("tracking_details")]
        public List<TrackingDetail>? TrackingDetails { get; set; }
        [JsonProperty("tracking_updated_at")]
        public DateTime TrackingUpdatedAt { get; set; }
        [JsonProperty("weight")]
        public double? Weight { get; set; }
    }
}
