using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#tracking-location-object">EasyPost tracking location</a>.
    /// </summary>
    public class TrackingLocation : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The city where the scan event occurred.
        /// </summary>
        [JsonProperty("city")]
        public string? City { get; set; }

        /// <summary>
        ///     The country where the scan event occurred.
        /// </summary>
        [JsonProperty("country")]
        public string? Country { get; set; }

        /// <summary>
        ///     The state where the scan event occurred.
        /// </summary>
        [JsonProperty("state")]
        public string? State { get; set; }

        /// <summary>
        ///     The postal code where the scan event occurred.
        /// </summary>
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        #endregion

    }
}
