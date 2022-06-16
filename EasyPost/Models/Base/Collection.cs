using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.Base
{
    public class Collection : EasyPostObject
    {
        [JsonProperty("filters")]
        public Dictionary<string, object>? Filters { get; set; }
        [JsonProperty("has_more")]
        public bool HasMore { get; set; }
    }
}
