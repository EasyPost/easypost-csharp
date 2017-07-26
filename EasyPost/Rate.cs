﻿using RestSharp;

using System;

namespace EasyPost {
    public class Rate : Resource {
        public string id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public string mode { get; set; }
        public string service { get; set; }
        public string rate { get; set; }
        public string list_rate { get; set; }
        public string retail_rate { get; set; }
        public string currency { get; set; }
        public string list_currency { get; set; }
        public string retail_currency { get; set; }
        public int est_delivery_days { get; set; }
        public DateTime delivery_date {get; set; } 
        public bool delivery_date_guaranteed {get; set;}
        public int delivery_days {get; set;}
        public string carrier { get; set; }
        public string shipment_id { get; set; }
        public string carrier_account_id { get; set; }

        /// <summary>
        /// Retrieve a Rate from its id.
        /// </summary>
        /// <param name="id">String representing a Rate. Starts with "rate_".</param>
        /// <param name="apiKey">Optional: Force a specific apiKey, bypassing the ClientManager singleton object.
        ///     Required for multithreaded applications using multiple apiKeys.
        ///     The singleton of the ClientManager does not allow this to work in the above case.
        /// </param>
        /// <returns>EasyPost.Rate instance.</returns>
        public static Rate Retrieve(string id, string apiKey = null) {
            Request request = new Request("rates/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Rate>(apiKey);
        }
    }
}
