using System.Collections.Generic;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TrackerCollection : Collection
    {
        #region JSON Properties

        [JsonProperty("trackers")]
        public List<Tracker> trackers { get; set; }

        #endregion
    }
}
