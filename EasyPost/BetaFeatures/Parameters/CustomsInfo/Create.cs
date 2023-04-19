using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.CustomsInfo
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.CustomsInfoService.Create(Create)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Create : BaseParameters, ICustomsInfoParameter
    {
        #region Request Parameters

        // ID can't be provided when creating a customs info, but can be provided when using a customs info in a non-customs info creation request.
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Insurance.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Orders.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Pickups.Create), Necessity.Optional, "id")]
        [NestedRequestParameter(typeof(Beta.Rates.Retrieve), Necessity.Optional, "id")]
        public string? Id { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "contents_explanation")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "contents_explanation")]
        public string? ContentsExplanation { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "contents_type")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "contents_type")]
        public string? ContentsType { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_certify")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "customs_certify")]
        public bool? CustomsCertify { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_items")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "customs_items")]
        public List<ICustomsItemParameter>? CustomsItems { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_signer")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "customs_signer")]
        public string? CustomsSigner { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "declaration")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "declaration")]
        public string? Declaration { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "eel_pfc")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "eel_pfc")]
        public string? EelPfc { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "non_delivery_option")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "non_delivery_option")]
        public string? NonDeliveryOption { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "restriction_type")]
        [NestedRequestParameter(typeof(Shipments.Create), Necessity.Optional, "restriction_type")]
        public string? RestrictionType { get; set; }

        #endregion
    }
}
