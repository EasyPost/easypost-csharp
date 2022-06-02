using Newtonsoft.Json;

namespace EasyPost
{
    public class CreditCardFund : Resource
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("amount")]
        public string amount { get; set; }
    }
}
