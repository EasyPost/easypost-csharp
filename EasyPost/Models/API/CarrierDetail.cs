using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost <see cref="Tracker"/>'s carrier details.
    /// </summary>
    public class CarrierDetail : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The alternate identifier for the package, as provided by the carrier.
        /// </summary>
        [JsonProperty("alternate_identifier")]
        public string? AlternateIdentifier { get; set; }

        /// <summary>
        ///     The type of container the associated <see cref="Shipment"/> was shipped in.
        /// </summary>
        [JsonProperty("container_type")]
        public string? ContainerType { get; set; }

        /// <summary>
        ///     The location to which the package is being sent, represented as a string for presentation purposes.
        /// </summary>
        [JsonProperty("destination_location")]
        public string? DestinationLocation { get; set; }

        /// <summary>
        ///     The location to which the package is being sent.
        /// </summary>
        [JsonProperty("destination_tracking_location")]
        public TrackingLocation? DestinationTrackingLocation { get; set; }

        /// <summary>
        ///     The estimated delivery date, as provided by the carrier, in the local time zone.
        /// </summary>
        [JsonProperty("est_delivery_date_local")]
        public string? EstDeliveryDateLocal { get; set; }

        /// <summary>
        ///     The estimated delivery time, as provided by the carrier, in the local time zone.
        /// </summary>
        [JsonProperty("est_delivery_time_local")]
        public string? EstDeliveryTimeLocal { get; set; }

        /// <summary>
        ///     The date and time the carrier guarantees the package to be delivered by.
        /// </summary>
        [JsonProperty("guaranteed_delivery_date")]
        public DateTime? GuaranteedDeliveryDate { get; set; }

        /// <summary>
        ///     The date and time of the first attempt by the carrier to deliver the package.
        /// </summary>
        [JsonProperty("initial_delivery_attempt")]
        public DateTime? InitialDeliveryAttempt { get; set; }

        /// <summary>
        ///     The location from which package originated, represented as a string for presentation purposes.
        /// </summary>
        [JsonProperty("origin_location")]
        public string? OriginLocation { get; set; }

        /// <summary>
        ///     The location from which package originated.
        /// </summary>
        [JsonProperty("origin_tracking_location")]
        public TrackingLocation? OriginTrackingLocation { get; set; }

        /// <summary>
        ///     The service level the associated <see cref="Shipment"/> was shipped with.
        /// </summary>
        [JsonProperty("service")]
        public string? Service { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="CarrierDetail"/> class.
        /// </summary>
        internal CarrierDetail()
        {
        }
    }
}
