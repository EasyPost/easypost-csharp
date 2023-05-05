using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost verification details object.
    /// </summary>
    public class VerificationDetails : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("latitude")]
        public float? Latitude { get; set; }
        [JsonProperty("longitude")]
        public float? Longitude { get; set; }
        [JsonProperty("time_zone")]
        public string? TimeZone { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="VerificationDetails"/> class.
        /// </summary>
        internal VerificationDetails()
        {
        }
    }
}
