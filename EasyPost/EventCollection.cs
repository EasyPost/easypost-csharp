using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class EventCollection : Resource
    {
        #region JSON Properties

        [JsonProperty("events")]
        public List<Event> events { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }

        #endregion
    }
}
