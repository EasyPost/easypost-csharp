using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class Report : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("end_date")]
        public DateTime? end_date { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("include_children")]
        public bool include_children { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("start_date")]
        public DateTime? start_date { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("url")]
        public string url { get; set; }
        [JsonProperty("url_expires_at")]
        public DateTime? url_expires_at { get; set; }
    }
}
