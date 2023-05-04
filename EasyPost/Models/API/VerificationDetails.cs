using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class VerificationDetails : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("latitude")]
        public float? Latitude { get; internal set; }
        [JsonProperty("longitude")]
        public float? Longitude { get; internal set; }
        [JsonProperty("time_zone")]
        public string? TimeZone { get; internal set; }

        #endregion

        internal VerificationDetails()
        {
        }
    }
}
