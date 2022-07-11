using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TrackerCollection : Collection, IPaginatedCollection
    {
        #region JSON Properties

        [JsonProperty("trackers")]
        public List<Tracker>? Trackers { get; set; }

        #endregion

        /// <summary>
        ///     Get the next page of trackers based on the original parameters passed to Tracker.All().
        /// </summary>
        /// <returns>An EasyPost.TrackerCollection instance.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<IPaginatedCollection> Next()
        {
            UpdateFilters(Trackers, "trackers");
            return await Client!.Trackers.All(Filters);
        }
    }
}
