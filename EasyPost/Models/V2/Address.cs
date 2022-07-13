using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Parameters.V2;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class Address : Base.Address
    {
        #region JSON Properties

        [JsonProperty("carrier_facility")]
        public string? CarrierFacility { get; set; }
        [JsonProperty("federal_tax_id")]
        public string? FederalTaxId { get; set; }
        [JsonProperty("residential")]
        public bool? Residential { get; set; }
        [JsonProperty("state_tax_id")]
        public string? StateTaxId { get; set; }
        [JsonProperty("verify")]
        public List<string>? ToVerify { get; set; }
        [JsonProperty("verify_strict")]
        public List<string>? ToVerifyStrict { get; set; }
        [JsonProperty("verifications")]
        public Verifications? Verifications { get; set; }

        #endregion

        /// <summary>
        ///     Verify this address.
        /// </summary>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<Address> Verify(Addresses.Verify? parameters = null)
        {
            if (Id == null)
            {
                throw new PropertyMissingException("id");
            }

            return await Update<Address>(Method.Get, $"addresses/{Id}/verify", parameters, "address");
        }
    }
}
