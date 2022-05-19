using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.V2
{
    public class Address : Base.Address
    {
        [JsonProperty("carrier_facility")]
        public string carrier_facility { get; set; }
        [JsonProperty("federal_tax_id")]
        public string federal_tax_id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("residential")]
        public bool? residential { get; set; }
        [JsonProperty("state_tax_id")]
        public string state_tax_id { get; set; }
        [JsonProperty("verifications")]
        public Verifications verifications { get; set; }
        [JsonProperty("verify")]
        public List<string> verify { get; set; }
        [JsonProperty("verify_strict")]
        public List<string> verify_strict { get; set; }

        /// <summary>
        ///     Verify this address.
        /// </summary>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        public async Task Verify(string? carrier = null)
        {
            if (id == null)
            {
                throw new PropertyMissing("id");
            }

            Dictionary<string, object> parameters = new Dictionary<string, object>();
            if (carrier != null)
            {
                parameters.Add("carrier", carrier);
            }

            await Update<Address>(Method.Get,$"addresses/{id}/verify", parameters, "address");
        }
    }
}
