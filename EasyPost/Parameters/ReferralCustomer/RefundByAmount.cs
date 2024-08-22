using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.ReferralCustomer
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/users/referral-customers#refund-a-referralcustomer">Parameters</a> for <see cref="EasyPost.Services.Beta.ReferralCustomerService.RefundByAmount(RefundByAmount, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RefundByAmount : BaseParameters<Models.API.ReferralCustomer>
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
