using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class BaseUser : Resource
    {
        #region JSON Properties

        [JsonProperty("api_keys")]
        public List<ApiKey> api_keys { get; set; }
        [JsonProperty("balance")]
        public string balance { get; set; }
        [JsonProperty("children")]
        public List<User> children { get; set; }
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("email")]
        public string email { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("parent_id")]
        public string parent_id { get; set; }
        [JsonProperty("password")]
        public string password { get; set; }
        [JsonProperty("password_confirmation")]
        public string password_confirmation { get; set; }
        [JsonProperty("phone_number")]
        public string phone_number { get; set; }
        [JsonProperty("price_per_shipment")]
        public string price_per_shipment { get; set; }
        [JsonProperty("recharge_amount")]
        public string recharge_amount { get; set; }
        [JsonProperty("recharge_threshold")]
        public string recharge_threshold { get; set; }
        [JsonProperty("secondary_recharge_amount")]
        public string secondary_recharge_amount { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        #endregion
    }
}
