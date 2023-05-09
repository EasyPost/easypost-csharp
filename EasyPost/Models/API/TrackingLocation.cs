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

        [JsonProperty("city")]
        public string? City { get; set; }
        [JsonProperty("country")]
        public string? Country { get; set; }
        [JsonProperty("state")]
        public string? State { get; set; }
        [JsonProperty("zip")]
        public string? Zip { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="TrackingLocation"/> class.
        /// </summary>
        internal TrackingLocation()
        {
        }
    }
}
