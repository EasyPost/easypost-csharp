﻿using RestSharp;

using System;

namespace EasyPost {
    public class Rate : Resource {
#pragma warning disable IDE1006 // Naming Styles
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
        public int? est_delivery_days { get; set; }
        public DateTime? delivery_date { get; set; } 
        public bool delivery_date_guaranteed { get; set;}
        public int? delivery_days { get; set;}
        public string carrier { get; set; }
        public string shipment_id { get; set; }
        public string carrier_account_id { get; set; }
#pragma warning restore IDE1006 // Naming Styles

        /// <summary>
        /// Retrieve a Rate from its id.
        /// </summary>
        /// <param name="id">String representing a Rate. Starts with "rate_".</param>
        /// <returns>EasyPost.Rate instance.</returns>
        public static Rate Retrieve(string id) {
            Request request = new Request("v2/rates/{id}");
            request.AddUrlSegment("id", id);

            return request.Execute<Rate>();
        }
    }
}
