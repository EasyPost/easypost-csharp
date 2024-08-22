using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.ReferralCustomer
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/users/referral-customers#refund-a-referralcustomer">Parameters</a> for <see cref="EasyPost.Services.Beta.ReferralCustomerService.RefundByPaymentLog(RefundByPaymentLog, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RefundByPaymentLog : BaseParameters<Models.API.ReferralCustomer>
    {
        #region Request Parameters

        /// <summary>
        ///     The ID of the payment log to refund.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "payment_log_id")]
        public string? PaymentLogId { get; set; }

        #endregion
    }
}
