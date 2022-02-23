using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace EasyPost
{
    public class TrackerList
    {
        [JsonProperty("filters")]
        public Dictionary<string, object> filters { get; set; }
        [JsonProperty("has_more")]
        public bool has_more { get; set; }
        [JsonProperty("trackers")]
        public List<Tracker> trackers { get; set; }

        /// <summary>
        ///     Get the next page of shipments based on the original parameters passed to Shipment.List().
        /// </summary>
        /// <returns>A new EasyPost.ShipmentList instance.</returns>
        public TrackerList Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = trackers.Last().id;

            return Tracker.List(filters);
        }
    }
}
