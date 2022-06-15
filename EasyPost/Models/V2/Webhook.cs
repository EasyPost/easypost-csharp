using System;
using System.Threading.Tasks;
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
        public async Task<bool> Delete()
        {
            CheckFunctionalityCompatible(nameof(Delete));

            return await Request(Method.Delete, $"webhooks/{id}");
        }

        /// <summary>
        ///     Enable a Webhook that has been disabled previously.
        /// </summary>
        public async Task<Webhook> Update()
        {
            CheckFunctionalityCompatible(nameof(Update));

            return await Update<Webhook>(Method.Patch, $"webhooks/{id}");
        }
    }
}
