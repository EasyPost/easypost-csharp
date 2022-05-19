using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Interfaces;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TrackerCollection : Collection
    {
        [JsonProperty("trackers")]
        public List<Tracker>? trackers { get; set; }

        public new V2Client V2Client { get; set; } // override the BaseClient property with a client property

        /// <summary>
        ///     Get the next page of trackers based on the original parameters passed to Tracker.All().
        /// </summary>
        /// <returns>An EasyPost.TrackerCollection instance.</returns>
        public async Task<TrackerCollection> Next()
        {
            filters = filters ?? new Dictionary<string, object>();
            filters["before_id"] = trackers.Last().id;

            if (V2Client == null)
            {
                throw new Exception("Client is null");
            }

            return await V2Client.Trackers.All(filters);
        }
    }
}
