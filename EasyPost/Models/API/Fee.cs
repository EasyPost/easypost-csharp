using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Fee : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("amount")]
        public double amount { get; set; }
        [JsonProperty("charged")]
        public bool charged { get; set; }
        [JsonProperty("refunded")]
        public bool refunded { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }

        #endregion
    }
}
