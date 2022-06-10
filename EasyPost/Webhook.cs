using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class Webhook : Resource
    {
        [JsonProperty("disabled_at")]
        public DateTime? disabled_at { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("url")]
        public string url { get; set; }

        /// <summary>
        ///     Delete this webhook.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> Delete()
        {
            Request request = new Request("webhooks/{id}", Method.Delete);
            request.AddUrlSegment("id", id);
            return await request.Execute();
        }

        /// <summary>
        ///     Enable a Webhook that has been disabled previously.
        /// </summary>
        public async Task Update()
        {
            Request request = new Request("webhooks/{id}", Method.Patch);
            request.AddUrlSegment("id", id);

            Merge(await request.Execute<Webhook>());
        }

        /// <summary>
        ///     Create a Webhook.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Webhook instance.</returns>
        public static async Task<Webhook> Create(Dictionary<string, object> parameters)
        {
            Request request = new Request("webhooks", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "webhook", parameters
                }
            });

            return await request.Execute<Webhook>();
        }


        /// <summary>
        ///     Get a list of scan forms.
        /// </summary>
        /// <returns>List of EasyPost.Webhook instances.</returns>
        public static async Task<List<Webhook>> All(Dictionary<string, object>? parameters = null)
        {
            Request request = new Request("webhooks", Method.Get);
            request.AddParameters(parameters);

            WebhookList webhookList = await request.Execute<WebhookList>();
            return webhookList.webhooks;
        }

        /// <summary>
        ///     Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <returns>EasyPost.User instance.</returns>
        public static async Task<Webhook> Retrieve(string id)
        {
            Request request = new Request("webhooks/{id}", Method.Get);
            request.AddUrlSegment("id", id);

            return await request.Execute<Webhook>();
        }
    }
}
