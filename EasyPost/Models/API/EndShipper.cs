using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.EndShipper
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#endshipper-object">EasyPost EndShipper</a>.
    /// </summary>
    public class EndShipper : EasyPostObject, Parameters.IEndShipperParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The city the end shipper is located in.
        /// </summary>
        [JsonProperty("city")]
        public string? City { get; set; }

        /// <summary>
        ///    The name of the company.
        /// </summary>
        [JsonProperty("company")]
        public string? Company { get; set; }

        /// <summary>
        ///     The ISO 3166 code of the country the end shipper is located in.
        ///     Code must be "US" for a valid end shipper.
        /// </summary>
        [JsonProperty("country")]
        public string? Country { get; set; }

        /// <summary>
        ///     The email address of the person or organization.
        /// </summary>
        [JsonProperty("email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Potential error encountered while processing the end shipper.
        /// </summary>
        [JsonProperty("error")]
        public string? Error { get; set; }

        /// <summary>
        ///     A human-readable message for any errors that occurred during the end shipper's life cycle.
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
        ///     The state the end shipper is located in.
        /// </summary>
        [JsonProperty("state")]
        public string? State { get; set; }

        /// <summary>
        ///     The first line of the end shipper street address.
        /// </summary>
        [JsonProperty("street1")]
        public string? Street1 { get; set; }

        /// <summary>
        ///     The second line of the end shipper street address.
        /// </summary>
        [JsonProperty("street2")]
        public string? Street2 { get; set; }

        /// <summary>
        ///     The zip code the end shipper is located in.
        /// </summary>
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        #endregion

        
    }
#pragma warning disable CA1724 // Naming conflicts with Parameters.EndShipper

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="EndShipper"/>s.
    /// </summary>
    public class EndShipperCollection : PaginatedCollection<EndShipper>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="EndShipper"/>s in the collection.
        /// </summary>
        [JsonProperty("end_shippers")]
        public List<EndShipper>? EndShippers { get; set; }

        #endregion

        

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        // Cannot currently get the next page of EndShippers, so this is not implemented.
        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<EndShipper> entries, int? pageSize = null) => throw new EndOfPaginationError();
    }
}
