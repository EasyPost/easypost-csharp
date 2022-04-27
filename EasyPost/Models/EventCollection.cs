using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class EventCollection : Collection
    {
        [JsonProperty("events")]
        public List<Event> events { get; set; }
    }
}
