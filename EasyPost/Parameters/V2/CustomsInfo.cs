using System.Collections.Generic;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Parameters.V2
{
    public static class CustomsInfo
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.CustomsInfoService.Create"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "customs_info", "contents_explanation")]
            public string? ContentsExplanation { get; set; }

            [RequestParameter(Necessity.Optional, "customs_info", "contents_type")]
            public string? ContentsType { get; set; }

            [RequestParameter(Necessity.Optional, "customs_info", "customs_certify")]
            public bool CustomsCertify { get; set; } = false;

            [RequestParameter(Necessity.Optional, "customs_info", "customs_items")]
            public List<EasyPost.Models.API.CustomsItem>? CustomsItems { get; set; }

            [RequestParameter(Necessity.Optional, "customs_info", "customs_signer")]
            public string? CustomsSigner { get; set; }

            [RequestParameter(Necessity.Optional, "customs_info", "eel_pfc")]
            public string? EelPfc { get; set; }

            [RequestParameter(Necessity.Optional, "customs_info", "non_delivery_option")]
            public string? NonDeliveryOption { get; set; }

            [RequestParameter(Necessity.Optional, "customs_info", "restriction_type")]
            public string? RestrictionType { get; set; }

            #endregion
        }
    }
}
