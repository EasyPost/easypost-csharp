using System;
using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.Tracker
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#tracker-object">EasyPost tracker</a>.
    /// </summary>
    public class Tracker : EasyPostObject, Parameters.ITrackerParameter
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string? Carrier { get; set; }
        [JsonProperty("carrier_detail")]
        public CarrierDetail? CarrierDetail { get; set; }
        [JsonProperty("est_delivery_date")]
        public DateTime? EstDeliveryDate { get; set; }
        [JsonProperty("public_url")]
        public string? PublicUrl { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
        [JsonProperty("signed_by")]
        public string? SignedBy { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("status_detail")]
        public string? StatusDetail { get; set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }
        [JsonProperty("tracking_details")]
        public List<TrackingDetail>? TrackingDetails { get; set; }
        [JsonProperty("tracking_updated_at")]
        public DateTime? TrackingUpdatedAt { get; set; }
        [JsonProperty("weight")]
        public double? Weight { get; set; }

        #endregion

    }
#pragma warning disable CA1724 // Naming conflicts with Parameters.Tracker

    /// <summary>
    ///     Class representing a collection of EasyPost <see cref="Tracker"/>s.
    /// </summary>
    public class TrackerCollection : PaginatedCollection<Tracker>
    {
        #region JSON Properties

        /// <summary>
        ///     The <see cref="Tracker"/>s in the collection.
        /// </summary>
        [JsonProperty("trackers")]
        public List<Tracker>? Trackers { get; set; }

        #endregion

        /// <summary>
        ///     Construct the parameter set for retrieving the next page of this paginated collection.
        /// </summary>
        /// <param name="entries">The entries on the current page of this paginated collection.</param>
        /// <param name="pageSize">The request size of the next page.</param>
        /// <typeparam name="TParameters">The type of parameters to construct.</typeparam>
        /// <returns>A TParameters-type parameters set.</returns>
        public override TParameters BuildNextPageParameters<TParameters>(IEnumerable<Tracker> entries, int? pageSize = null)
        {
            Parameters.Tracker.All parameters = Filters != null ? (Parameters.Tracker.All)Filters : new Parameters.Tracker.All();

            parameters.BeforeId = entries.Last().Id;

            if (pageSize != null)
            {
                parameters.PageSize = pageSize;
            }

            return (parameters as TParameters)!;
        }
    }
}
