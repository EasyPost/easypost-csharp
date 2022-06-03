using System;
using System.Threading.Tasks;
using EasyPost.Interfaces;
using EasyPost.Models.V2;

namespace EasyPost.Services.V2
{
    public class PaymentService : Service
    {
        /// <summary>
        ///     Payment method priority
        /// </summary>
        public enum Priority
        {
            Primary,
            Secondary
        }

        internal PaymentService(BaseClient client) : base(client)
        {
        }

        /// <summary>
        ///     Get a payment method (credit card) by priority.
        /// </summary>
        /// <param name="priority">Which priority payment method to get.</param>
        /// <returns>An EasyPost.CreditCard instance.</returns>
        /// <exception cref="Exception">Billing has not been set up yet, or the Priority provided is invalid.</exception>
        private async Task<CreditCard> GetPaymentMethodByPriority(Priority priority)
        {
            PaymentMethodSummary summary = await All();

            CreditCard? paymentMethod = null;
            switch (priority)
            {
                case Priority.Primary:
                    paymentMethod = summary.primary_payment_method;
                    break;
                case Priority.Secondary:
                    paymentMethod = summary.secondary_payment_method;
                    break;
                default:
                    break;
            }

            if (paymentMethod == null)
            {
                throw new Exception("The chosen payment method is not a valid method. Please try again.");
            }

            return paymentMethod;
        }

        /// <summary>
        ///     List all payment methods for this account.
        /// </summary>
        /// <returns>An EasyPost.PaymentMethod summary object.</returns>
        /// <exception cref="Exception"></exception>
        public async Task<PaymentMethodSummary> All()
        {
            PaymentMethodSummary summary = await Get<PaymentMethodSummary>("payment_methods");

            if (summary.id == null)
            {
                throw new Exception("Billing has not been setup for this user. Please add a payment method.");
            }

            return summary;
        }

        /// <summary>
        ///     Delete a credit card.
        /// </summary>
        /// <param name="priority">Which payment method to delete.</param>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<bool> DeletePaymentMethod(Priority priority)
        {
            CreditCard paymentMethod = await GetPaymentMethodByPriority(priority);

            return await paymentMethod.Delete();
        }

        /// <summary>
        ///     Fund a credit card.
        /// </summary>
        /// <param name="amount">Amount to fund.</param>
        /// <param name="priority">Which payment method to fund.</param>
        /// <returns>Whether the request was successful or not.</returns>
        public async Task<CreditCardFunding> FundPaymentMethod(string amount, Priority priority)
        {
            CreditCard paymentMethod = await GetPaymentMethodByPriority(priority);

            return await paymentMethod.Fund(amount);
        }
    }
}
