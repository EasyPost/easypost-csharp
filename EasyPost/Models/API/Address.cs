using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Parameters;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Address : EasyPostObject
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
