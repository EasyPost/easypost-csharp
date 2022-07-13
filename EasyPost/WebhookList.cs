using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    internal class WebhookList : Resource
    {
        #region JSON Properties

        [JsonProperty("webhooks")]
        public List<Webhook> webhooks { get; set; }

        #endregion
    }
}
