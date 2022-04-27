using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models
{
    public class CarrierAccount : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("credentials")]
        public Dictionary<string, object> credentials { get; set; }
        [JsonProperty("description")]
        public string description { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("readable")]
        public string readable { get; set; }
        [JsonProperty("reference")]
        public string reference { get; set; }
        [JsonProperty("test_credentials")]
        public Dictionary<string, object> test_credentials { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        /// <summary>
        ///     Remove this CarrierAccount from your account.
        /// </summary>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> Delete()
        {
            return await Request(Method.Delete, $"carrier_accounts/{id}");
        }

        /// <summary>
        ///     Update this CarrierAccount.
        /// </summary>
        /// <param name="parameters">See CarrierAccount.Create for more details.</param>
        public async Task Update(Dictionary<string, object> parameters)
        {
            await Update<CarrierAccount>(Method.Put, $"carrier_accounts/{id}", parameters);
        }
    }
}
