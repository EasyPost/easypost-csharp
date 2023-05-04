using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Webhook : EasyPostObject, IWebhookParameter
    {
        #region JSON Properties

        [JsonProperty("disabled_at")]
        public DateTime? DisabledAt { get; internal set; }
        [JsonProperty("url")]
        public string? Url { get; internal set; }

        #endregion

        internal Webhook()
        {
        }
    }
}
