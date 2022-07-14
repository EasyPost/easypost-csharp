using System;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Exceptions;
using EasyPost.Models.API;
using EasyPost.Parameters;
using EasyPost.Utilities;
using RestSharp;

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
        /// <returns>Whether the request was successful or not.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<bool> DeletePaymentMethod(PaymentMethodPriority priority)
        {
            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority);

            return await Request(Method.Delete, $"{paymentMethod.Endpoint}/{paymentMethod.Id}");
        }

        /// <summary>
        ///     Fund your wallet from a specific payment method.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="priority">Which type of payment method to use to fund the wallet. Defaults to primary.</param>
        /// <returns>True if successful, false otherwise.</returns>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<bool> FundWallet(Billing.Fund parameters, PaymentMethodPriority? priority = null)
        {
            priority ??= PaymentMethodPriority.Primary;

            PaymentMethod paymentMethod = await GetPaymentMethodByPriority(priority);

            return await Request(Method.Post, $"{paymentMethod.Endpoint}/{paymentMethod.Id}/charge", parameters);
        }

        /// <summary>
        ///     List all payment methods for this account.
        /// </summary>
        /// <returns>An EasyPost.PaymentMethodSummary summary object.</returns>
        /// <exception cref="ServerSideConfigurationException">If billing has not been set up yet.</exception>
        [ApiCompatibility(ApiVersion.Latest)]
        public async Task<PaymentMethodsSummary> RetrievePaymentMethodsSummary()
        {
            PaymentMethodsSummary paymentMethodsSummary = await Get<PaymentMethodsSummary>("payment_methods");
            if (paymentMethodsSummary.Id == null)
            {
                throw new ServerSideConfigurationException("Billing", "Please add a payment method via the dashboard.");
            }

            return paymentMethodsSummary;
        }

        /// <summary>
        ///     Get a payment method by priority.
        /// </summary>
        /// <param name="priority">Which priority payment method to get.</param>
        /// <returns>An EasyPost.PaymentMethodObject instance.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the Priority provided is invalid.</exception>
        private async Task<PaymentMethod> GetPaymentMethodByPriority(PaymentMethodPriority priority)
        {
            PaymentMethodsSummary paymentMethodsSummary = await RetrievePaymentMethodsSummary();

            PaymentMethod? paymentMethod = null;
            var @switch = new SwitchCase
            {
                {
                    PaymentMethodPriority.Primary, () => { paymentMethod = paymentMethodsSummary.PrimaryPaymentMethod; }
                },
                {
                    PaymentMethodPriority.Secondary, () => { paymentMethod = paymentMethodsSummary.SecondaryPaymentMethod; }
                },
                {
                    SwitchCaseScenario.Default, () => throw new Exception("Invalid priority provided.")
                }
            };

            @switch.Match(priority);

            if (paymentMethod?.Id == null)
            {
                throw new ServerSideConfigurationException("The chosen payment method");
            }

            return paymentMethod;
        }
    }
}
