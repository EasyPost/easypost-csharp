using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TrackerCollection : Collection, IPaginatedCollection
    {
        [JsonProperty("trackers")]
        public List<Tracker>? Trackers { get; set; }

        /// <summary>
        ///     Get the next page of trackers based on the original parameters passed to Tracker.All().
        /// </summary>
        /// <returns>An EasyPost.TrackerCollection instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<IPaginatedCollection> Next()
        {
            Filters ??= new Dictionary<string, object>();
            Filters["before_id"] = (Trackers ?? throw new PropertyMissing("trackers")).Last().Id ?? throw new PropertyMissing("id");

            if (Client == null)
            {
                throw new ClientNotConfigured();
            }

            return await Client.TrackersEasyPost.All(Filters);
        }
    }
}
