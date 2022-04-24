using System;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class TrackingDetail : Resource
    {
        [JsonProperty("datetime")]
        public DateTime? datetime { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("tracking_location")]
        public TrackingLocation tracking_location { get; set; }
    }
}
