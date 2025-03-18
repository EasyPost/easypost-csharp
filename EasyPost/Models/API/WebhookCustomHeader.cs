using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class for webhook custom header.
    /// </summary>
    public class WebhookCustomHeader
    {
        #region JSON Properties

        /// <summary>
        ///     Custom Header name.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Custom Header value.
        /// </summary>
        [JsonProperty("value")]
        public string? Value { get; set; }
        #endregion
    }
}
