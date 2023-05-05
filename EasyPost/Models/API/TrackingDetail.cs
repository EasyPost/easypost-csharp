using System;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost tracker detail object.
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackingDetail"/> class.
        /// </summary>
        internal TrackingDetail()
        {
        }
    }
}
