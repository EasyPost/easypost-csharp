using System;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class Webhook : EasyPostObject
    {
        [JsonProperty("disabled_at")]
        public DateTime? disabled_at { get; set; }
        [JsonProperty("url")]
        public string? url { get; set; }

        /// <summary>
        ///     Delete this webhook.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<bool> Delete()
        {
            return await Request(Method.Delete, $"webhooks/{id}");
        }

        /// <summary>
        ///     Enable a Webhook that has been disabled previously.
        /// </summary>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Webhook> Update()
        {
            return await Update<Webhook>(Method.Patch, $"webhooks/{id}");
        }
    }
}
