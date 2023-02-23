using EasyPost.Utilities.Annotations;

namespace EasyPost.Beta.Parameters.V2
{
    public static class CustomsItems
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.CustomsItemService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "customs_item", "description")]
            public string? Description { get; set; }

            [RequestParameter(Necessity.Optional, "customs_item", "hs_tariff_number")]
            public string? HsTariffNumber { get; set; }

            [RequestParameter(Necessity.Optional, "customs_item", "origin_country")]
            public string? OriginCountry { get; set; }

            [RequestParameter(Necessity.Optional, "customs_item", "quantity")]
            public int? Quantity { get; set; }

            [RequestParameter(Necessity.Optional, "customs_item", "value")]
            public double? Value { get; set; }

            [RequestParameter(Necessity.Optional, "customs_item", "weight")]
            public double? Weight { get; set; }

            #endregion
        }
    }
}
