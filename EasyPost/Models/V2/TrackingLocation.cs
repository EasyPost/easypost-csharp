using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TrackingLocation : EasyPostObject
    {
        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("country")]
        public string? Country { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("zip")]
        public string? Zip { get; set; }
    }
}
