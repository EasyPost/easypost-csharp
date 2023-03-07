using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Refunds
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.RefundService.Create(Create)"/> API calls.
    /// </summary>
    public class Create : BaseParameters, IRefundParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "refund", "carrier")]
        public string? Carrier { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "refund", "tracking_codes")]
        public List<string>? TrackingCodes { get; set; }

        #endregion
    }
}
