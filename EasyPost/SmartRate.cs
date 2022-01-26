using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

namespace EasyPost
{
    public class SmartrateResult : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public List<Smartrate> result { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }

    public class Smartrate : Resource
    {
#pragma warning disable IDE1006 // Naming Styles
        public string carrier { get; set; }
        public string carrier_account_id { get; set; }
        public DateTime created_at { get; set; }
        public string currency { get; set; }
        public DateTime? delivery_date { get; set; }
        public bool delivery_date_guaranteed { get; set; }
        public int? delivery_days { get; set; }
        public int? est_delivery_days { get; set; }
        public string id { get; set; }
        public string list_currency { get; set; }
        public double list_rate { get; set; }
        public string mode { get; set; }
        public double rate { get; set; }
        public string retail_currency { get; set; }
        public double retail_rate { get; set; }
        public string service { get; set; }
        public string shipment_id { get; set; }
        public TimeInTransit time_in_transit { get; set; }
        public DateTime updated_at { get; set; }
#pragma warning restore IDE1006 // Naming Styles
    }
}
