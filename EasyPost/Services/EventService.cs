using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EventService : EasyPostService
    {
        internal EventService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     List all Event objects.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to filter the list with. Valid pairs:
        ///     * {"before_id", string} String representing an Event ID. Starts with "evt_". Only retrieve events created
        ///     before this id. Takes precedence over after_id.
        ///     * {"after_id", string} String representing an Event ID. Starts with "evt_". Only retrieve events created after
        ///     this id.
        ///     * {"start_datetime", string} ISO 8601 datetime string. Only retrieve events created after this datetime.
        ///     * {"end_datetime", string} ISO 8601 datetime string. Only retrieve events created before this datetime.
        ///     * {"page_size", int} Max size of list. Default to 20.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>An EasyPost.EventCollection instance.</returns>
        [CrudOperations.Read]
        public async Task<EventCollection> All(Dictionary<string, object>? parameters = null) => await Get<EventCollection>("events", parameters);

        /// <summary>
        ///     List all <see cref="Event"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Events.All"/> parameter set.</param>
        /// <returns><see cref="EventCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<EventCollection> All(BetaFeatures.Parameters.Events.All parameters) => await Get<EventCollection>("events", parameters.ToDictionary());

        /// <summary>
        ///     Get the next page of a paginated <see cref="EventCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="EventCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="EventCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<EventCollection> GetNextPage(EventCollection collection, int? pageSize = null) => await collection.GetNextPage<EventCollection, BetaFeatures.Parameters.Events.All>(async parameters => await All(parameters), collection.Events, pageSize);

        /// <summary>
        ///     Retrieve an Event from its id.
        /// </summary>
        /// <param name="id">String representing a Event. Starts with "evt_".</param>
        /// <returns>EasyPost.Event instance.</returns>
        [CrudOperations.Read]
        public async Task<Event> Retrieve(string id) => await Get<Event>($"events/{id}");

        /// <summary>
        ///     Retrieve all <see cref="Payload"/>s for an <see cref="Event"/>.
        /// </summary>
        /// <param name="eventId">ID of the <see cref="Event"/> to retrieve payloads for.</param>
        /// <returns>A list of <see cref="Payload"/> objects.</returns>
        public async Task<List<Payload>> RetrieveAllPayloads(string eventId) => await Get<List<Payload>>($"events/{eventId}/payloads", rootElement: "payloads");

        /// <summary>
        ///     Retrieve a specific <see cref="Payload"/> for an <see cref="Event"/>.
        /// </summary>
        /// <param name="eventId">ID of the <see cref="Event"/> to retrieve payload for.</param>
        /// <param name="payloadId">ID of payload to retrieve.</param>
        /// <returns>A <see cref="Payload"/> object.</returns>
        /// <exception cref="InvalidRequestError">Thrown if the specified payload ID is malformed.</exception>
        /// <exception cref="NotFoundError">Thrown if the specified payload is not found.</exception>
        public async Task<Payload> RetrievePayload(string eventId, string payloadId) => await Get<Payload>($"events/{eventId}/payloads/{payloadId}");

        #endregion
    }
}
