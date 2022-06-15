using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Interfaces;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TrackerCollection : Collection, IPaginatedCollection
    {
        [JsonProperty("trackers")]
        public List<Tracker>? trackers { get; set; }

        /// <summary>
        ///     Get the next page of trackers based on the original parameters passed to Tracker.All().
        /// </summary>
        /// <returns>An EasyPost.TrackerCollection instance.</returns>
        public async Task<IPaginatedCollection> Next()
        {
            filters ??= new Dictionary<string, object>();
            filters["before_id"] = (trackers ?? throw new PropertyMissing("trackers")).Last().id ?? throw new PropertyMissing("id");

            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await Client.Trackers.All(filters);
        }
    }
}
