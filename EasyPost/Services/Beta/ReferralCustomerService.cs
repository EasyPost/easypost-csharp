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
    /// <summary>
    ///     Class representing a set of <a href="https://docs.easypost.com/docs/users/referral-customers">referral customer-related beta functionality</a>.
    /// </summary>
    public class ReferralCustomerService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ReferralCustomerService"/> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        public ReferralCustomerService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Add a Stripe payment method to a <see cref="ReferralCustomer"/>
        ///     This function should be called against a <see cref="EasyPost.Client"/> configured with the <see cref="ReferralCustomer"/>'s API key.
        ///     This function will fail if called against a non-Referral Customer Client.
        ///     <a href="https://docs.easypost.com/docs/users/referral-customers#add-payment-method-to-referralcustomer">Related API documentation</a>.
        /// </summary>
        /// <param name="stripeCustomerId">Unique customer ID provided by Stripe.</param>
        /// <param name="paymentMethodReference">ID of the card or bank account provided by Stripe.</param>
        /// <param name="priority">Which priority to save this payment method as on EasyPost.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
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
        ///     Add a Stripe payment method to a <see cref="ReferralCustomer"/>
        ///     This function should be called against a <see cref="EasyPost.Client"/> configured with the <see cref="ReferralCustomer"/>'s API key.
        ///     This function will fail if called against a non-Referral Customer Client.
        ///     <a href="https://docs.easypost.com/docs/users/referral-customers#add-payment-method-to-referralcustomer">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.ReferralCustomer.AddPaymentMethod"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="PaymentMethod"/> object.</returns>
        /// <exception cref="ApiError">When the request fails.</exception>
        [CrudOperations.Update]
        public async Task<PaymentMethod> AddPaymentMethod(Parameters.ReferralCustomer.AddPaymentMethod parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<PaymentMethod>(Method.Post, "referral_customers/payment_method", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Refund a <see cref="ReferralCustomer"/>'s wallet by a specified amount.
        ///     Refund will be issued to the user's original payment method.
        ///     <a href="https://docs.easypost.com/docs/users/referral-customers#refund-a-referralcustomer">Related API documentation</a>.
        /// </summary>
        /// <param name="amount">Amount in cents to refund the Referral Customer.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
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
        ///     Refund a <see cref="ReferralCustomer"/>'s wallet by a specified amount.
        ///     Refund will be issued to the user's original payment method.
        ///     <a href="https://docs.easypost.com/docs/users/referral-customers#refund-a-referralcustomer">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.ReferralCustomer.RefundByAmount"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="PaymentRefund"/> object.</returns>
        [CrudOperations.Update]
        public async Task<PaymentRefund> RefundByAmount(Parameters.ReferralCustomer.RefundByAmount parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<PaymentRefund>(Method.Post, "referral_customers/refunds", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Refund a <see cref="ReferralCustomer"/>'s wallet for a specified payment log entry.
        ///     Refund will be issued to the user's original payment method.
        ///     <a href="https://docs.easypost.com/docs/users/referral-customers#refund-a-referralcustomer">Related API documentation</a>.
        /// </summary>
        /// <param name="paymentLogId">Payment log ID to refund.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
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
        ///     Refund will be issued to the user's original payment method.
        ///     <a href="https://docs.easypost.com/docs/users/referral-customers#refund-a-referralcustomer">Related API documentation</a>.
        /// </summary>
        /// <param name="parameters"><see cref="Parameters.ReferralCustomer.RefundByPaymentLog"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="PaymentRefund"/> object.</returns>
        [CrudOperations.Update]
        public async Task<PaymentRefund> RefundByPaymentLog(Parameters.ReferralCustomer.RefundByPaymentLog parameters, CancellationToken cancellationToken = default)
        {
            return await RequestAsync<PaymentRefund>(Method.Post, "referral_customers/refunds", cancellationToken, parameters.ToDictionary(), overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Creates a client secret to use with Stripe when adding a credit card.
        ///     <a href="https://docs.easypost.com/docs/users/billing#confirm-the-setupintents-via-stripejs">Related API documentation</a>.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="StripeClientSecret"/> object containing the client secret.</returns>
        /// <exception cref="ApiError">When the request fails.</exception>
        [CrudOperations.Create]
        public async Task<StripeClientSecret> CreateCreditCardClientSecret(CancellationToken cancellationToken = default)
        {
            return await RequestAsync<StripeClientSecret>(Method.Post, "setup_intents", cancellationToken, null, overrideApiVersion: ApiVersion.Beta);
        }

        /// <summary>
        ///     Creates a client secret to use with Stripe when adding a bank account.
        ///     <a href="https://docs.easypost.com/docs/users/billing#collect-bank-account-details">Related API documentation</a>.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="StripeClientSecret"/> object containing the client secret.</returns>
        /// <exception cref="ApiError">When the request fails.</exception>
        [CrudOperations.Create]
        public async Task<StripeClientSecret> CreateBankAccountClientSecret(CancellationToken cancellationToken = default)
        {
            return await CreateBankAccountClientSecret(null, cancellationToken);
        }

        /// <summary>
        ///     Creates a client secret to use with Stripe when adding a bank account.
        ///     <a href="https://docs.easypost.com/docs/users/billing#collect-bank-account-details">Related API documentation</a>.
        /// </summary>
        /// <param name="returnUrl">Optional return URL for the bank account setup.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="StripeClientSecret"/> object containing the client secret.</returns>
        /// <exception cref="ApiError">When the request fails.</exception>
        [CrudOperations.Create]
        public async Task<StripeClientSecret> CreateBankAccountClientSecret(string? returnUrl, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object>? parameters = null;

            if (!string.IsNullOrEmpty(returnUrl))
            {
                parameters = new Dictionary<string, object>
                {
                    { "return_url", returnUrl },
                };
            }

            return await RequestAsync<StripeClientSecret>(Method.Post, "financial_connections_sessions", cancellationToken, parameters, overrideApiVersion: ApiVersion.Beta);
        }

        #endregion
    }
}
