using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class CarrierAccount : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("credentials")]
        public Dictionary<string, object> credentials { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("readable")]
        public string readable { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("test_credentials")]
        public Dictionary<string, object?> test_credentials { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("billing_type")]
        public string billing_type { get; set; }

        #endregion

        /// <summary>
        ///     Remove this CarrierAccount from your account.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task Delete()
        {
            await Delete($"carrier_accounts/{id}");
        }

        /// <summary>
        ///     Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        public async Task<CarrierAccount> Update(Dictionary<string, object?> parameters)
        {
            return await Update<CarrierAccount>(Method.Patch, $"carrier_accounts/{id}", parameters);
        }
    }
}
