using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class Fee : Resource
    {
        [JsonProperty("amount")]
        public double amount { get; set; }
        [JsonProperty("charged")]
        public bool charged { get; set; }
        [JsonProperty("refunded")]
        public bool refunded { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
    }
}
