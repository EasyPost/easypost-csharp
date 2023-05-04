﻿using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class SmartRate : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("billing_type")]
        public string? BillingType { get; internal set; }
        [JsonProperty("carrier")]
        public string? Carrier { get; internal set; }
        [JsonProperty("carrier_account_id")]
        public string? CarrierAccountId { get; internal set; }
        [JsonProperty("currency")]
        public string? Currency { get; internal set; }
        [JsonProperty("delivery_date")]
        public DateTime? DeliveryDate { get; internal set; }
        [JsonProperty("delivery_date_guaranteed")]
        public bool? DeliveryDateGuaranteed { get; internal set; }
        [JsonProperty("delivery_days")]
        public int? DeliveryDays { get; internal set; }
        [JsonProperty("est_delivery_days")]
        public int? EstDeliveryDays { get; internal set; }
        [JsonProperty("list_currency")]
        public string? ListCurrency { get; internal set; }
        [JsonProperty("list_rate")]
        public double? ListRate { get; internal set; }
        [JsonProperty("rate")]
        public double? Rate { get; internal set; }
        [JsonProperty("retail_currency")]
        public string? RetailCurrency { get; internal set; }
        [JsonProperty("retail_rate")]
        public double? RetailRate { get; internal set; }
        [JsonProperty("service")]
        public string? Service { get; internal set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; internal set; }
        [JsonProperty("time_in_transit")]
        public TimeInTransit? TimeInTransit { get; internal set; }

        #endregion

        internal SmartRate()
        {
        }
    }
}
