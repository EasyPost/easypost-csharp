using System.Collections.Generic;
using EasyPost.Interfaces;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class EventCollection : Collection
    {
        [JsonProperty("events")]
        public List<Event> events { get; set; }
    }
}
