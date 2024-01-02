using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API.Beta
{
    /// <summary>
    ///     Class representing an EasyPost <a href="https://www.easypost.com/docs/api#retrieve-rates-for-a-shipment">stateless rate</a>.
    /// </summary>
    public class StatelessRate : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The billing type for the rate.
        /// </summary>
        [JsonProperty("billing_type")]
        public string? BillingType { get; set; }

        /// <summary>
        ///     The name of the carrier for the rate.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The ID of the <see cref="CarrierAccount"/> used to generate the rate.
        /// </summary>
        [JsonProperty("carrier_account_id")]
        public string? CarrierAccountId { get; set; }

        /// <summary>
        ///     The currency for the rate.
        /// </summary>
        [JsonProperty("currency")]
        public string? Currency { get; set; }

        /// <summary>
        ///     The delivery date for the rate.
        /// </summary>
        [JsonProperty("delivery_date")]
        public DateTime? DeliveryDate { get; set; }

        /// <summary>
        ///     Whether or not the <see cref="DeliveryDate"/> for the rate is guaranteed.
        /// </summary>
        [JsonProperty("delivery_date_guaranteed")]
        public bool? DeliveryDateGuaranteed { get; set; }

        /// <summary>
        ///     The number of days until delivery for the rate.
        /// </summary>
        [JsonProperty("delivery_days")]
        public int? DeliveryDays { get; set; }

        /// <summary>
        ///     The number of days until delivery for the rate.
        ///     This field is deprecated in favor of <see cref="DeliveryDays"/>.
        /// </summary>
        [JsonProperty("est_delivery_days")]
        public int? EstDeliveryDays { get; set; }

        /// <summary>
        ///     The current for the <see cref="ListRate"/> for the rate.
        /// </summary>
        [JsonProperty("list_currency")]
        public string? ListCurrency { get; set; }

        /// <summary>
        ///     The list rate for the rate.
        ///     The list rate is the non-negotiated rate given for having an account with the carrier.
        /// </summary>
        [JsonProperty("list_rate")]
        public string? ListRate { get; set; }

        /// <summary>
        ///     The actual quoted price for the rate.
        /// </summary>
        [JsonProperty("rate")]
        public string? Price { get; set; } // "Rate" is the enclosing class name
        // TODO: Rate is a double in SmartRate...

        /// <summary>
        ///     The currency for the <see cref="RetailRate"/> for the rate.
        /// </summary>
        [JsonProperty("retail_currency")]
        public string? RetailCurrency { get; set; }

        /// <summary>
        ///     The retail rate for the rate.
        ///     The retail rate is the in-store rate given with no account with the carrier.
        /// </summary>
        [JsonProperty("retail_rate")]
        public string? RetailRate { get; set; }

        /// <summary>
        ///     The <see cref="ServiceLevel"/> for the rate.
        /// </summary>
        [JsonProperty("service")]
        public string? Service { get; set; }

        /// <summary>
        ///     The ID of the <see cref="Shipment"/> associated with the rate.
        /// </summary>
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }

        #endregion
    }
}
