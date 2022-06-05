using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.Base
{
    public class Collection : EasyPostObject
    {
        [JsonProperty("filters")]
        public Dictionary<string, object>? filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
