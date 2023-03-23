using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using EasyPost.Utilities.Internal.Attributes;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models.API
{
    public class Address : EasyPostObject, IAddressParameter
    {
        #region JSON Properties

        [JsonProperty("carrier_facility")]
        public string? CarrierFacility { get; set; }
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
        [JsonProperty("federal_tax_id")]
        public string? FederalTaxId { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("phone")]
        public string? Phone { get; set; }
        [JsonProperty("residential")]
        public bool? Residential { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("state_tax_id")]
        public string? StateTaxId { get; set; }
        [JsonProperty("street1")]
        public string? Street1 { get; set; }
        [JsonProperty("street2")]
        public string? Street2 { get; set; }
        [JsonProperty("verifications")]
        public Verifications? Verifications { get; set; }
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        #endregion

        internal Address()
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Verify this address.
        /// </summary>
        /// <returns>EasyPost.Address instance. Check message for verification failures.</returns>
        [CrudOperations.Update]
        public async Task<Address> Verify()
        {
            if (Id == null)
            {
                throw new MissingPropertyError(this, nameof(Id));
            }

            await Update<Address>(Method.Get, $"addresses/{Id}/verify", null, "address");
            return this;
        }

        #endregion
    }

    public class AddressCollection : PaginatedCollection<Address>
    {
        #region JSON Properties

        [JsonProperty("addresses")]
        public List<Address>? Addresses { get; set; }

        #endregion

        internal AddressCollection()
        {
        }

        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Address> entries, int? pageSize = null)
        {
            string? lastId = entries.Last().Id;

            BetaFeatures.Parameters.Addresses.All parameters = new()
            {
                BeforeId = lastId,
            };

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
