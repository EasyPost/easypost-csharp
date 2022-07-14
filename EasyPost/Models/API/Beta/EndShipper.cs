using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Parameters.Beta;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API.Beta
{
    public class EndShipper : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("company")]
        public string? Company { get; set; }
        [JsonProperty("country")]
        public string? Country { get; set; }
        [JsonProperty("email")]
        public string? Email { get; set; }
        [JsonProperty("error")]
        public string? Error { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }

        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("phone")]
        public string? Phone { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("street1")]
        public string? Street1 { get; set; }
        [JsonProperty("street2")]
        public string? Street2 { get; set; }
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        #endregion

        /// <summary>
        ///     Update this EndShipper. Must pass in all properties (new and existing).
        /// </summary>
        /// <param name="parameters">See EndShipper.Create for more details.</param>
        [ApiCompatibility(ApiVersion.Beta)]
        public async Task<EndShipper> Update(EndShippers.Update parameters)
        {
            return await Update<EndShipper>(Method.Put, $"end_shippers/{Id}", parameters);
        }
        // EndShipper needs Put, not Patch
    }
}
