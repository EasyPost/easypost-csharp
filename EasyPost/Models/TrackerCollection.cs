using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class TrackerCollection : Collection
    {
        [JsonProperty("trackers")]
        public List<Tracker>? trackers { get; set; }

        /// <summary>
        ///     Get the next page of trackers based on the original parameters passed to Tracker.All().
        /// </summary>
        /// <returns>An EasyPost.TrackerCollection instance.</returns>
        public async Task<TrackerCollection> Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = trackers.Last().id;

            if (Client == null)
            {
                throw new Exception("Client is null");
            }

            return await Client.Trackers.All(filters);
        }
    }
}
