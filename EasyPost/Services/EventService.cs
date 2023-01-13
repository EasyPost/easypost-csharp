using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.API;
using EasyPost.Models.API;
using EasyPost.Utilities.Annotations;

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
        ///     Retrieve an Event from its id.
        /// </summary>
        /// <param name="id">String representing a Event. Starts with "evt_".</param>
        /// <returns>EasyPost.Event instance.</returns>
        [CrudOperations.Read]
        public async Task<Event> Retrieve(string id) => await Get<Event>($"events/{id}");

        /// <summary>
        ///     Retrieve all <see cref="Payload"/>s for an <see cref="Event"/>.
        /// </summary>
        /// <param name="event"><see cref="Event"/> to retrieve payloads for.</param>
        /// <returns>A list of <see cref="Payload"/> objects.</returns>
        public async Task<List<Payload>> RetrievePayloadsForEvent(Event @event) => await Get<List<Payload>>($"events/{@event.Id}/payloads", rootElement: "payloads");

        /// <summary>
        ///     Retrieve a specific <see cref="Payload"/> for an <see cref="Event"/>.
        /// </summary>
        /// <param name="event"><see cref="Event"/> to retrieve payload for.</param>
        /// <param name="payloadId">ID of payload to retrieve.</param>
        /// <returns>A <see cref="Payload"/> object.</returns>
        /// <exception cref="InvalidRequestError">Thrown if the specified payload is not found.</exception>
        public async Task<Payload> RetrievePayloadForEvent(Event @event, string payloadId)
        {
            try
            {
                return await Get<Payload>($"events/{@event.Id}/payloads/{payloadId}");
            }
            catch (InternalServerError)
            {
                // The API returns a 500 error when the payload is not found.
                // This is a catch clause to instead throw a more appropriate exception.
                throw new InvalidRequestError($"Payload with id {payloadId} not found for event {@event.Id}.", 422);
            }
        }

        #endregion
    }
}
