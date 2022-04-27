using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost.Interfaces
{
    public class Collection : Resource
    {
        [JsonProperty("filters")]
        public Dictionary<string, object>? filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
