using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class PaymentMethodSummary : EasyPostObject
    {
        [JsonProperty("primary_payment_method")]
        public CreditCard? PrimaryPaymentMethod { get; set; }
        [JsonProperty("secondary_payment_method")]
        public CreditCard? SecondaryPaymentMethod { get; set; }
    }
}
