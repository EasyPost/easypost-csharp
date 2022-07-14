using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Parameters;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class CarrierAccount : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("billing_type")]
        public string? BillingType { get; set; }
        [JsonProperty("credentials")]
        public Dictionary<string, object>? Credentials { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("readable")]
        public string? Readable { get; set; }
        [JsonProperty("reference")]
        public string? Reference { get; set; }
        [JsonProperty("test_credentials")]
        public Dictionary<string, object>? TestCredentials { get; set; }
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion


        /// <summary>
        ///     Remove this CarrierAccount from your account.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<bool> Delete()
        {
            return await Request(Method.Delete, $"carrier_accounts/{Id}");
        }

        /// <summary>
        ///     Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<CarrierAccount> Update(CarrierAccounts.Update parameters)
        {
            return await Update<CarrierAccount>(Method.Patch, $"carrier_accounts/{Id}", parameters);
        }
    }
}
