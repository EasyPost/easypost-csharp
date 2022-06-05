using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class CreditCardFunding : EasyPostObject
    {
        [JsonProperty("amount")]
        public string? amount { get; set; }
    }
}
