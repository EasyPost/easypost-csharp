using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class CarrierType : EasyPostObject
    {
        [JsonProperty("fields")]
        public Dictionary<string, object>? Fields { get; set; }
        [JsonProperty("logo")]
        public string? Logo { get; set; }

        [JsonProperty("readable")]
        public string? Readable { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }
    }
}
