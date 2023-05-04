using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Refunds
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-refund">Parameters</a> for <see cref="EasyPost.Services.RefundService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, IRefundParameter
    {
        #region Request Parameters

        /// <summary>
        ///     The carrier to request a refund from.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "refund", "carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     A list of tracking codes to request refunds for.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "refund", "tracking_codes")]
        public List<string>? TrackingCodes { get; set; }

        #endregion
    }
}
