// <copyright file="WebhookList.cs" company="EasyPost">
// Copyright (c) EasyPost. All rights reserved.
// </copyright>

using System.Collections.Generic;

namespace EasyPost
{
    public class WebhookList : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public List<Webhook> webhooks { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
