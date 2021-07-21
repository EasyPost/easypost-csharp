using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using RestSharp.Deserializers;

//RANDOM COMMENT: Make sure to gave the get set accessors. I forgot to place it on the result property and it was returning null on the response data even though the content came back correcly.
//RestSharp uses your class as the starting point, looping through each publicly-accessible, writable property and searching for a corresponding element in the data returned.
namespace EasyPost
{
    public class SmartRateResult:Resource
    {
        //Custom json property name is not working for some reason. Maybe restsharp issue
        public List<SmartRate> result { get; set; }
    }
    public class SmartRate:Resource
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
