using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Models.API;
using Newtonsoft.Json;

namespace EasyPost.Models.Shared
{
    /// <summary>
    ///     Base class for all EasyPost user objects.
    /// </summary>
    public class BaseUser : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("api_keys")]
        public List<ApiKey>? ApiKeys { get; set; }
        [JsonProperty("balance")]
        public string? Balance { get; set; }
        [JsonProperty("children")]
        public List<User>? Children { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("parent_id")]
        public string? ParentId { get; set; }
        [JsonProperty("password")]
        public string? Password { get; set; }
        [JsonProperty("password_confirmation")]
        public string? PasswordConfirmation { get; set; }
        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; set; }
        [JsonProperty("price_per_shipment")]
        public string? PricePerShipment { get; set; }
        [JsonProperty("recharge_amount")]
        public string? RechargeAmount { get; set; }
        [JsonProperty("recharge_threshold")]
        public string? RechargeThreshold { get; set; }
        [JsonProperty("secondary_recharge_amount")]
        public string? SecondaryRechargeAmount { get; set; }

        #endregion
    }
}
