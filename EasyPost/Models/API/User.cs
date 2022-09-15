﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Utilities.Annotations;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class User : EasyPostObject
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

        #region CRUD Operations

        /// <summary>
        ///     Update the User's brand.
        /// </summary>
        /// <param name="parameters">
        ///     Dictionary containing parameters to update the brand with. Valid pairs:
        ///     * {"ad", string} Base64 encoded string for a png, gif, jpeg, or svg.
        ///     * {"ad_href", string} Valid URL under 255 characters
        ///     * {"background_color", string} Valid hex code
        ///     * {"color", string} Valid hex code
        ///     * {"logo", string} Base64 encoded string for a png, gif, jpeg, or svg
        ///     * {"logo_href", string} Valid URL under 255 characters
        ///     * {"theme", string} "theme1" or "theme2"
        ///     All invalid keys will be ignored.
        /// </param>
        /// <returns>EasyPost.Brand instance.</returns>
        [CrudOperations.Create]
        public async Task<Brand> UpdateBrand(Dictionary<string, object> parameters)
        {
            return await Request<Brand>(Method.Patch, $"users/{Id}/brand", parameters);
        }

        /// <summary>
        ///     Update the User associated with the api_key specified.
        /// </summary>
        /// <param name="parameters">
        ///     Optional dictionary containing parameters to create the carrier account with. Valid pairs:
        ///     * {"name", string} Name on the account.
        ///     * {"email", string} Email on the account. Can only be updated on the parent account.
        ///     * {"phone_number", string} Phone number on the account. Can only be updated on the parent account.
        ///     * {"recharge_amount", int} Recharge amount for the account in cents. Can only be updated on the parent account.
        ///     * {"secondary_recharge_amount", int} Secondary recharge amount for the account in cents. Can only be updated on the
        ///     parent account.
        ///     * {"recharge_threshold", int} Recharge threshold for the account in cents. Can only be updated on the parent
        ///     account.
        ///     All invalid keys will be ignored.
        /// </param>
        [CrudOperations.Update]
        public async Task<User> Update(Dictionary<string, object> parameters)
        {
            await Update<User>(Method.Patch, $"users/{Id}", parameters);
            return this;
        }

        /// <summary>
        ///     Delete the user.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [CrudOperations.Delete]
        public async Task Delete()
        {
            await DeleteNoResponse($"users/{Id}");
        }

        #endregion
    }
}