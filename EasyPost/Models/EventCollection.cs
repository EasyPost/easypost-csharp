using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class EventCollection : Resource
    {
        [JsonProperty("events")]
        public List<Event> events { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
    }
}
