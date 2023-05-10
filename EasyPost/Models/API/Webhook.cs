using System;
using EasyPost._base;
using EasyPost.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#webhook-object">EasyPost webhook</a>.
    /// </summary>
    public class Webhook : EasyPostObject, IWebhookParameter
    {
        #region JSON Properties

        [JsonProperty("disabled_at")]
        public DateTime? DisabledAt { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Webhook"/> class.
        /// </summary>
        internal Webhook()
        {
        }
    }
}
