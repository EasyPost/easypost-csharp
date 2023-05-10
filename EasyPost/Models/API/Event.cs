using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using EasyPost.Parameters.Event;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1716
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#event-object">EasyPost event</a>.
    /// </summary>
    public class Event : EasyPostObject
#pragma warning restore CA1716
    {
        #region JSON Properties

        /// <summary>
        ///     <see cref="Webhook"/> URLs that have already been successfully notified as of the time the current webhook was sent.
        /// </summary>
        [JsonProperty("completed_urls")]
        public List<string>? CompletedUrls { get; set; }

        /// <summary>
        ///     The result type and event name.
        ///     See https://www.easypost.com/docs/api#possible-event-types for a list of possible values.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     <see cref="Webhook"/> URls that have not yet been successfully notified as of the time the current webhook was sent.
        ///     The URL receiving this event will still be listed here.
        /// </summary>
        [JsonProperty("pending_urls")]
        public List<string>? PendingUrls { get; set; }

        /// <summary>
        ///     Any previous values of relevant result attributes.
        /// </summary>
        [JsonProperty("previous_attributes")]
        public Dictionary<string, object>? PreviousAttributes { get; set; }

        /// <summary>
        ///     The result of the event.
        ///     See the "object" key to determine its specific type.
        ///     This field will not be returned when retrieving events directly from the API.
        /// </summary>
        [JsonProperty("result")]
        public Dictionary<string, object>? Result { get; set; }

        /// <summary>
        ///     The current status of the event.
        ///     Possible values are "pending", "completed", "failed", "in_queue" and "retrying".
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     The ID of the <see cref="User"/> associated with the event and <see cref="Webhook"/>.
        /// </summary>
        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Event"/> class.
        /// </summary>
        internal Event()
        {
        }
    }

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Event"/>s.
    /// </summary>
    public class EventCollection : PaginatedCollection<Event>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Event"/>s in the collection.
        /// </summary>
        [JsonProperty("events")]
        public List<Event>? Events { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="EventCollection"/> class.
        /// </summary>
        internal EventCollection()
        {
        }

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Event> entries, int? pageSize = null)
        {
            All parameters = Filters != null ? (All)Filters : new All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
