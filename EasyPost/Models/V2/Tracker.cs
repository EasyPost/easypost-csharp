using System;
using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Tracker : Resource
    {
        [JsonProperty("carrier")]
        public string? carrier { get; set; }
        [JsonProperty("carrier_detail")]
        public CarrierDetail? carrier_detail { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("est_delivery_date")]
        public DateTime? est_delivery_date { get; set; }
        [JsonProperty("id")]
        public string? id { get; set; }
        [JsonProperty("mode")]
        public string? mode { get; set; }
        [JsonProperty("public_url")]
        public string? public_url { get; set; }
        [JsonProperty("shipment_id")]
        public string? shipment_id { get; set; }
        [JsonProperty("signed_by")]
        public string? signed_by { get; set; }
        [JsonProperty("status")]
        public string? status { get; set; }
        [JsonProperty("tracking_code")]
        public string? tracking_code { get; set; }
        [JsonProperty("tracking_details")]
        public List<TrackingDetail>? tracking_details { get; set; }
        [JsonProperty("tracking_updated_at")]
        public DateTime tracking_updated_at { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("weight")]
        public double? weight { get; set; }
    }
}
