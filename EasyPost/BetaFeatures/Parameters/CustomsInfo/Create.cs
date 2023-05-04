using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.CustomsInfo
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-customsinfo">Parameters</a> for <see cref="EasyPost.Services.CustomsInfoService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, ICustomsInfoParameter
    {
        #region Request Parameters

        /// <summary>
        ///     ID of the <see cref="Models.API.CustomsInfo"/> to reference in this request.
        ///     ID is not used when calling <see cref="Services.CustomsInfoService.Create(Create, System.Threading.CancellationToken)"/>,
        ///     but is used when using this parameter set as a nested parameter set in other API calls.
        /// </summary>
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Human-readable description of the shipment contents.
        ///     Required for certain carriers or if <see cref="ContentsType"/> is <c>"other"</c>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "contents_explanation")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "contents_explanation")]
        public string? ContentsExplanation { get; set; }

        /// <summary>
        ///     Type of contents for the shipment.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "contents_type")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "contents_type")]
        public string? ContentsType { get; set; }

        /// <summary>
        ///     Whether the new <see cref="Models.API.CustomsInfo"/> should be electronically certified.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_certify")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "customs_certify")]
        public bool? CustomsCertify { get; set; }

        /// <summary>
        ///     List of <see cref="Models.API.CustomsItem"/>s (or <see cref="Parameters.CustomsItems.Create"/> parameters) being shipped.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_items")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "customs_items")]
        public List<ICustomsItemParameter>? CustomsItems { get; set; }

        /// <summary>
        ///     The customs signer. Required if <see cref="CustomsCertify"/> is <c>true</c>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_signer")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "customs_signer")]
        public string? CustomsSigner { get; set; }

        /// <summary>
        ///     Customs declaration message, available for eligible carriers.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "declaration")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "declaration")]
        public string? Declaration { get; set; }

        /// <summary>
        ///     <c>"EEL"</c> or <c>"PFC"</c> for the new <see cref="Models.API.CustomsInfo"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "eel_pfc")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "eel_pfc")]
        public string? EelPfc { get; set; }

        /// <summary>
        ///     What to do if the package(s) cannot be delivered. Defaults to <c>"return"</c>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "non_delivery_option")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "non_delivery_option")]
        public string? NonDeliveryOption { get; set; }

        /// <summary>
        ///     Restrictions on the package(s).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "restriction_type")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "restriction_type")]
        public string? RestrictionType { get; set; }

        #endregion
    }
}
