using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.CustomsItems
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-customsitem">Parameters</a> for <see cref="EasyPost.Services.CustomsItemService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, ICustomsItemParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Description of the item being shipped. Required.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "description")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "description")]
        public string? Description { get; set; }

        /// <summary>
        ///     <a href="https://hts.usitc.gov/">Harmonized Tariff Schedule</a> number for the item.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "hs_tariff_number")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "hs_tariff_number")]
        public string? HsTariffNumber { get; set; }

        /// <summary>
        ///     Two-letter code for the origin country. Required.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "origin_country")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "origin_country")]
        public string? OriginCountry { get; set; }

        /// <summary>
        ///     Quantity of the item being shipped. Required.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "quantity")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        ///     Total value of all items (unit value * <see cref="Quantity"/>) in USD. Required.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "value")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "value")]
        public double? Value { get; set; }

        /// <summary>
        ///     Total weight of all items (unit weight * <see cref="Quantity"/>) in ounces. Required.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_item", "weight")]
        [NestedRequestParameter(typeof(CustomsInfo.Create), Necessity.Optional, "weight")]
        public double? Weight { get; set; }

        #endregion
    }
}
