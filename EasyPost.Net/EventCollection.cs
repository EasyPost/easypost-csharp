using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class EventCollection : Resource
    {
        [JsonProperty("events")]
        public List<Event> events { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
