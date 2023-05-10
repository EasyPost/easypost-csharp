using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using EasyPost.Parameters;
using EasyPost.Parameters.Refund;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Refund
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#refund-object">EasyPost refund</a>.
    /// </summary>
    public class Refund : EasyPostObject, IRefundParameter
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }
        [JsonProperty("confirmation_number")]
        public string? ConfirmationNumber { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Refund"/> class.
        /// </summary>
        internal Refund()
        {
        }
    }
#pragma warning disable CA1724 // Naming conflicts with Parameters.Refund

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Refund"/>s.
    /// </summary>
    public class RefundCollection : PaginatedCollection<Refund>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Refund"/>s in the collection.
        /// </summary>
        [JsonProperty("refunds")]
        public List<Refund>? Refunds { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="RefundCollection"/> class.
        /// </summary>
        internal RefundCollection()
        {
        }

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        protected internal override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Refund> entries, int? pageSize = null)
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
