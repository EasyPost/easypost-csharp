using System;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class CustomsItem : Resource
    {
        [JsonProperty("code")]
        public string code { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("hs_tariff_number")]
        public string hs_tariff_number { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("origin_country")]
        public string origin_country { get; set; }
        [JsonProperty("quantity")]
        public int quantity { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("value")]
        public double? value { get; set; }
        [JsonProperty("weight")]
        public double weight { get; set; }
    }
}
