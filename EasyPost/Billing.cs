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

            return await DeletePaymentMethod(paymentMethod);
        }

        /// <summary>
        ///     Delete a payment method.
        /// </summary>
        /// <param name="paymentMethodObject">Payment method to delete.</param>
        /// <returns>Whether the request was successful or not.</returns>
        public static async Task<bool> DeletePaymentMethod(PaymentMethodObject paymentMethodObject)
        {
            Request request = new Request($"{paymentMethodObject.Endpoint}/{paymentMethodObject.id}", Method.Delete);

            return await request.Execute();
        }

        /// <summary>
        ///     Fund your wallet from a specific payment method.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="priority">Which type of payment method to use to fund the wallet.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static async Task<bool> FundWallet(string amount, PaymentMethod.Priority priority)
        {
            PaymentMethodObject paymentMethod = await GetPaymentMethodByPriority(priority);

            return await FundWallet(amount, paymentMethod);
        }

        /// <summary>
        ///     Fund your wallet from a specific payment method.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="paymentMethodObject">Payment method to use to fund the wallet.</param>
        /// <returns>True if successful, false otherwise.</returns>
        public static async Task<bool> FundWallet(string amount, PaymentMethodObject paymentMethodObject)
        {
            Request request = new Request($"{paymentMethodObject.Endpoint}/{paymentMethodObject.id}/charges", Method.Post);
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
            switch (primaryOrSecondary)
            {
                case PaymentMethod.Priority.Primary:
                    paymentMethod = paymentMethods.primary_payment_method;
                    break;
                case PaymentMethod.Priority.Secondary:
                    paymentMethod = paymentMethods.secondary_payment_method;
                    break;
                default:
                    break;
            }

            if (paymentMethod?.id == null)
            {
                throw new Exception("The chosen payment method is not set up yet.");
            }

            return paymentMethod;
        }
    }
}
