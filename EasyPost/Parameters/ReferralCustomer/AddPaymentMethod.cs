using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.ReferralCustomer
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#add-payment-method-to-referral-user">Parameters</a> for <see cref="EasyPost.Services.Beta.ReferralCustomerService.AddPaymentMethod(AddPaymentMethod, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class AddPaymentMethod : BaseParameters<Models.API.ReferralCustomer>
    {
        #region Request Parameters

        /// <summary>
        ///     The Stripe-provided customer ID associated with the payment method being added.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "payment_method", "stripe_customer_id")]
        public string? StripeCustomerId { get; set; }

        /// <summary>
        ///     The Stripe-provided payment method ID associated with the payment method being added.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "payment_method", "payment_method_reference")]
        public string? PaymentMethodReference { get; set; }

        /// <summary>
        ///     Which priority to assign to the payment method in their EasyPost account.
        /// </summary>
        // Users will set this property, but toDictionary() will use PriorityString instead.
        // Default to Primary priority.
        public PaymentMethod.Priority? Priority { get; set; } = PaymentMethod.Priority.Primary;

        /// <summary>
        ///     The <see cref="Priority"/> as a string.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "payment_method", "priority")]
        internal string? PriorityString => Priority?.ToString().ToLowerInvariant();

        #endregion
    }
}
