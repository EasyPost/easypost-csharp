using System;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Parameters;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Webhook : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("disabled_at")]
        public DateTime? DisabledAt { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }

        #endregion

        /// <summary>
        ///     Delete this webhook.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<bool> Delete()
        {
            return await Request(Method.Delete, $"webhooks/{Id}");
        }

        /// <summary>
        ///     Update a Webhook. A disabled webhook will be enabled.
        /// <param name="parameters">
        ///     Dictionary containing parameters to update the webhook with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     * { "webhook_secret", string } Secret token to include as a header when sending a webhook.
        ///     All invalid keys will be ignored.
        /// </param>
        /// </summary>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Webhook> Update(Webhooks.Update? parameters = null)
        {
            return await Update<Webhook>(Method.Patch, $"webhooks/{Id}", parameters);
        }
    }
}
