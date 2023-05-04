using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Address : EasyPostObject, IAddressParameter
    {
        #region JSON Properties

        [JsonProperty("carrier_facility")]
        public string? CarrierFacility { get; internal set; }
        [JsonProperty("city")]
        public string? City { get; internal set; }
        [JsonProperty("company")]
        public string? Company { get; internal set; }
        [JsonProperty("country")]
        public string? Country { get; internal set; }
        [JsonProperty("email")]
        public string? Email { get; internal set; }
        [JsonProperty("error")]
        public string? Error { get; internal set; }
        [JsonProperty("federal_tax_id")]
        public string? FederalTaxId { get; internal set; }
        [JsonProperty("message")]
        public string? Message { get; internal set; }
        [JsonProperty("name")]
        public string? Name { get; internal set; }
        [JsonProperty("phone")]
        public string? Phone { get; internal set; }
        [JsonProperty("residential")]
        public bool? Residential { get; internal set; }
        [JsonProperty("state")]
        public string? State { get; internal set; }

        [JsonProperty("state_tax_id")]
        public string? StateTaxId { get; internal set; }
        [JsonProperty("street1")]
        public string? Street1 { get; internal set; }
        [JsonProperty("street2")]
        public string? Street2 { get; internal set; }
        [JsonProperty("verifications")]
        public Verifications? Verifications { get; internal set; }
        [JsonProperty("zip")]
        public string? Zip { get; internal set; }

        #endregion

        internal Address()
        {
        }
    }

    public class AddressCollection : PaginatedCollection<Address>
    {
        #region JSON Properties

        [JsonProperty("addresses")]
        public List<Address>? Addresses { get; internal set; }

        #endregion

        internal AddressCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Address> entries, int? pageSize = null)
        {
            BetaFeatures.Parameters.Addresses.All parameters = Filters != null ? (BetaFeatures.Parameters.Addresses.All)Filters : new BetaFeatures.Parameters.Addresses.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
