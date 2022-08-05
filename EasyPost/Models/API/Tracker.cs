using System;
using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Tracker : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("carrier_detail")]
        public CarrierDetail carrier_detail { get; set; }

        [JsonProperty("est_delivery_date")]
        public DateTime? est_delivery_date { get; set; }

        [JsonProperty("mode")]
        public new string mode { get; set; }
        [JsonProperty("public_url")]
        public string public_url { get; set; }
        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
        [JsonProperty("signed_by")]
        public string signed_by { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }
        [JsonProperty("tracking_details")]
        public List<TrackingDetail> tracking_details { get; set; }
        [JsonProperty("tracking_updated_at")]
        public DateTime tracking_updated_at { get; set; }

        [JsonProperty("weight")]
        public double? weight { get; set; }

        #endregion
    }
}
