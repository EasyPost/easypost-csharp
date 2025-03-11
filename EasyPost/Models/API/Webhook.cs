using System;
using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Webhook
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/webhooks#webhook-object">EasyPost webhook</a>.
    /// </summary>
    public class Webhook : EasyPostObject, Parameters.IWebhookParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The timestamp of the last time this webhook was disabled.
        /// </summary>
        [JsonProperty("disabled_at")]
        public DateTime? DisabledAt { get; set; }

        /// <summary>
        ///     The URL to which this webhook will send its POST request.
        /// </summary>
        [JsonProperty("url")]
        public string? Url { get; set; }

        /// <summary>
        ///     custom headers
        /// </summary>
        [JsonProperty("customheaders")]
        public List<WebhookCustomHeader>? CustomHeaders { get; set; }

        #endregion

    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.Webhook
