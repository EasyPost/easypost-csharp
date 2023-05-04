using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Models.API;
using Newtonsoft.Json;

namespace EasyPost.Models.Shared
{
    public class BaseUser : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("api_keys")]
        public List<ApiKey>? ApiKeys { get; internal set; }
        [JsonProperty("balance")]
        public string? Balance { get; internal set; }
        [JsonProperty("children")]
        public List<User>? Children { get; internal set; }
        [JsonProperty("email")]
        public string? Email { get; internal set; }
        [JsonProperty("name")]
        public string? Name { get; internal set; }
        [JsonProperty("parent_id")]
        public string? ParentId { get; internal set; }
        [JsonProperty("password")]
        public string? Password { get; internal set; }
        [JsonProperty("password_confirmation")]
        public string? PasswordConfirmation { get; internal set; }
        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; internal set; }
        [JsonProperty("price_per_shipment")]
        public string? PricePerShipment { get; internal set; }
        [JsonProperty("recharge_amount")]
        public string? RechargeAmount { get; internal set; }
        [JsonProperty("recharge_threshold")]
        public string? RechargeThreshold { get; internal set; }
        [JsonProperty("secondary_recharge_amount")]
        public string? SecondaryRechargeAmount { get; internal set; }

        #endregion
    }
}
