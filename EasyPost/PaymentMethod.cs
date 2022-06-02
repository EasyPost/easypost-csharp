using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost
{
    public class PaymentMethod : Resource
    {
        /// <summary>
        ///     Payment method priority
        /// </summary>
        public enum Priority
        {
            Primary,
            Secondary
        }

        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("object")]
        public string Object { get; set; }
        [JsonProperty("primary_payment_method")]
        public CreditCard primary_payment_method { get; set; }
        [JsonProperty("secondary_payment_method")]
        public CreditCard secondary_payment_method { get; set; }

        /// <summary>
        ///     Get a payment method (credit card) by priority.
        /// </summary>
        /// <param name="priority">Which priority payment method to get.</param>
        /// <returns>An EasyPost.CreditCard instance.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the Priority provided is invalid.</exception>
        private static async Task<CreditCard> GetPaymentMethodByPriority(Priority priority)
        {
            PaymentMethod paymentMethods = await All();

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

            if (paymentMethod == null)
            {
                throw new Exception("The chosen payment method is not a valid method. Please try again.");
            }

            return paymentMethod;
        }

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

        /// <summary>
        ///     Delete this credit card.
        /// </summary>
        /// <param name="paymentMethodId">ID of the payment method to delete.</param>
        /// <returns>Whether the request was successful or not.</returns>
        public static async Task<bool> Delete(string paymentMethodId)
        {
            Request request = new Request($"credit_cards/{paymentMethodId}", Method.Delete);

            return await request.Execute();
        }

        /// <summary>
        ///     Delete a credit card.
        /// </summary>
        /// <param name="priority">Which payment method to delete.</param>
        /// <returns>Whether the request was successful or not.</returns>
        public static async Task<bool> Delete(Priority priority)
        {
            CreditCard paymentMethod = await GetPaymentMethodByPriority(priority);

            return await Delete(paymentMethod.id);
        }

        /// <summary>
        ///     Fund a credit card.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="paymentMethodId">ID of payment method to fund.</param>
        /// <returns>An EasyPost.CreditCardFund instance.</returns>
        public static async Task<CreditCardFund> Fund(string amount, string paymentMethodId)
        {
            Request request = new Request($"credit_cards/{paymentMethodId}/charge", Method.Post);
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
    }
}
