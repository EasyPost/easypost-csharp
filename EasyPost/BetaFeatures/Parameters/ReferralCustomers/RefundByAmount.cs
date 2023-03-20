using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.ReferralCustomers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.Beta.ReferralService.RefundByAmount(RefundByAmount)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class RefundByAmount : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "refund_amount")]
        public int? Amount { get; set; }

        #endregion
    }
}
