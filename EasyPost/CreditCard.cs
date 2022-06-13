using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class CreditCard : Resource
    {
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("disabled_at")]
        public string disabled_at { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("last4")]
        public string last4 { get; set; }
        [JsonProperty("exp_month")]
        public string exp_month { get; set; }
        [JsonProperty("exp_year")]
        public string exp_year { get; set; }
        [JsonProperty("brand")]
        public string brand { get; set; }

        /// <summary>
        ///     Payment method priority
        /// </summary>
        public enum Priority
        {
            Primary,
            Secondary
        }

        /// <summary>
        ///     Get a payment method (credit card) by priority.
        /// </summary>
        /// <param name="priority">Which priority payment method to get.</param>
        /// <returns>An EasyPost.CreditCard instance.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the Priority provided is invalid.</exception>
        private static async Task<CreditCard> GetPaymentMethodByPriority(Priority priority)
        {
            PaymentMethod paymentMethods = await PaymentMethod.All();

            CreditCard? paymentMethod = null;
            switch (priority)
            {
                case Priority.Primary:
                    paymentMethod = paymentMethods.primary_payment_method;
                    break;
                case Priority.Secondary:
                    paymentMethod = paymentMethods.secondary_payment_method;
                    break;
                default:
                    break;
            }

            if (paymentMethod == null || !paymentMethod.id.StartsWith("card_"))
            {
                throw new Exception("The chosen payment method is not a credit card. Please try again.");
            }

            return paymentMethod;
        }

        /// <summary>
        ///     Fund a credit card.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="paymentMethodId">ID of payment method to fund.</param>
        /// <returns>An EasyPost.CreditCardFund instance.</returns>
        private static async Task<CreditCardFund> Fund(string amount, string paymentMethodId)
        {
            Request request = new Request($"credit_cards/{paymentMethodId}/charges", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "amount", amount
                }
            });

            return await request.Execute<CreditCardFund>();
        }

        /// <summary>
        ///     Fund a credit card.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="priority">Which type of payment method to fund.</param>
        /// <returns>An EasyPost.CreditCardFund instance.</returns>
        public static async Task<CreditCardFund> Fund(string amount, Priority priority)
        {
            CreditCard paymentMethod = await GetPaymentMethodByPriority(priority);

            return await Fund(amount, paymentMethod.id);
        }

        /// <summary>
        ///     Delete a credit card.
        /// </summary>
        /// <param name="paymentMethodId">ID of the payment method to delete.</param>
        /// <returns>Whether the request was successful or not.</returns>
        public static async Task<bool> Delete(string paymentMethodId)
        {
            Request request = new Request($"credit_cards/{paymentMethodId}", Method.Delete);

            return await request.Execute();
        }
    }
}
