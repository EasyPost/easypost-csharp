using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    internal class WebhookList : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("webhooks")]
        public List<Webhook>? Webhooks { get; set; }

        #endregion
    }
}
