using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Rate : EasyPostObject
    {
        [JsonProperty("billing_type")]
        public string? BillingType { get; set; }
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }
        [JsonProperty("carrier_account_id")]
        public string? CarrierAccountId { get; set; }
        [JsonProperty("currency")]
        public string? Currency { get; set; }
        [JsonProperty("delivery_date")]
        public DateTime? DeliveryDate { get; set; }
        [JsonProperty("delivery_date_guaranteed")]
        public bool DeliveryDateGuaranteed { get; set; }
        [JsonProperty("delivery_days")]
        public int? DeliveryDays { get; set; }
        [JsonProperty("est_delivery_days")]
        public int? EstDeliveryDays { get; set; }
        [JsonProperty("list_currency")]
        public string? ListCurrency { get; set; }
        [JsonProperty("list_rate")]
        public string? ListPrice { get; set; }
        [JsonProperty("rate")]
        public string? Price { get; set; }
        [JsonProperty("retail_currency")]
        public string? RetailCurrency { get; set; }
        [JsonProperty("retail_rate")]
        public string? RetailPrice { get; set; }
        [JsonProperty("service")]
        public string? Service { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
    }
}
