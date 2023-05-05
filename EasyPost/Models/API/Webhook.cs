using System;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost webhook.
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
