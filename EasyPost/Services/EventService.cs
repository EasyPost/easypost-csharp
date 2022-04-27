using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Interfaces;
using EasyPost.Models;
using RestSharp;

namespace EasyPost.Services
{
    public class EventService : Service
    {
        internal EventService(BaseClient client) : base(client)
        {
        }

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
        public async Task<EventCollection> All(Dictionary<string, object>? parameters = null)
        {
            return await Get<EventCollection>("events", parameters);
        }

        /// <summary>
        ///     Resend the last Event for a specific EasyPost object instance.
        /// </summary>
        /// <param name="id">String representing an EasyPost object instance.</param>
        public async Task<bool> Create(string id)
        {
            return await CreateBlind("events", new Dictionary<string, object>
            {
                {
                    "result_id", id
                }
            });
        }

        /// <summary>
        ///     Retrieve an Event from its id.
        /// </summary>
        /// <param name="id">String representing a Event. Starts with "evt_".</param>
        /// <returns>EasyPost.Event instance.</returns>
        public async Task<Event> Retrieve(string id)
        {
            return await Get<Event>($"events/{id}");
        }
    }
}
