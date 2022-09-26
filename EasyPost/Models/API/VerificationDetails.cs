using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
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

        internal VerificationDetails()
        {
        }
    }
}
