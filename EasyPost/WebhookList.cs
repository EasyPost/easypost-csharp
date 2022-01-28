// WebhookList.cs
// See LICENSE for licensing info.

using System.Collections.Generic;

namespace EasyPost
{
    public class WebhookList : Resource
    {
        public List<Webhook> webhooks { get; set; }
    }
}
