using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using EasyPost.Parameters;
using EasyPost.Parameters.Address;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#address-object">EasyPost address</a>.
    /// </summary>
    public class Address : EasyPostObject, IAddressParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The specific designation for the address (only relevant if the address is a carrier facility).
        /// </summary>
        [JsonProperty("carrier_facility")]
        public string? CarrierFacility { get; set; }

        /// <summary>
        ///     The city the address is located in.
        /// </summary>
        [JsonProperty("city")]
        public string? City { get; set; }

        /// <summary>
        ///    The name of the company.
        /// </summary>
        [JsonProperty("company")]
        public string? Company { get; set; }

        /// <summary>
        ///     The ISO 3166 code of the country the address is located in.
        /// </summary>
        [JsonProperty("country")]
        public string? Country { get; set; }

        /// <summary>
        ///     The email address of the person or organization.
        /// </summary>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Potential error encountered while processing the address.
        /// </summary>
        [JsonProperty("error")]
        public string? Error { get; set; }

        /// <summary>
        ///     The federal tax ID of the person or organization.
        /// </summary>
        [JsonProperty("federal_tax_id")]
        public string? FederalTaxId { get; set; }

        /// <summary>
        ///     A human-readable message for any errors that occurred during the address's life cycle.
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        ///     The name of the person or organization.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     The phone number of the person or organization.
        /// </summary>
        [JsonProperty("phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     Whether the address is a residential address.
        /// </summary>
        [JsonProperty("residential")]
        public bool? Residential { get; set; }

        /// <summary>
        ///     The state the address is located in.
        /// </summary>
        [JsonProperty("state")]
        public string? State { get; set; }

        /// <summary>
        ///     The state tax ID of the person or organization.
        /// </summary>
        [JsonProperty("state_tax_id")]
        public string? StateTaxId { get; set; }

        /// <summary>
        ///     The first line of the street address.
        /// </summary>
        [JsonProperty("street1")]
        public string? Street1 { get; set; }

        /// <summary>
        ///     The second line of the street address.
        /// </summary>
        [JsonProperty("street2")]
        public string? Street2 { get; set; }

        /// <summary>
        ///     The result of any verifications.
        /// </summary>
        [JsonProperty("verifications")]
        public Verifications? Verifications { get; set; }

        /// <summary>
        ///     The zip code the address is located in.
        /// </summary>
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Address"/> class.
        /// </summary>
        internal Address()
        {
        }
    }

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Address"/>es.
    /// </summary>
    public class AddressCollection : PaginatedCollection<Address>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Address"/>es in the collection.
        /// </summary>
        [JsonProperty("addresses")]
        public List<Address>? Addresses { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="AddressCollection"/> class.
        /// </summary>
        internal AddressCollection()
        {
        }

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Address> entries, int? pageSize = null)
        {
            All parameters = Filters != null ? (All)Filters : new All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
