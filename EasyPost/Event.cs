using System;
using System.Collections.Generic;

using RestSharp;

namespace EasyPost {
    public class Event : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public Dictionary<string, object> result { get; set; }
        public string description { get; set; }
        public string mode { get; set; }
        public Dictionary<string, object> previous_attributes { get; set; }
        public List<string> pending_urls { get; set; }
        public List<string> completed_urls { get; set; }
        public string status { get; set; }

        /// <summary>
        /// Resend the last Event for a specific EasyPost object.
        /// </summary>
        /// <param name="id">String representing an EasyPost object.</param>
        public static void Create(string id) {
            Request request = new Request("events", Method.POST);
            request.AddQueryString(new Dictionary<string, object>() { { "result_id", id } });
        }

        /// <summary>
        /// Retrieve a Event from its id.
        /// </summary>
        /// <param name="id">String representing a Event. Starts with "evt_".</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.Event instance.</returns>
        public static Event Retrieve(string id, string apiKey = null) {
            Request request = new Request("events/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Event>(apiKey);
        }
    }
}