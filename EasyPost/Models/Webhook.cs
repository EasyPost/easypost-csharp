using System;
using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models
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
            Request request = new Request("webhooks/{id}", Method.Put);
            request.AddUrlSegment("id", id);

            Merge(await request.Execute<Webhook>());
        }
    }
}
