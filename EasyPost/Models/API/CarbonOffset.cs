using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CarbonOffset : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("currency")]
        public string? Currency { get; internal set; }
        [JsonProperty("grams")]
        public int? Grams { get; internal set; }
        [JsonProperty("price")]
        public string? Price { get; internal set; }

        #endregion

        internal CarbonOffset()
        {
        }
    }
}
