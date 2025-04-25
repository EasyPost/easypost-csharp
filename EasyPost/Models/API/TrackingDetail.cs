using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/trackers#trackingdetail-object">EasyPost tracker detail object</a>.
    /// </summary>
    public class TrackingDetail : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The timestamp when the tracking scan event occurred.
        /// </summary>
        [JsonProperty("datetime")]
        public DateTime? Datetime { get; set; }

        /// <summary>
        ///     A human-readable summary message of the scan event.
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        ///     A human-readable description of the scan event.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     A code associated with the carrier.
        /// </summary>
        [JsonProperty("carrier_code")]
        public string? CarrierCode { get; set; }

        /// <summary>
        ///     The original source of the information for this scan event, usually the carrier.
        /// </summary>
        [JsonProperty("source")]
        public string? Source { get; set; }

        /// <summary>
        ///     The status of the package at the time of the scan event.
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
        ///     </list>
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     Additional details about the status of the package at the time of the scan event.
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
        ///     The <see cref="TrackingLocation"/> associated with the scan event.
        /// </summary>
        [JsonProperty("tracking_location")]
        public TrackingLocation? TrackingLocation { get; set; }

        /// <summary>
        ///     The estimated delivery date of the tracker.
        /// </summary>
        [JsonProperty("est_delivery_date")]
        public DateTime? EstDeliveryDate { get; set; }

        #endregion

    }
}
