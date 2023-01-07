using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using RestSharp;

namespace EasyPost.Services.Beta
{
    public class ReferralService : EasyPostService
    {
        public ReferralService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Add a Stripe payment method to a Referral Customer.
        ///     This function should be called against a Client configured with the Referral Customer's API key.
        ///     This function will fail if called against a non-Referral Customer Client.
        /// </summary>
        /// <param name="stripeCustomerId">Unique customer ID provided by Stripe.</param>
        /// <param name="paymentMethodReference">ID of the card or bank account provided by Stripe.</param>
        /// <param name="priority">Which priority to save this payment method as on EasyPost.</param>
        /// <returns>A <see cref="PaymentMethod"/> object.</returns>
        /// <exception cref="Exception">When the request fails.</exception>
        public async Task<PaymentMethod> AddPaymentMethod(string stripeCustomerId, string paymentMethodReference, PaymentMethod.Priority? priority = null)
        {
            priority ??= PaymentMethod.Priority.Primary;

            Dictionary<string, object> parameters = new()
            {
                {
                    "payment_method",
                    new Dictionary<string, object>
                    {
                        { "stripe_customer_id", stripeCustomerId },
                        { "payment_method_reference", paymentMethodReference },
                        { "priority", priority.ToString().ToLowerInvariant() },
                    }
                },
            };

            return await Request<PaymentMethod>(Method.Post, "referral_customers/payment_method", parameters, overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Refund a Referral Customer's wallet by a specified amount.
        ///     Refund will be issued to the user's original payment method.
        /// </summary>
        /// <param name="amount">Amount in cents to refund the Referral Customer.</param>
        /// <returns>A <see cref="PaymentRefund"/> object.</returns>
        public async Task<PaymentRefund> RefundByAmount(int amount)
        {
            Dictionary<string, object> parameters = new()
            {
                { "refund_amount", amount },
            };

            return await Request<PaymentRefund>(Method.Post, "referral_customers/refunds", parameters, overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Refund a Referral Customer's wallet for a specified payment log entry.
        ///     Refund will be issued to the user's original payment method.
        /// </summary>
        /// <param name="paymentLogId">Payment log ID to refund.</param>
        /// <returns>A <see cref="PaymentRefund"/> object.</returns>
        public async Task<PaymentRefund> RefundByPaymentLog(string paymentLogId)
        {
            Dictionary<string, object> parameters = new()
            {
                { "payment_log_id", paymentLogId },
            };

            return await Request<PaymentRefund>(Method.Post, "referral_customers/refunds", parameters, overrideApiVersion: ApiVersion.Beta);
        }

        #endregion
    }
}
