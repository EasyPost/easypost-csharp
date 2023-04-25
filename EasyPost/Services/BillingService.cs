using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Exceptions.General;
using EasyPost.Http;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Services
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BillingService : EasyPostService
    {
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
        /// <returns>A Task to fund the wallet.</returns>
        [CrudOperations.Create]
        public async Task FundWallet(string amount, PaymentMethod.Priority? priority = null)
        {
            priority ??= PaymentMethod.Priority.Primary;

            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority);

            Dictionary<string, object> parameters = new() { { "amount", amount } };

            await Request(Method.Post,$"{paymentMethod.Endpoint}/{paymentMethod.Id}/charges", parameters);
        }

        /// <summary>
        ///     List all payment methods for this account.
        /// </summary>
        /// <returns>An EasyPost.PaymentMethodSummary summary object.</returns>
        [CrudOperations.Read]
        public async Task<PaymentMethodsSummary> RetrievePaymentMethodsSummary()
        {
            PaymentMethodsSummary paymentMethodsSummary = await Request<PaymentMethodsSummary>(Method.Get, "payment_methods");

            return paymentMethodsSummary.Id == null
                ? throw new InvalidObjectError(Constants.ErrorMessages.NoPaymentMethods)
                : paymentMethodsSummary;
        }

        /// <summary>
        ///     Delete a payment method from the user's account.
        /// </summary>
        /// <param name="priority">Which type of payment method to delete.</param>
        /// <returns>A Task to delete a payment method.</returns>
        [CrudOperations.Delete]
        public async Task DeletePaymentMethod(PaymentMethod.Priority priority)
        {
            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority);

            await Request(Method.Delete, $"{paymentMethod.Endpoint}/{paymentMethod.Id}");
        }

        #endregion

        /// <summary>
        ///     Get a payment method by priority.
        /// </summary>
        /// <param name="priority">Which priority payment method to get.</param>
        /// <returns>An EasyPost.PaymentMethodObject instance.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the Priority provided is invalid.</exception>
        private async Task<PaymentMethod> GetPaymentMethodByPriority(PaymentMethod.Priority priority)
        {
            PaymentMethodsSummary paymentMethodsSummarySummary = await RetrievePaymentMethodsSummary();

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
