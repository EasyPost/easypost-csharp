using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class CreditCardFunding : Resource
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("amount")]
        public string amount { get; set; }
    }
}
