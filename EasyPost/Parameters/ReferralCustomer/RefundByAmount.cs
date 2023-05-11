using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.ReferralCustomer
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#refund-a-referral-user">Parameters</a> for <see cref="EasyPost.Services.Beta.ReferralCustomerService.RefundByAmount(RefundByAmount, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RefundByAmount : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     The amount to refund the user.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "refund_amount")]
        public int? Amount { get; set; }

        #endregion
    }
}
