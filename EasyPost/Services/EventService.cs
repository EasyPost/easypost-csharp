using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.API;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Parameters.Event;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#events">event-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class EventService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EventService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal EventService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     List all <see cref="Event"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-events">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters">Dictionary containing parameters to filter the results on. See <see cref="Parameters.Event.All"/> for valid keys.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="EventCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<EventCollection> All(Dictionary<string, object>? parameters = null, CancellationToken cancellationToken = default)
        {
            EventCollection collection = await RequestAsync<EventCollection>(Method.Get, "events", cancellationToken, parameters);
            collection.Filters = Parameters.Event.All.FromDictionary(parameters);
            return collection;
        }

        /// <summary>
        ///     List all <see cref="Event"/>s.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-events">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.Event.All"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="EventCollection"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<EventCollection> All(All parameters, CancellationToken cancellationToken = default)
        {
            EventCollection collection = await RequestAsync<EventCollection>(Method.Get, "events", cancellationToken, parameters.ToDictionary());
            collection.Filters = parameters;
            return collection;
        }

        /// <summary>
        ///     Get the next page of a paginated <see cref="EventCollection"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-events">Related API documentation</a>.
        /// </summary>
        /// <param name="collection">The <see cref="EventCollection"/> to get the next page of.</param>
        /// <param name="pageSize">The size of the next page.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>The next page, as a <see cref="EventCollection"/> instance.</returns>
        /// <exception cref="EndOfPaginationError">Thrown if there is no next page to retrieve.</exception>
        [CrudOperations.Read]
        public async Task<EventCollection> GetNextPage(EventCollection collection, int? pageSize = null, CancellationToken cancellationToken = default) => await collection.GetNextPage<EventCollection, All>(async parameters => await All((All)parameters, cancellationToken), collection.Events, pageSize);

        /// <summary>
        ///     Retrieve an <see cref="Event"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-an-event">Related API documentation</a>.
        /// </summary>
        /// <param name="id">The ID of the <see cref="Event"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An <see cref="Event"/> instance.</returns>
        [CrudOperations.Read]
        public async Task<Event> Retrieve(string id, CancellationToken cancellationToken = default) => await RequestAsync<Event>(Method.Get, $"events/{id}", cancellationToken);

        /// <summary>
        ///     Retrieve all <see cref="Payload"/>s for an <see cref="Event"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-list-of-payloads">Related API documentation</a>.
        /// </summary>
        /// <param name="eventId">ID of the <see cref="Event"/> to retrieve payloads for.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A list of <see cref="Payload"/> objects.</returns>
        public async Task<List<Payload>> RetrieveAllPayloads(string eventId, CancellationToken cancellationToken = default) => await RequestAsync<List<Payload>>(Method.Get, $"events/{eventId}/payloads", cancellationToken: cancellationToken, rootElement: "payloads");

        /// <summary>
        ///     Retrieve a specific <see cref="Payload"/> for an <see cref="Event"/>.
        ///     <a href="https://www.easypost.com/docs/api#retrieve-a-payload">Related API documentation</a>.
        /// </summary>
        /// <param name="eventId">ID of the <see cref="Event"/> to retrieve payload for.</param>
        /// <param name="payloadId">ID of the <see cref="Payload"/> to retrieve.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="Payload"/> object.</returns>
        /// <exception cref="InvalidRequestError">Thrown if the specified payload ID is malformed.</exception>
        /// <exception cref="NotFoundError">Thrown if the specified payload is not found.</exception>
        public async Task<Payload> RetrievePayload(string eventId, string payloadId, CancellationToken cancellationToken = default) => await RequestAsync<Payload>(Method.Get, $"events/{eventId}/payloads/{payloadId}", cancellationToken);

        #endregion
    }
}
