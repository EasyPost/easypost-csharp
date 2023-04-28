using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1716
    public class Event : EasyPostObject
#pragma warning restore CA1716
    {
        #region JSON Properties

        [JsonProperty("completed_urls")]
        public List<string>? CompletedUrls { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("pending_urls")]
        public List<string>? PendingUrls { get; set; }
        [JsonProperty("previous_attributes")]
        public Dictionary<string, object>? PreviousAttributes { get; set; }
        [JsonProperty("result")]
        public Dictionary<string, object>? Result { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        #endregion

        internal Event()
        {
        }
    }

    public class EventCollection : PaginatedCollection<Event>
    {
        #region JSON Properties

        [JsonProperty("events")]
        public List<Event>? Events { get; set; }

        #endregion

        internal EventCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Event> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.Events.All parameters = Filters != null ? (BetaFeatures.Parameters.Events.All)Filters : new BetaFeatures.Parameters.Events.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
