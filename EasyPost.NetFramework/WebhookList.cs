using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class WebhookList : Resource
    {
        [JsonProperty("webhooks")]
        public List<Webhook> webhooks { get; set; }
    }
}
