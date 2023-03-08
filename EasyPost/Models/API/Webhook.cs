using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Webhook : EasyPostObject, IWebhookParameter
    {
        #region JSON Properties

        [JsonProperty("disabled_at")]
        public DateTime? DisabledAt { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }

        #endregion

        internal Webhook()
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Update a Webhook. A disabled webhook will be enabled.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to update the webhook with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     * { "webhook_secret", string } Secret token to include as a header when sending a webhook.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>The updated Webhook.</returns>
        [CrudOperations.Update]
        public async Task<Webhook> Update(Dictionary<string, object>? parameters = null)
        {
            await Update<Webhook>(Method.Patch, $"webhooks/{Id}", parameters);
            return this;
        }

        /// <summary>
        ///     Delete this webhook.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [CrudOperations.Delete]
        public async Task Delete() => await DeleteNoResponse($"webhooks/{Id}");

        #endregion
    }
}
