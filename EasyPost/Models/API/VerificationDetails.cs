using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class VerificationDetails : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("latitude")]
        public float latitude { get; set; }
        [JsonProperty("longitude")]
        public float longitude { get; set; }
        [JsonProperty("time_zone")]
        public string time_zone { get; set; }

        #endregion
    }
}
