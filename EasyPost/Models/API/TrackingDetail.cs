using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TrackingDetail : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("datetime")]
        public DateTime? Datetime { get; internal set; }
        [JsonProperty("message")]
        public string? Message { get; internal set; }
        [JsonProperty("status")]
        public string? Status { get; internal set; }
        [JsonProperty("status_detail")]
        public string? StatusDetail { get; internal set; }
        [JsonProperty("tracking_location")]
        public TrackingLocation? TrackingLocation { get; internal set; }

        #endregion

        internal TrackingDetail()
        {
        }
    }
}
