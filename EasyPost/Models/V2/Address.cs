using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class Address : Base.Address
    {
        [JsonProperty("carrier_facility")]
        public string? CarrierFacility { get; set; }
        [JsonProperty("federal_tax_id")]
        public string? FederalTaxId { get; set; }
        [JsonProperty("residential")]
        public bool? Residential { get; set; }
        [JsonProperty("state_tax_id")]
        public string? StateTaxId { get; set; }
        [JsonProperty("verifications")]
        public Verifications? Verifications { get; set; }
        [JsonProperty("verify")]
        public List<string>? ToVerify { get; set; }
        [JsonProperty("verify_strict")]
        public List<string>? ToVerifyStrict { get; set; }

        /// <summary>
        ///     Verify this address.
        /// </summary>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        [ApiCompatibility(ApiVersion.V2)]
        public async Task<Address> Verify(string? carrier = null)
        {
            if (Id == null)
            {
                throw new PropertyMissing("id");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (carrier != null)
            {
                parameters.Add("carrier", carrier);
            }

            return await Update<Address>(Method.Get, $"addresses/{Id}/verify", parameters, "address");
        }
    }
}
