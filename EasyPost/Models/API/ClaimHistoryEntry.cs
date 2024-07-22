using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an entry in a <see cref="Models.API.Claim.History"/> property.
    /// </summary>
    public class ClaimHistoryEntry : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The status of the claim at the time of this history entry.
        /// </summary>
        [JsonProperty("status")]
        public string? Status { get; set; }

        /// <summary>
        ///     The reason for the status of the claim at the time of this history entry.
        /// </summary>
        [JsonProperty("status_details")]
        public string? StatusDetails { get; set; }

        /// <summary>
        ///     The date and time of the history entry.
        /// </summary>
        [JsonProperty("status_timestamp")]
        public string? StatusTimestamp { get; set; }

        #endregion
    }
}
