using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of <a href="https://docs.easypost.com/docs/users/billing">billing-related functionality</a>.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BillingService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BillingService" /> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal BillingService(EasyPostClient client)
            : base(client)
        {
        }

        #region CRUD Operations

        /// <summary>
        ///     Fund your wallet from a specific <see cref="PaymentMethod"/>.
        ///     <a href="https://docs.easypost.com/docs/users/billing#add-funds-to-your-wallet-one-time-charge">Related API documentation</a>.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="priority">Which <see cref="PaymentMethod.Priority"/> of payment method to use to fund the wallet. Defaults to <see cref="PaymentMethod.Priority.Primary"/>.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>None.</returns>
        [CrudOperations.Create]
        public async Task FundWallet(string amount, PaymentMethod.Priority? priority = null, CancellationToken cancellationToken = default)
        {
            priority ??= PaymentMethod.Priority.Primary;

            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority, cancellationToken);

            Dictionary<string, object> parameters = new() { { "amount", amount } };

            await RequestAsync(Method.Post, $"{paymentMethod.Endpoint}/{paymentMethod.Id}/charges", cancellationToken, parameters);
        }

        /// <summary>
        ///     List all <see cref="PaymentMethod"/>s for the user's account.
        ///     <a href="https://docs.easypost.com/docs/users/billing#retrieve-payment-methods">Related API documentation</a>.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="PaymentMethodsSummary"/> object.</returns>
        [CrudOperations.Read]
        public async Task<PaymentMethodsSummary> RetrievePaymentMethodsSummary(CancellationToken cancellationToken = default)
        {
            PaymentMethodsSummary paymentMethodsSummary = await RequestAsync<PaymentMethodsSummary>(Method.Get, "payment_methods", cancellationToken);

            return paymentMethodsSummary.Id == null
                ? throw new InvalidObjectError(Constants.ErrorMessages.NoPaymentMethods)
                : paymentMethodsSummary;
        }

        /// <summary>
        ///     Delete a <see cref="PaymentMethod"/> from the user's account.
        ///     <a href="https://docs.easypost.com/docs/users/billing#delete-a-payment-method">Related API documentation</a>.
        /// </summary>
        /// <param name="priority">Which <see cref="PaymentMethod.Priority"/> of payment method to delete.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>None.</returns>
        [CrudOperations.Delete]
        public async Task DeletePaymentMethod(PaymentMethod.Priority priority, CancellationToken cancellationToken = default)
        {
            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority, cancellationToken);

            await RequestAsync(Method.Delete, $"{paymentMethod.Endpoint}/{paymentMethod.Id}", cancellationToken);
        }

        #endregion

        /// <summary>
        ///     Get a <see cref="PaymentMethod"/> by priority.
        /// </summary>
        /// <param name="priority">Which priority <see cref="PaymentMethod"/> to get.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A <see cref="PaymentMethod"/> object.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the <see cref="PaymentMethod.Priority"/> provided is invalid.</exception>
        private async Task<PaymentMethod> GetPaymentMethodByPriority(PaymentMethod.Priority priority, CancellationToken cancellationToken = default)
        {
            PaymentMethodsSummary paymentMethodsSummarySummary = await RetrievePaymentMethodsSummary(cancellationToken);

            PaymentMethod? paymentMethod = null;
            SwitchCase @switch = new()
            {
                { PaymentMethod.Priority.Primary, () => { paymentMethod = paymentMethodsSummarySummary.PrimaryPaymentMethod; } },
                { PaymentMethod.Priority.Secondary, () => { paymentMethod = paymentMethodsSummarySummary.SecondaryPaymentMethod; } },
                { SwitchCaseScenario.Default, () => throw new InvalidParameterError("priority") },
            };

            @switch.MatchFirst(priority);

#pragma warning disable CA1508 // Avoid dead conditional code (false positive)
            return paymentMethod?.Id == null ? throw new InvalidObjectError(Constants.ErrorMessages.PaymentNotSetUp) : paymentMethod;
#pragma warning restore CA1508 // Avoid dead conditional code
        }
    }
}
