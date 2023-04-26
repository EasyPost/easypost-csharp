using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Services.Beta;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.ReferralCustomers
{
    /// <summary>
    ///     Parameters for <see cref="ReferralCustomerService.AddPaymentMethod(AddPaymentMethod)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class AddPaymentMethod : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "payment_method", "stripe_customer_id")]
        public string? StripeCustomerId { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "payment_method", "payment_method_reference")]
        public string? PaymentMethodReference { get; set; }

        // Users will set this property, but toDictionary() will use PriorityString instead.
        // Default to Primary priority.
        public PaymentMethod.Priority? Priority { get; set; } = PaymentMethod.Priority.Primary;

        [TopLevelRequestParameter(Necessity.Required, "payment_method", "priority")]
        internal string? PriorityString => Priority?.ToString().ToLowerInvariant();

        #endregion
    }
}
