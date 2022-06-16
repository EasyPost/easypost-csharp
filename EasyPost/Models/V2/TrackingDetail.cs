using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TrackingDetail : EasyPostObject
    {
        [JsonProperty("datetime")]
        public DateTime? Datetime { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("tracking_location")]
        public TrackingLocation? TrackingLocation { get; set; }
    }
}
