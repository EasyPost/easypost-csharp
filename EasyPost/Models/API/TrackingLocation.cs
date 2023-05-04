using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TrackingLocation : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("city")]
        public string? City { get; internal set; }
        [JsonProperty("country")]
        public string? Country { get; internal set; }
        [JsonProperty("state")]
        public string? State { get; internal set; }
        [JsonProperty("zip")]
        public string? Zip { get; internal set; }

        #endregion

        internal TrackingLocation()
        {
        }
    }
}
