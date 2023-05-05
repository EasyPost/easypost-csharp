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
    ///     Class representing a set of <a href="https://www.easypost.com/docs/api#billing">billing-related functionality</a>.
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
        ///     Fund your wallet from a specific payment method.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="priority">Which type of payment method to use to fund the wallet. Defaults to primary.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A Task to fund the wallet.</returns>
        [CrudOperations.Create]
        public async Task FundWallet(string amount, PaymentMethod.Priority? priority = null, CancellationToken cancellationToken = default)
        {
            priority ??= PaymentMethod.Priority.Primary;

            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority, cancellationToken);

            Dictionary<string, object> parameters = new() { { "amount", amount } };

            await RequestAsync(Method.Post, $"{paymentMethod.Endpoint}/{paymentMethod.Id}/charges", cancellationToken, parameters);
        }

        /// <summary>
        ///     List all payment methods for this account.
        /// </summary>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An EasyPost.PaymentMethodSummary summary object.</returns>
        [CrudOperations.Read]
        public async Task<PaymentMethodsSummary> RetrievePaymentMethodsSummary(CancellationToken cancellationToken = default)
        {
            PaymentMethodsSummary paymentMethodsSummary = await RequestAsync<PaymentMethodsSummary>(Method.Get, "payment_methods", cancellationToken);

            return paymentMethodsSummary.Id == null
                ? throw new InvalidObjectError(Constants.ErrorMessages.NoPaymentMethods)
                : paymentMethodsSummary;
        }

        /// <summary>
        ///     Delete a payment method from the user's account.
        /// </summary>
        /// <param name="priority">Which type of payment method to delete.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>A Task to delete a payment method.</returns>
        [CrudOperations.Delete]
        public async Task DeletePaymentMethod(PaymentMethod.Priority priority, CancellationToken cancellationToken = default)
        {
            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority, cancellationToken);

            await RequestAsync(Method.Delete, $"{paymentMethod.Endpoint}/{paymentMethod.Id}", cancellationToken);
        }

        #endregion

        /// <summary>
        ///     Get a payment method by priority.
        /// </summary>
        /// <param name="priority">Which priority payment method to get.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns>An EasyPost.PaymentMethodObject instance.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the Priority provided is invalid.</exception>
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
