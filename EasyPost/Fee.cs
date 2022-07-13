using Newtonsoft.Json;

namespace EasyPost
{
    public class Fee : Resource
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
