using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost.Base
{
    public abstract class Address : Resource
    {
        [JsonProperty("carrier_facility")]
        public string carrier_facility { get; set; }
        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("company")]
        public string company { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("error")]
        public string error { get; set; }
        [JsonProperty("federal_tax_id")]
        public string federal_tax_id { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("phone")]
        public string phone { get; set; }
        [JsonProperty("residential")]
        public bool? residential { get; set; }
        [JsonProperty("state")]
        public string state { get; set; }
        [JsonProperty("state_tax_id")]
        public string state_tax_id { get; set; }
        [JsonProperty("street1")]
        public string street1 { get; set; }
        [JsonProperty("street2")]
        public string street2 { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("verifications")]
        public Verifications verifications { get; set; }
        [JsonProperty("verify")]
        public List<string> verify { get; set; }
        [JsonProperty("verify_strict")]
        public List<string> verify_strict { get; set; }
        [JsonProperty("zip")]
        public string zip { get; set; }
    }
}
