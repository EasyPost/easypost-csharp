using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Report : EasyPostObject
    {
        [JsonProperty("end_date")]
        public DateTime? end_date { get; set; }
        [JsonProperty("include_children")]
        public bool include_children { get; set; }
        [JsonProperty("start_date")]
        public DateTime? start_date { get; set; }
        [JsonProperty("status")]
        public string? status { get; set; }
        [JsonProperty("url")]
        public string? url { get; set; }
        [JsonProperty("url_expires_at")]
        public DateTime? url_expires_at { get; set; }
    }
}
