using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TrackingLocation : EasyPostObject
    {
        [JsonProperty("city")]
        public string? city { get; set; }
        [JsonProperty("country")]
        public string? country { get; set; }

        [JsonProperty("state")]
        public string? state { get; set; }

        [JsonProperty("zip")]
        public string? zip { get; set; }
    }
}
