using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Insurance
{
    public class Create : BaseParameters, IInsuranceParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "insurance", "amount")]
        public double? Amount { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "insurance", "carrier")]
        public string? Carrier { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "insurance", "from_address")]
        public IAddressParameter? FromAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "insurance", "reference")]
        public string? Reference { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "insurance", "to_address")]
        public IAddressParameter? ToAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "insurance", "tracking_code")]
        public string? TrackingCode { get; set; }

        #endregion
    }
}
