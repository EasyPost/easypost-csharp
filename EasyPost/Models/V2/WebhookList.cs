using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    internal class WebhookList : Resource
    {
        [JsonProperty("webhooks")]
        public List<Webhook>? webhooks { get; set; }
    }
}
