using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class EventCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("events")]
        public List<Event>? Events { get; set; }

        #endregion

        internal EventCollection()
        {
        }
    }
}
