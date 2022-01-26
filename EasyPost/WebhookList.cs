// <copyright file="WebhookList.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace EasyPost
{
    public class WebhookList : Resource
    {
        public List<Webhook> webhooks { get; set; }
    }
}
