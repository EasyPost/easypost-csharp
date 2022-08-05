using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TrackingLocation
    {
        #region JSON Properties

        [JsonProperty("city")]
        public string city { get; set; }
        [JsonProperty("country")]
        public string country { get; set; }

        [JsonProperty("state")]
        public string state { get; set; }

        [JsonProperty("zip")]
        public string zip { get; set; }

        #endregion
    }
}
