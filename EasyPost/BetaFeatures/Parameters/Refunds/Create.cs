using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Refunds
{
    public class Create : BaseParameters, IRefundParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "refund", "carrier")]
        public string? Carrier { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "refund", "tracking_codes")] // yes, the param name is plural when it's really just one code
        public string? TrackingCode { get; set; }

        #endregion
    }
}
