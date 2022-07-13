using System;
using System.Collections.Generic;
using EasyPost.Utilities;
using Newtonsoft.Json;

namespace EasyPost
{
    public class SmartrateAccuracy : Enumeration
    {
        public static SmartrateAccuracy Percentile50 = new SmartrateAccuracy(1, "percentile_50");
        public static SmartrateAccuracy Percentile75 = new SmartrateAccuracy(2, "percentile_75");
        public static SmartrateAccuracy Percentile85 = new SmartrateAccuracy(2, "percentile_85");
        public static SmartrateAccuracy Percentile90 = new SmartrateAccuracy(3, "percentile_90");
        public static SmartrateAccuracy Percentile95 = new SmartrateAccuracy(4, "percentile_95");
        public static SmartrateAccuracy Percentile97 = new SmartrateAccuracy(2, "percentile_97");
        public static SmartrateAccuracy Percentile99 = new SmartrateAccuracy(5, "percentile_99");

        private SmartrateAccuracy(int id, string name)
            : base(id, name)
        {
        }

        public static IEnumerable<SmartrateAccuracy> All()
        {
            return GetAll<SmartrateAccuracy>();
        }
    }

    public class SmartrateResult : Resource
    {
        #region JSON Properties

        [JsonProperty("result")]
        public List<Smartrate> result { get; set; }

        #endregion
    }

    public class Smartrate : Resource
    {
        #region JSON Properties

        [JsonProperty("billing_type")]
        public string billing_type { get; set; }
        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("carrier_account_id")]
        public string carrier_account_id { get; set; }
        [JsonProperty("created_at")]
        public DateTime created_at { get; set; }
        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("delivery_date")]
        public DateTime? delivery_date { get; set; }
        [JsonProperty("delivery_date_guaranteed")]
        public bool delivery_date_guaranteed { get; set; }
        [JsonProperty("delivery_days")]
        public int? delivery_days { get; set; }
        [JsonProperty("est_delivery_days")]
        public int? est_delivery_days { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("list_currency")]
        public string list_currency { get; set; }
        [JsonProperty("list_rate")]
        public double list_rate { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("rate")]
        public double rate { get; set; }
        [JsonProperty("retail_currency")]
        public string retail_currency { get; set; }
        [JsonProperty("retail_rate")]
        public double retail_rate { get; set; }
        [JsonProperty("service")]
        public string service { get; set; }
        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
        [JsonProperty("time_in_transit")]
        public TimeInTransit time_in_transit { get; set; }
        [JsonProperty("updated_at")]
        public DateTime updated_at { get; set; }

        #endregion
    }
}
