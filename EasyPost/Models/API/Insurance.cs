using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Insurance
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/insurance#insurance-object">EasyPost insurance object</a>.
    /// </summary>
    public class Insurance : EasyPostObject, Parameters.IInsuranceParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The value of the insured goods, in US cents with sub-cent precision.
        /// </summary>
        [JsonProperty("amount")]
        public string? Amount { get; set; }

        /// <summary>
        ///     The <see cref="Address"/> from which the shipment is being sent.
        /// </summary>
        [JsonProperty("from_address")]
        public Address? FromAddress { get; set; }

        /// <summary>
        ///     A list of errors encountered during the attempted purchase of the insurance.
        /// </summary>
        [JsonProperty("messages")]
        public List<string>? Messages { get; set; }

        /// <summary>
        ///     The insurance provider used by EasyPost.
        /// </summary>
        [JsonProperty("provider")]
        public string? Provider { get; set; }

        /// <summary>
        ///     The identifying number for some insurance providers used by EasyPost.
        /// </summary>
        [JsonProperty("provider_id")]
        public string? ProviderId { get; set; }

        /// <summary>
        ///     An optional field that may be used in place of ID in some API endpoints.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     The ID of the <see cref="Shipment"/> being insured, if postage was purchased through EasyPost.
        /// </summary>
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }

        /// <summary>
        ///     The current status of the insurance.
        ///     Possible values include "new", "pending", "purchased", "failed" and "cancelled".
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     The <see cref="Address"/> to which the shipment is being sent.
        /// </summary>
        [JsonProperty("to_address")]
        public Address? ToAddress { get; set; }

        /// <summary>
        ///     The <see cref="Tracker"/> associated with this insurance.
        /// </summary>
        [JsonProperty("tracker")]
        public Tracker? Tracker { get; set; }

        /// <summary>
        ///     The tracking code of either the <see cref="Shipment"/> created using EasyPost, or provided by the user during creation of the insurance.
        /// </summary>
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        /// <summary>
        ///     The <see cref="Models.API.Fee"/> for the insurance.
        /// </summary>
        [JsonProperty("fee")]
        public Fee? Fee { get; set; }
        #endregion

    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.Insurance

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Insurance"/> objects.
    /// </summary>
    public class InsuranceCollection : PaginatedCollection<Insurance>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Insurance"/>s in the collection.
        /// </summary>
        [JsonProperty("insurances")]
        public List<Insurance>? Insurances { get; set; }

        #endregion

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Insurance> entries, int? pageSize = null)
        {
            Parameters.Insurance.All parameters = Filters != null ? (Parameters.Insurance.All)Filters : new Parameters.Insurance.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
