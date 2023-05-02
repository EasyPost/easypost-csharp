using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.API;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Models.API.Beta;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services.Beta
{
    public class ReferralCustomerService : EasyPostService
    {
        public ReferralCustomerService(EasyPostClient client)
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
        /// <exception cref="ApiError">When the request fails.</exception>
        [CrudOperations.Update]
        public async Task<PaymentMethod> AddPaymentMethod(string stripeCustomerId, string paymentMethodReference, PaymentMethod.Priority? priority = null, CancellationToken cancellationToken = default)
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

            return await RequestAsync<PaymentMethod>(Method.Post, "referral_customers/payment_method", cancellationToken, parameters, overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Add a Stripe payment method to a <see cref="ReferralCustomer"/>.
        ///     This function should be called against a Client configured with the Referral Customer's API key.
        ///     This function will fail if called against a non-Referral Customer Client.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.ReferralCustomers.AddPaymentMethod"/> parameter set.</param>
        /// <returns>A <see cref="PaymentMethod"/> object.</returns>
        /// <exception cref="ApiError">When the request fails.</exception>
        [CrudOperations.Update]
        public async Task<PaymentMethod> AddPaymentMethod(BetaFeatures.Parameters.ReferralCustomers.AddPaymentMethod parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<PaymentMethod>(Method.Post, "referral_customers/payment_method", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Refund a Referral Customer's wallet by a specified amount.
        ///     Refund will be issued to the user's original payment method.
        /// </summary>
        /// <param name="amount">Amount in cents to refund the Referral Customer.</param>
        /// <returns>A <see cref="PaymentRefund"/> object.</returns>
        [CrudOperations.Update]
        public async Task<PaymentRefund> RefundByAmount(int amount, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                { "refund_amount", amount },
            };

            return await RequestAsync<PaymentRefund>(Method.Post, "referral_customers/refunds", cancellationToken, parameters, overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Refund a <see cref="ReferralCustomer"/>'s wallet for a specified amount.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.ReferralCustomers.RefundByAmount"/> parameter set.</param>
        /// <returns>A <see cref="PaymentRefund"/> object.</returns>
        [CrudOperations.Update]
        public async Task<PaymentRefund> RefundByAmount(BetaFeatures.Parameters.ReferralCustomers.RefundByAmount parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<PaymentRefund>(Method.Post, "referral_customers/refunds", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Refund a Referral Customer's wallet for a specified payment log entry.
        ///     Refund will be issued to the user's original payment method.
        /// </summary>
        /// <param name="paymentLogId">Payment log ID to refund.</param>
        /// <returns>A <see cref="PaymentRefund"/> object.</returns>
        [CrudOperations.Update]
        public async Task<PaymentRefund> RefundByPaymentLog(string paymentLogId, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> parameters = new()
            {
                { "payment_log_id", paymentLogId },
            };

            return await RequestAsync<PaymentRefund>(Method.Post, "referral_customers/refunds", cancellationToken, parameters, overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Refund a <see cref="ReferralCustomer"/>'s wallet for a specified payment log entry.
        /// </summary>
        /// <param name="parameters"><see cref="BetaFeatures.Parameters.ReferralCustomers.RefundByPaymentLog"/> parameter set.</param>
        /// <returns>A <see cref="PaymentRefund"/> object.</returns>
        [CrudOperations.Update]
        public async Task<PaymentRefund> RefundByPaymentLog(BetaFeatures.Parameters.ReferralCustomers.RefundByPaymentLog parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<PaymentRefund>(Method.Post, "referral_customers/refunds", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);
        }

        #endregion
    }
}
