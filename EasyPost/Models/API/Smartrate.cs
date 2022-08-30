﻿using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Smartrate : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("billing_type")]
        public string billing_type { get; set; }
        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("carrier_account_id")]
        public string carrier_account_id { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("delivery_date")]
        public DateTime? delivery_date { get; set; }
        [JsonProperty("delivery_date_guaranteed")]
        public bool delivery_date_guaranteed { get; set; }
        [JsonProperty("delivery_days")]
        public int? delivery_days { get; set; }
        [JsonProperty("est_delivery_days")]
        public int? est_delivery_days { get; set; }
        [JsonProperty("list_currency")]
        public string list_currency { get; set; }
        [JsonProperty("list_rate")]
        public double list_rate { get; set; }
        [JsonProperty("rate")]
        public double rate { get; set; }
        [JsonProperty("retail_currency")]
        public string retail_currency { get; set; }
        [JsonProperty("retail_rate")]
        public double retail_rate { get; set; }
        [JsonProperty("service")]
        public string service { get; set; }
        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
        [JsonProperty("time_in_transit")]
        public TimeInTransit time_in_transit { get; set; }

        #endregion
    }
}
