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

        [JsonProperty("latitude")]
        public float? Latitude { get; set; }

        [JsonProperty("longitude")]
        public float? Longitude { get; set; }

        [JsonProperty("time_zone")]
        public string? TimeZone { get; set; }

        #endregion

    }
}
