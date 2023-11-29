using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Batch
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#batch-object">EasyPost batch</a>.
    /// </summary>
    public class Batch : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     Potential error encountered while processing the batch.
        /// </summary>
        [JsonProperty("error")]
        public string? Error { get; set; }

        /// <summary>
        ///     The URL of the label image.
        /// </summary>
        [JsonProperty("label_url")]
        public string? LabelUrl { get; set; }

        /// <summary>
        ///     A human-readable message for any errors that occurred during the batch's life cycle.
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        ///     The number of shipments in the batch.
        /// </summary>
        [JsonProperty("num_shipments")]
        public int? NumShipments { get; set; }

        /// <summary>
        ///     An optional field that may be used in place of ID in some API endpoints.
        /// </summary>
        [JsonProperty("reference")]
        public string? Reference { get; set; }

        /// <summary>
        ///     The <see cref="ScanForm"/> associated with the batch.
        /// </summary>
        [JsonProperty("scan_form")]
        public ScanForm? ScanForm { get; set; }

        /// <summary>
        ///     The <see cref="BatchShipment"/>s associated with the batch.
        /// </summary>
        [JsonProperty("shipments")]
        public List<BatchShipment>? Shipments { get; set; }

        /// <summary>
        ///     The current state of the batch.
        ///     Possible values include: "creating", "creation_failed", "created", "purchasing", "purchase_failed", "purchased", "label_generating" and "label_generated".
        /// </summary>
        [JsonProperty("state")]
        public string? State { get; set; }

        /// <summary>
        ///     A dictionary of <see cref="BatchShipment"/> statuses and their counts.
        ///     Valid statuses are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"postage_purchased"</description>
        ///         </item>
        ///         <item>
        ///             <description>"postage_purchase_failed"</description>
        ///         </item>
        ///         <item>
        ///             <description>"queued_for_purchase"</description>
        ///         </item>
        ///         <item>
        ///             <description>"creation_failed"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("status")]
        public Dictionary<string, int>? Status { get; set; }

        #endregion
    }
#pragma warning disable CA1724 // Naming conflicts with Parameters.Batch

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Batch"/>es.
    /// </summary>
    public class BatchCollection : PaginatedCollection<Batch>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Batch"/>es in the collection.
        /// </summary>
        [JsonProperty("batches")]
        public List<Batch>? Batches { get; set; }

        #endregion

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Batch> entries, int? pageSize = null)
        {
            Parameters.Batch.All parameters = Filters != null ? (Parameters.Batch.All)Filters : new Parameters.Batch.All();

            // TODO: Batches get returned in reverse order from everything else (oldest first instead of newest first), so this needs to be "after_id" instead of "before_id"
            parameters.AfterId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
