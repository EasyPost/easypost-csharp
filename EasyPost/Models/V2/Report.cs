using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Report : EasyPostObject
    {
        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonProperty("include_children")]
        public bool IncludeChildren { get; set; }
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("url_expires_at")]
        public DateTime? UrlExpiresAt { get; set; }
    }
}
