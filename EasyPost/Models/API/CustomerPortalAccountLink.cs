using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing a CustomerPortalAccountLink.
    /// </summary>
    public class CustomerPortalAccountLink : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     One-time-use session URL for initiating the Customer Portal.
        /// </summary>
        [JsonProperty("link")]
        public string? Link { get; set; }

        /// <summary>
        ///     One-time-use session URL for initiating the Customer Portal.
        /// </summary>
        [JsonProperty("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        ///     ISO 8601 timestamp when the link will expire (5 minutes from creation).
        /// </summary>
        [JsonProperty("expires_at")]
        public string? ExpiresAt { get; set; }

        #endregion
    }
}
