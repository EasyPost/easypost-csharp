using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Report : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("end_date")]
        public DateTime? EndDate { get; set; }
        [JsonProperty("include_children")]
        public bool? IncludeChildren { get; set; }
        [JsonProperty("start_date")]
        public DateTime? StartDate { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("url")]
        public string? Url { get; set; }
        [JsonProperty("url_expires_at")]
        public DateTime? UrlExpiresAt { get; set; }

        #endregion

        internal Report()
        {
        }
    }
}
