using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.CustomsItems
{
    public class Create : BaseParameters, ICustomsItemParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "description")]
        public string? Description { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "hs_tariff_number")]
        public string? HsTariffNumber { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "origin_country")]
        public string? OriginCountry { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "quantity")]
        public int? Quantity { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "value")]
        public double? Value { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "weight")]
        public double? Weight { get; set; }

        #endregion
    }
}
