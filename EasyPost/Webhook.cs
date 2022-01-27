﻿// Webhook.cs
// Copyright (c) 2022 EasyPost
// All rights reserved.

using System;
using System.Collections.Generic;
using RestSharp;

namespace EasyPost
{
    public class Webhook : Resource
    {
        public DateTime? disabled_at { get; set; }

        public string id { get; set; }

        public string mode { get; set; }

        public string url { get; set; }

        /// <summary>
        ///     Delete this webhook.
        /// </summary>
        public void Destroy()
        {
            var request = new Request("webhooks/{id}", Method.DELETE);
            request.AddUrlSegment("id", id);
            request.Execute();
        }

        /// <summary>
        ///     Enable a Webhook that has been disabled previously.
        /// </summary>
        public void Update()
        {
            var request = new Request("webhooks/{id}", Method.PUT);
            request.AddUrlSegment("id", id);

            Merge(request.Execute<Webhook>());
        }

        /// <summary>
        ///     Create a Webhook.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * { "url", string } Url of the webhook that events will be sent to.
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Webhook instance.</returns>
        public static Webhook Create(Dictionary<string, object> parameters)
        {
            var request = new Request("webhooks", Method.POST);
            request.AddBody(new Dictionary<string, object> { { "webhook", parameters } });

            return request.Execute<Webhook>();
        }


        /// <summary>
        ///     Get a list of scan forms.
        /// </summary>
        /// <returns>List of EasyPost.Webhook instances.</returns>
        public static List<Webhook> List(Dictionary<string, object> parameters = null)
        {
            var request = new Request("webhooks");

            var webhookList = request.Execute<WebhookList>();
            return webhookList.webhooks;
        }

        /// <summary>
        ///     Retrieve a Webhook from its id.
        /// </summary>
        /// <param name="id">String representing a webhook. Starts with "hook_".</param>
        /// <returns>EasyPost.User instance.</returns>
        public static Webhook Retrieve(string id)
        {
            var request = new Request("webhooks/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Webhook>();
        }
    }
}
