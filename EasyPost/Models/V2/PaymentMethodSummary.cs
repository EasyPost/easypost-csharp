using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class PaymentMethodSummary : EasyPostObject
    {
        [JsonProperty("primary_payment_method")]
        public CreditCard? primary_payment_method { get; set; }
        [JsonProperty("secondary_payment_method")]
        public CreditCard? secondary_payment_method { get; set; }
    }
}
