using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarbonOffset
    {
        #region JSON Properties

        [JsonProperty("currency")]
        public string? Currency { get; set; }
        [JsonProperty("grams")]
        public int? Grams { get; set; }
        [JsonProperty("object")]
        public string? Object { get; set; }
        [JsonProperty("price")]
        public string? Price { get; set; }

        #endregion

        internal CarbonOffset()
        {
        }
    }
}
