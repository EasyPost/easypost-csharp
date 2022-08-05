using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Report : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("end_date")]
        public DateTime? end_date { get; set; }

        [JsonProperty("include_children")]
        public bool include_children { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("start_date")]
        public DateTime? start_date { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("url")]
        public string url { get; set; }
        [JsonProperty("url_expires_at")]
        public DateTime? url_expires_at { get; set; }

        #endregion
    }
}
