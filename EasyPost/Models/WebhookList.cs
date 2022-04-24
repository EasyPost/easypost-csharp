using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    internal class WebhookList : Resource
    {
        [JsonProperty("webhooks")]
        public List<Webhook> webhooks { get; set; }
    }
}
