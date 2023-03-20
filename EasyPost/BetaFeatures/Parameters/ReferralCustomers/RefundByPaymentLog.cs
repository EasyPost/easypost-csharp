using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.ReferralCustomers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.Beta.ReferralService.RefundByPaymentLog(RefundByPaymentLog)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class RefundByPaymentLog : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "payment_log_id")]
        public string? PaymentLogId { get; set; }

        #endregion
    }
}
