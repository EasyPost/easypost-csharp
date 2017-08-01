using System;
using System.Collections.Generic;

using RestSharp;

namespace EasyPost {
    public class Webhook : Resource {
        public string id { get; set; }
        public string mode { get; set; }
        public string url { get; set; }
        public DateTime? disabled_at { get; set; }

        /// <summary>
        /// Get a list of scan forms.
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>List of EasyPost.Webhook insteances.</returns>
        public static List<Webhook> List(Dictionary<string, object> parameters = null, string apiKey = null) {
            Request request = new Request("webhooks");

            WebhookList webhookList = request.Execute<WebhookList>(apiKey);
            return webhookList.webhooks;
        }

        /// <summary>
        /// Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.User instance.</returns>
        public static Webhook Retrieve(string id, string apiKey = null) {
            Request request = new Request("webhooks/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Webhook>(apiKey);
        }

        /// <summary>
        /// Create a Webhook.
        /// </summary>
        /// <param name="parameters">
        /// Dictionary containing parameters to create the carrier account with. Valid pairs:
        ///   * { "url", string } Url of the webhook that events will be sent to.
        /// All invalid keys will be ignored.
        /// </param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.Webhook instance.</returns>
        public static Webhook Create(Dictionary<string, object> parameters, string apiKey = null) {
            Request request = new Request("webhooks", Method.POST);
            request.AddBody(parameters, "webhook");

            return request.Execute<Webhook>(apiKey);
        }

        /// <summary>
        /// Enable a Webhook that has been disabled previously.
        /// </summary>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public void Update(string apiKey = null) {
            Request request = new Request("webhooks/{id}", Method.PUT);
            request.AddUrlSegment("id", id);

            Merge(request.Execute<Webhook>(apiKey));
        }

        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        public void Destroy(string apiKey = null) {
            Request request = new Request("webhooks/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.Execute<Webhook>(apiKey);
        }
    }
}
