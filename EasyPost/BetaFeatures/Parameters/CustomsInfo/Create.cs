using System.Collections.Generic;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.CustomsInfo
{
    public class Create : BaseParameters, ICustomsInfoParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "contents_explanation")]
        public string? ContentsExplanation { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "contents_type")]
        public string? ContentsType { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_certify")]
        public bool CustomsCertify { get; set; } = false;

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_items")]
        public List<EasyPost.Models.API.CustomsItem>? CustomsItems { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "customs_signer")]
        public string? CustomsSigner { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "eel_pfc")]
        public string? EelPfc { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "non_delivery_option")]
        public string? NonDeliveryOption { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "customs_info", "restriction_type")]
        public string? RestrictionType { get; set; }

        #endregion
    }
}
