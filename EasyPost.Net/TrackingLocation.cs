using System;
using Newtonsoft.Json;

namespace EasyPost
{
    public class TrackingLocation
    {
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("zip")]
        public string zip { get; set; }
    }
}
