using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class PaymentMethod : Resource
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("primary_payment_method")]
        public CreditCard primary_payment_method { get; set; }
        [JsonProperty("secondary_payment_method")]
        public CreditCard secondary_payment_method { get; set; }

        /// <summary>
        ///     List all payment methods for this account.
        /// </summary>
        /// <returns>An EasyPost.PaymentMethod summary object.</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<PaymentMethod> All()
        {
            Request request = new Request("payment_methods", Method.Get);

            PaymentMethod paymentMethod = await request.Execute<PaymentMethod>();

            if (paymentMethod.id == null)
            {
                throw new Exception("Billing has not been setup for this user. Please add a payment method.");
            }

            return paymentMethod;
        }
    }
}
