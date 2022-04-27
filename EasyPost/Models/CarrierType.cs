using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class CarrierType : Resource
    {
        [JsonProperty("fields")]
        public Dictionary<string, object> fields { get; set; }
        [JsonProperty("logo")]
        public string logo { get; set; }
        [JsonProperty("readable")]
        public string readable { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
    }
}
