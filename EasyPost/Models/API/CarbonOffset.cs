using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarbonOffset
    {
        #region JSON Properties

        [JsonProperty("currency")]
        public string currency { get; set; }
        [JsonProperty("grams")]
        public int grams { get; set; }
        [JsonProperty("object")]
        public string @object { get; set; }
        [JsonProperty("price")]
        public string price { get; set; }

        #endregion
    }
}
