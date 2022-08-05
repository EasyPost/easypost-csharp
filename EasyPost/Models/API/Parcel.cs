using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Parcel : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("height")]
        public double? height { get; set; }

        [JsonProperty("length")]
        public double? length { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("predefined_package")]
        public string predefined_package { get; set; }

        [JsonProperty("weight")]
        public double weight { get; set; }
        [JsonProperty("width")]
        public double? width { get; set; }

        #endregion
    }
}
