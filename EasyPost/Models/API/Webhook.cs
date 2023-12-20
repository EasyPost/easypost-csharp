using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Webhook
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#webhook-object">EasyPost webhook</a>.
    /// </summary>
    public class Webhook : EasyPostObject, Parameters.IWebhookParameter
    {
        #region JSON Properties

        [JsonProperty("disabled_at")]
        public DateTime? DisabledAt { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }

        #endregion

    }
}
#pragma warning disable CA1724 // Naming conflicts with Parameters.Webhook
