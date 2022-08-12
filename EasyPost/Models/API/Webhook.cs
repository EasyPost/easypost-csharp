using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Webhook : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("disabled_at")]
        public DateTime? disabled_at { get; set; }
        [JsonProperty("mode")]
        public new string mode { get; set; }
        [JsonProperty("url")]
        public string url { get; set; }

        #endregion

        /// <summary>
        ///     Delete this webhook.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task Delete()
        {
            await Delete($"webhooks/{id}");
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
        public async Task<Webhook> Update(Dictionary<string, object>? parameters = null)
        {
            return await Update<Webhook>(Method.Patch, $"webhooks/{id}", parameters);
        }
    }
}
