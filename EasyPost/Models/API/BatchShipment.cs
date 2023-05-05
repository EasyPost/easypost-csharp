using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost batch shipment.
    /// </summary>
    public class BatchShipment : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     A human-readable message for any errors that occurred during the batch shipment's life cycle.
        /// </summary>
        [JsonProperty("batch_message")]
        public string? BatchMessage { get; set; }

        /// <summary>
        ///     The current state of the batch shipment.
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
        [JsonProperty("batch_status")]
        public string? BatchStatus { get; set; }

        /// <summary>
        ///     The tracking code associated with the batch shipment.
        /// </summary>
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="BatchShipment"/> class.
        /// </summary>
        internal BatchShipment()
        {
        }
    }
}
