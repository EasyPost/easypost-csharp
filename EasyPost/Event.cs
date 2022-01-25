using System;
using System.Collections.Generic;

using RestSharp;

namespace EasyPost {
    public class Event : Resource {
#pragma warning disable IDE1006 // Naming Styles
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
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Resend the last Event for a specific EasyPost object instance.
        /// </summary>
        /// <param name="id">String representing an EasyPost object instance.</param>
        public static void Create(string id) {
            Request request = new Request("v2/events", Method.POST);
            request.AddQueryString(new Dictionary<string, object>() { { "result_id", id } });
        }

        /// <summary>
        /// Retrieve an Event from its id.
        /// </summary>
        /// <param name="id">String representing a Event. Starts with "evt_".</param>
        /// <returns>EasyPost.Event instance.</returns>
        public static Event Retrieve(string id) {
            Request request = new Request("v2/events/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Event>();
        }
    }
}