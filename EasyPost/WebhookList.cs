// WebhookList.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System.Collections.Generic;

namespace EasyPost
{
    public class WebhookList : Resource
    {
        public List<Webhook> webhooks { get; set; }
    }
}
