using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#tracking-detail-object">EasyPost tracker detail object</a>.
    /// </summary>
    public class TrackingDetail : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("datetime")]
        public DateTime? Datetime { get; set; }

        /// <summary>
        ///     A human-readable message for any errors that occurred during the tracking detail's life cycle.
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("status_detail")]
        public string? StatusDetail { get; set; }
        [JsonProperty("tracking_location")]
        public TrackingLocation? TrackingLocation { get; set; }

        #endregion

        
    }
}
