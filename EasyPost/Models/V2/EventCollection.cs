using System.Collections.Generic;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class EventCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("events")]
        public List<Event>? Events { get; set; }

        #endregion
    }
}
