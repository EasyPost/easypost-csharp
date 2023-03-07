using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.CustomsItems
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.CustomsItemService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, ICustomsItemParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "description")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "description")]
        public string? Description { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "hs_tariff_number")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "hs_tariff_number")]
        public string? HsTariffNumber { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "origin_country")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "origin_country")]
        public string? OriginCountry { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "quantity")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "quantity")]
        public int? Quantity { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "value")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "value")]
        public double? Value { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "weight")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "weight")]
        public double? Weight { get; set; }

        #endregion
    }
}
