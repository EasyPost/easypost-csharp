using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TrackingDetail : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("datetime")]
        public DateTime? Datetime { get; set; }
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("status_detail")]
        public string? StatusDetail { get; set; }
        [JsonProperty("tracking_location")]
        public TrackingLocation? TrackingLocation { get; set; }

        #endregion

        internal TrackingDetail()
        {
        }
    }
}
