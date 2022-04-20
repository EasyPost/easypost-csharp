using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Event : Resource
    {
        [JsonProperty("completed_urls")]
        public List<string> completed_urls { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("pending_urls")]
        public List<string> pending_urls { get; set; }
        [JsonProperty("previous_attributes")]
        public Dictionary<string, object> previous_attributes { get; set; }
        [JsonProperty("result")]
        public Dictionary<string, object> result { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Resend the last Event for a specific EasyPost object instance.
        /// </summary>
        /// <param name="id">String representing an EasyPost object instance.</param>
        public async Task<bool> Create(string id)
        {
            Request request = new Request("events", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "result_id", id
                }
            });
            return await request.Execute();
        }

        /// <summary>
        ///     Retrieve an Event from its id.
        /// </summary>
        /// <param name="id">String representing a Event. Starts with "evt_".</param>
        /// <returns>EasyPost.Event instance.</returns>
        public static async Task<Event> Retrieve(string id)
        {
            Request request = new Request("events/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Event>();
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
        public static async Task<EventCollection> All(Dictionary<string, object>? parameters = null)
        {
            parameters = parameters ?? new Dictionary<string, object>();

            Request request = new Request("events", Method.Get, parameters);

            return await request.Execute<EventCollection>();
        }
    }
}
