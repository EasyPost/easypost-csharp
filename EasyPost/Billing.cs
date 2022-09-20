using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;

namespace EasyPost
{
    public static class Billing
    {
        /// <summary>
        ///     Delete a payment method.
        /// </summary>
        /// <param name="priority">Which type of payment method to delete.</param>
        /// <returns>Whether the request was successful or not.</returns>
        public static async Task<bool> DeletePaymentMethod(PaymentMethod.Priority priority)
        {
            PaymentMethodObject paymentMethod = await GetPaymentMethodByPriority(priority);

            Request request = new Request($"{paymentMethod.Endpoint}/{paymentMethod.id}", Method.Delete);

            return await request.Execute();
        }

        /// <summary>
        ///     Fund your wallet from a specific payment method.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="priority">Which type of payment method to use to fund the wallet. Defaults to primary.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static async Task<bool> FundWallet(string amount, PaymentMethod.Priority? priority = null)
        {
            PaymentMethodObject paymentMethod = await GetPaymentMethodByPriority(priority ?? PaymentMethod.Priority.Primary);

            Request request = new Request($"{paymentMethod.Endpoint}/{paymentMethod.id}/charges", Method.Post);
            request.AddParameters(new Dictionary<string, object>
            {
                {
                    "amount", amount
                }
            });

            return await request.Execute();
        }

        /// <summary>
        ///     List all payment methods for this account.
        /// </summary>
        /// <returns>An EasyPost.PaymentMethod summary object.</returns>
        /// <exception cref="Exception">If billing has not been set up yet.</exception>
        public static async Task<PaymentMethod> RetrievePaymentMethods()
        {
            Request request = new Request("payment_methods", Method.Get);

            PaymentMethod paymentMethod = await request.Execute<PaymentMethod>();

            if (paymentMethod.id == null)
            {
                throw new Exception("Billing has not been set up for this user. Please add a payment method.");
            }

            return paymentMethod;
        }

        /// <summary>
        ///     Get a payment method by priority.
        /// </summary>
        /// <param name="primaryOrSecondary">Which priority payment method to get.</param>
        /// <returns>An EasyPost.PaymentMethodObject instance.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the Priority provided is invalid.</exception>
        private static async Task<PaymentMethodObject> GetPaymentMethodByPriority(PaymentMethod.Priority primaryOrSecondary)
        {
            PaymentMethod paymentMethods = await RetrievePaymentMethods();

            PaymentMethodObject? paymentMethod = null;
            if (primaryOrSecondary.Equals(PaymentMethod.Priority.Primary))
            {
                paymentMethod = paymentMethods.primary_payment_method;
            }
            else if (primaryOrSecondary.Equals(PaymentMethod.Priority.Secondary))
            {
                paymentMethod = paymentMethods.secondary_payment_method;
            }

            if (paymentMethod?.id == null)
            {
                throw new Exception("The chosen payment method is not set up yet.");
            }

            return paymentMethod;
        }
    }
}
