using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#verification-details-object">EasyPost verification details object</a>.
    /// </summary>
    public class VerificationDetails : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The latitude of the address.
        /// </summary>
        [JsonProperty("latitude")]
        public float? Latitude { get; set; }

        /// <summary>
        ///     The longitude of the address.
        /// </summary>
        [JsonProperty("longitude")]
        public float? Longitude { get; set; }

        /// <summary>
        ///     The time zone of the address (e.g. "America/Los_Angeles").
        /// </summary>
        [JsonProperty("time_zone")]
        public string? TimeZone { get; set; }

        #endregion

    }
}
