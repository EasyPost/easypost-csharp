using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Refund
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#refund-object">EasyPost refund</a>.
    /// </summary>
    public class Refund : EasyPostObject, Parameters.IRefundParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The carrier the refund request was submitted to.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The confirmation number for the refund request submitted to the carrier.
        /// </summary>
        [JsonProperty("confirmation_number")]
        public string? ConfirmationNumber { get; set; }

        /// <summary>
        ///     The ID of the <see cref="EasyPost.Models.API.Shipment"/> associated with this refund.
        /// </summary>
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }

        /// <summary>
        ///     The status of the refund request.
        ///     Possible values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"submitted"</description>
        ///         </item>
        ///         <item>
        ///             <description>"refunded"</description>
        ///         </item>
        ///         <item>
        ///             <description>"rejected"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     The tracking code of the <see cref="EasyPost.Models.API.Shipment"/> associated with this refund.
        /// </summary>
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion

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
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Refund> entries, int? pageSize = null)
        {
            Parameters.Refund.All parameters = Filters != null ? (Parameters.Refund.All)Filters : new Parameters.Refund.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
