using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/smartrate">EasyPost SmartRate</a>.
    /// </summary>
    public class SmartRate : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The billing type for this rate.
        /// </summary>
        [JsonProperty("billing_type")]
        public string? BillingType { get; set; }

        /// <summary>
        ///     The name of the carrier for this rate.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The ID of the <see cref="EasyPost.Models.API.CarrierAccount"/> used to generate this rate.
        /// </summary>
        [JsonProperty("carrier_account_id")]
        public string? CarrierAccountId { get; set; }

        /// <summary>
        ///     The currency for the <see cref="Price"/> of this rate.
        /// </summary>
        [JsonProperty("currency")]
        public string? Currency { get; set; }

        /// <summary>
        ///     The delivery date for this rate.
        /// </summary>
        [JsonProperty("delivery_date")]
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        ///     Whether the delivery window is guaranteed for this rate.
        /// </summary>
        [JsonProperty("delivery_date_guaranteed")]
        public bool? DeliveryDateGuaranteed { get; set; }

        /// <summary>
        ///     The number of days until delivery for this rate.
        /// </summary>
        [JsonProperty("delivery_days")]
        public int? DeliveryDays { get; set; }

        /// <summary>
        ///     The number of days until delivery for this rate.
        /// </summary>
        [Obsolete("Use DeliveryDays instead.")]
        [JsonProperty("est_delivery_days")]
        public int? EstDeliveryDays { get; set; }

        /// <summary>
        ///     The currency for the <see cref="ListRate"/>.
        /// </summary>
        [JsonProperty("list_currency")]
        public string? ListCurrency { get; set; }

        /// <summary>
        ///     The non-negotiated rate given for having an account with the carrier.
        /// </summary>
        [JsonProperty("list_rate")]
        public string? ListRate { get; set; }

        /// <summary>
        ///     The actual monetary amount charged by the carrier.
        /// </summary>
        [JsonProperty("rate")]
        public double? Rate { get; set; }
        // TODO: Rate/Price is a string in normal Rate and StatelessRate...

        /// <summary>
        ///     The actual monetary amount charged by the carrier.
        ///     Alias for <see cref="Rate"/>.
        /// </summary>
        [JsonIgnore]
        public double? Price => Rate;

        /// <summary>
        ///     The currency for the <see cref="RetailRate"/>.
        /// </summary>
        [JsonProperty("retail_currency")]
        public string? RetailCurrency { get; set; }

        /// <summary>
        ///     The in-store rate given with no account.
        /// </summary>
        [JsonProperty("retail_rate")]
        public string? RetailRate { get; set; }

        /// <summary>
        ///     The service level for the <see cref="Carrier"/> of this rate.
        /// </summary>
        [JsonProperty("service")]
        public string? Service { get; set; }

        /// <summary>
        ///     The ID of the <see cref="EasyPost.Models.API.Shipment"/> associated with this rate.
        /// </summary>
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }

        /// <summary>
        ///     Details about estimated time in transit for this rate.
        /// </summary>
        [JsonProperty("time_in_transit")]
        public TimeInTransit? TimeInTransit { get; set; }

        #endregion

    }
}
