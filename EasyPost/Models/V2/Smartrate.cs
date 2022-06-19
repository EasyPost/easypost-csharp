using System;
using System.Collections.Generic;
using EasyPost.Interfaces;
using EasyPost.Utilities;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class SmartrateAccuracy : ValueEnum
    {
        public static readonly SmartrateAccuracy Percentile50 = new SmartrateAccuracy(1, "percentile_50");
        public static readonly SmartrateAccuracy Percentile75 = new SmartrateAccuracy(2, "percentile_75");
        public static readonly SmartrateAccuracy Percentile85 = new SmartrateAccuracy(2, "percentile_85");
        public static readonly SmartrateAccuracy Percentile90 = new SmartrateAccuracy(3, "percentile_90");
        public static readonly SmartrateAccuracy Percentile95 = new SmartrateAccuracy(4, "percentile_95");
        public static readonly SmartrateAccuracy Percentile97 = new SmartrateAccuracy(2, "percentile_97");
        public static readonly SmartrateAccuracy Percentile99 = new SmartrateAccuracy(5, "percentile_99");

        private SmartrateAccuracy(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<SmartrateAccuracy> All()
        {
            return GetAll<SmartrateAccuracy>();
        }
    }

    public class SmartrateResult : EasyPostObject
    {
        [JsonProperty("result")]
        public List<Smartrate>? Result { get; set; }
    }

    public class Smartrate : EasyPostObject
    {
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
        public double ListRate { get; set; }
        [JsonProperty("rate")]
        public double Rate { get; set; }
        [JsonProperty("retail_currency")]
        public string? RetailCurrency { get; set; }
        [JsonProperty("retail_rate")]
        public double RetailRate { get; set; }
        [JsonProperty("service")]
        public string? Service { get; set; }
        [JsonProperty("shipment_id")]
        public string? ShipmentId { get; set; }
        [JsonProperty("time_in_transit")]
        public TimeInTransit? TimeInTransit { get; set; }
    }
}
