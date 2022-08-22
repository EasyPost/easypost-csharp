using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TrackerCollection : Collection, IPaginatedCollection
    {
        #region JSON Properties

        [JsonProperty("filters")]
        public Dictionary<string, object>? filters { get; set; }
        [JsonProperty("trackers")]
        public List<Tracker> trackers { get; set; }

        #endregion

        #region CRUD Operations

        /// <summary>
        ///     Get the next page of trackers based on the original parameters passed to Tracker.All().
        /// </summary>
        /// <returns>An EasyPost.TrackerCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<IPaginatedCollection> Next()
        {
            UpdateFilters(trackers, "trackers");
            return await (Client as Client)!.Tracker.All(filters);
        }

        #endregion
    }
}
