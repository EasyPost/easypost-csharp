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
    ///     Class representing an <a href="https://docs.easypost.com/docs/trackers#tracker-object">EasyPost tracker</a>.
    /// </summary>
    public class Tracker : EasyPostObject, Parameters.ITrackerParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The name of the carrier handling the package.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     Additional details provided by the carrier.
        /// </summary>
        [JsonProperty("carrier_detail")]
        public CarrierDetail? CarrierDetail { get; set; }

        /// <summary>
        ///     The estimated delivery date provided by the carrier.
        /// </summary>
        [JsonProperty("est_delivery_date")]
        public DateTime? EstDeliveryDate { get; set; }

        /// <summary>
        ///     A list of <see cref="Fee"/>s associated with the package.
        /// </summary>
        [JsonProperty("fees")]
        public List<Fee>? Fees { get; set; }

        /// <summary>
        ///     The URL of the publicly-accessible webpage with tracking details for the package.
        /// </summary>
        [JsonProperty("public_url")]
        public string? PublicUrl { get; set; }

        /// <summary>
        ///     The ID of the <see cref="Shipment"/> associated with this tracker.
        /// </summary>
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }

        /// <summary>
        ///     The name of the person who signed for the package.
        /// </summary>
        [JsonProperty("signed_by")]
        public string? SignedBy { get; set; }

        /// <summary>
        ///     The current status of the package.
        ///     Possible values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"pre_transit"</description>
        ///         </item>
        ///         <item>
        ///             <description>"in_transit"</description>
        ///         </item>
        ///         <item>
        ///             <description>"out_for_delivery"</description>
        ///         </item>
        ///         <item>
        ///             <description>"delivered"</description>
        ///         </item>
        ///         <item>
        ///             <description>"available_for_pickup"</description>
        ///         </item>
        ///         <item>
        ///             <description>"return_to_sender"</description>
        ///         </item>
        ///         <item>
        ///             <description>"failure"</description>
        ///         </item>
        ///         <item>
        ///             <description>"cancelled"</description>
        ///         </item>
        ///         <item>
        ///             <description>"unknown"</description>
        ///         </item>
        ///         <item>
        ///             <description>"error"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Additional details about the current status of the package.
        ///     Possible values are:
        ///     <list type="bullet">
        ///         <item>
        ///             <description>"address_correction"</description>
        ///         </item>
        ///         <item>
        ///             <description>"arrived_at_destination"</description>
        ///         </item>
        ///         <item>
        ///             <description>"arrived_at_facility"</description>
        ///         </item>
        ///         <item>
        ///             <description>"arrived_at_pickup_location"</description>
        ///         </item>
        ///         <item>
        ///             <description>"awaiting_information"</description>
        ///         </item>
        ///         <item>
        ///             <description>"cancelled"</description>
        ///         </item>
        ///         <item>
        ///             <description>"damaged"</description>
        ///         </item>
        ///         <item>
        ///             <description>"delayed"</description>
        ///         </item>
        ///         <item>
        ///             <description>"delivery_exception"</description>
        ///         </item>
        ///         <item>
        ///             <description>"departed_facility"</description>
        ///         </item>
        ///         <item>
        ///             <description>"departed_origin_facility"</description>
        ///         </item>
        ///         <item>
        ///             <description>"expired"</description>
        ///         </item>
        ///         <item>
        ///             <description>"failure"</description>
        ///         </item>
        ///         <item>
        ///             <description>"held"</description>
        ///         </item>
        ///         <item>
        ///             <description>"in_transit"</description>
        ///         </item>
        ///         <item>
        ///             <description>"label_created"</description>
        ///         </item>
        ///         <item>
        ///             <description>"lost"</description>
        ///         </item>
        ///         <item>
        ///             <description>"missorted"</description>
        ///         </item>
        ///         <item>
        ///             <description>"out_for_delivery"</description>
        ///         </item>
        ///         <item>
        ///             <description>"received_at_destination_facility"</description>
        ///         </item>
        ///         <item>
        ///             <description>"received_at_origin_facility"</description>
        ///         </item>
        ///         <item>
        ///             <description>"refused"</description>
        ///         </item>
        ///         <item>
        ///             <description>"return"</description>
        ///         </item>
        ///         <item>
        ///             <description>"status_update"</description>
        ///         </item>
        ///         <item>
        ///             <description>"transferred_to_destination_carrier"</description>
        ///         </item>
        ///         <item>
        ///             <description>"transit_exception"</description>
        ///         </item>
        ///         <item>
        ///             <description>"unknown"</description>
        ///         </item>
        ///         <item>
        ///             <description>"weather_delay"</description>
        ///         </item>
        ///     </list>
        /// </summary>
        [JsonProperty("status_detail")]
        public string? StatusDetail { get; set; }

        /// <summary>
        ///     The tracking code provided by the carrier.
        /// </summary>
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; set; }

        /// <summary>
        ///     A list of every scan event recorded for the package.
        /// </summary>
        [JsonProperty("tracking_details")]
        public List<TrackingDetail>? TrackingDetails { get; set; }

        /// <summary>
        ///     The weight of the package as measured by the carrier, in ounces.
        /// </summary>
        [JsonProperty("weight")]
        public double? Weight { get; set; }

        /// <summary>
        ///     Is the tracker in a finalized state.
        /// </summary>
        [JsonProperty("finalized")]
        public bool? Finalized { get; set; }

        /// <summary>
        ///     Is the tracker for a return shipment.
        /// </summary>
        [JsonProperty("is_return")]
        public bool? IsReturn { get; set; }

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
