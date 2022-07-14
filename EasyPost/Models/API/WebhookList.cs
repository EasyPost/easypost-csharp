using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    internal class WebhookList : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("webhooks")]
        public List<Webhook>? Webhooks { get; set; }

        #endregion
    }
}
