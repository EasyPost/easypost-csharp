using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TrackerCollection : Collection
    {
        [JsonProperty("trackers")]
        public List<Tracker>? trackers { get; set; }

        public V2Client? V2Client { get; set; } // override the BaseClient property with a client property

        /// <summary>
        ///     Get the next page of trackers based on the original parameters passed to Tracker.All().
        /// </summary>
        /// <returns>An EasyPost.TrackerCollection instance.</returns>
        public async Task<TrackerCollection> Next()
        {
            filters ??= new Dictionary<string, object>();
            filters["before_id"] = (trackers ?? throw new PropertyMissing("trackers")).Last().id ?? throw new PropertyMissing("id");

            if (V2Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await V2Client.Trackers.All(filters);
        }
    }
}
