using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Models.API;
using EasyPost.Utilities;

namespace EasyPost.Services
{
    public class BillingService : EasyPostService
    {
        internal BillingService(Client client) : base(client)
        {
        }

        /// <summary>
        ///     Delete a payment method from the user's account.
        /// </summary>
        /// <param name="priority">Which type of payment method to delete.</param>
        public async Task DeletePaymentMethod(PaymentMethod.Priority priority)
        {
            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority);

            await DeleteBlind($"{paymentMethod.Endpoint}/{paymentMethod.id}");
        }

        /// <summary>
        ///     Fund your wallet from a specific payment method.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="priority">Which type of payment method to use to fund the wallet. Defaults to primary.</param>
        public async Task FundWallet(string amount, PaymentMethod.Priority? priority = null)
        {
            priority ??= PaymentMethod.Priority.Primary;

            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority);

            Dictionary<string, object?> parameters = new Dictionary<string, object?>
            {
                {
                    "amount", amount
                }
            };

            await CreateBlind($"{paymentMethod.Endpoint}/{paymentMethod.id}/charge", parameters);
        }

        /// <summary>
        ///     List all payment methods for this account.
        /// </summary>
        /// <returns>An EasyPost.PaymentMethodSummary summary object.</returns>
        public async Task<PaymentMethodsSummary> RetrievePaymentMethodsSummary()
        {
            PaymentMethodsSummary paymentMethodsSummary = await Get<PaymentMethodsSummary>("payment_methods");
            if (paymentMethodsSummary.id == null)
            {
                throw new Exception("Please add a payment method via the dashboard.");
            }

            return paymentMethodsSummary;
        }

        /// <summary>
        ///     Get a payment method by priority.
        /// </summary>
        /// <param name="priority">Which priority payment method to get.</param>
        /// <returns>An EasyPost.PaymentMethodObject instance.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the Priority provided is invalid.</exception>
        private async Task<PaymentMethod> GetPaymentMethodByPriority(PaymentMethod.Priority? priority)
        {
            if (priority == null)
            {
                throw new Exception("Please provide a priority.");
            }

            PaymentMethodsSummary paymentMethodsSummarySummary = await RetrievePaymentMethodsSummary();

            PaymentMethod? paymentMethod = null;
            var @switch = new SwitchCase
            {
                {
                    PaymentMethod.Priority.Primary, () => { paymentMethod = paymentMethodsSummarySummary.primary_payment_method; }
                },
                {
                    PaymentMethod.Priority.Secondary, () => { paymentMethod = paymentMethodsSummarySummary.secondary_payment_method; }
                },
                {
                    SwitchCaseScenario.Default, () => throw new Exception("Invalid priority provided.")
                }
            };

            @switch.Match(priority);

            if (paymentMethod?.id == null)
            {
                throw new Exception("The chosen payment method has not been set up yet.");
            }

            return paymentMethod;
        }
    }
}