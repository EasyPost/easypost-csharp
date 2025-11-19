using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EmbeddablesSession.
    /// </summary>
    public class EmbeddablesSession : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     Always returns "EmbeddablesSession".
        /// </summary>
        [JsonProperty("object")]
        public string? Object { get; set; }

        /// <summary>
        ///     Short-lived, one-time-use token that authorizes an Embeddables Components session.
        ///     Must be provided to the client-side Embeddables script to initialize the component.
        /// </summary>
        [JsonProperty("session_id")]
        public string? SessionId { get; set; }

        /// <summary>
        ///     ISO 8601 timestamp indicating when the session was created.
        /// </summary>
        [JsonProperty("created_at")]
        public string? CreatedAt { get; set; }

        /// <summary>
        ///     ISO 8601 timestamp indicating when the session expires.
        /// </summary>
        [JsonProperty("expires_at")]
        public string? ExpiresAt { get; set; }

        #endregion
    }
}
