﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EasyPost
{
    public class TrackerCollection
    {
        #region JSON Properties

        [JsonProperty("filters")]
        public Dictionary<string, object>? filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("trackers")]
        public List<Tracker> trackers { get; set; }

        #endregion

        /// <summary>
        ///     Get the next page of trackers based on the original parameters passed to Tracker.All().
        /// </summary>
        /// <returns>An EasyPost.TrackerCollection instance.</returns>
        public async Task<TrackerCollection> Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = trackers.Last().id;

            return await Tracker.All(filters);
        }
    }
}
