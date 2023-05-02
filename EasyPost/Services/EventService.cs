using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
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
        public async Task<EventCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            EventCollection collection = await RequestAsync<EventCollection>(Method.Get, "events", cancellationToken, parameters);
            collection.Filters = BaseAllParameters.FromDictionary<BetaFeatures.Parameters.Events.All>(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Event"/> objects.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.Events.All"/> parameter set.</param>
        /// <returns><see cref="EventCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<EventCollection> All(BetaFeatures.Parameters.Events.All parameters, CancellationToken cancellationToken = default)
        {
            EventCollection collection = await RequestAsync<EventCollection>(Method.Get, "events", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="EventCollection"/>.
        /// </summary>
        /// <param name="collection">The <see cref="EventCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <returns>The next page, as a <see cref="EventCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<EventCollection> GetNextPage(EventCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<EventCollection, BetaFeatures.Parameters.Events.All>(async parameters => await All(parameters, cancellationToken), collection.Events, pageSize);

        /// <summary>
        ///     Retrieve an Event from its id.
        /// </summary>
        /// <param name="id">String representing a Event. Starts with "evt_".</param>
        /// <returns>EasyPost.Event instance.</returns>
        [CrudOperations.Read]
        public async Task<Event> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Event>(Method.Get, $"events/{id}", cancellationToken);

        /// <summary>
        ///     Retrieve all <see cref="Payload"/>s for an <see cref="Event"/>.
        /// </summary>
        /// <param name="eventId">ID of the <see cref="Event"/> to retrieve payloads for.</param>
        /// <returns>A list of <see cref="Payload"/> objects.</returns>
        public async Task<List<Payload>> RetrieveAllPayloads(string eventId, CancellationToken cancellationToken = default) => await RequestAsync<List<Payload>>(Method.Get, $"events/{eventId}/payloads", cancellationToken: cancellationToken, rootElement: "payloads");

        /// <summary>
        ///     Retrieve a specific <see cref="Payload"/> for an <see cref="Event"/>.
        /// </summary>
        /// <param name="eventId">ID of the <see cref="Event"/> to retrieve payload for.</param>
        /// <param name="payloadId">ID of payload to retrieve.</param>
        /// <returns>A <see cref="Payload"/> object.</returns>
        /// <exception cref="InvalidRequestError">Thrown if the specified payload ID is malformed.</exception>
        /// <exception cref="NotFoundError">Thrown if the specified payload is not found.</exception>
        public async Task<Payload> RetrievePayload(string eventId, string payloadId, CancellationToken cancellationToken = default) => await RequestAsync<Payload>(Method.Get, $"events/{eventId}/payloads/{payloadId}", cancellationToken);

        #endregion
    }
}
