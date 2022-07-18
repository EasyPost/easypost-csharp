using System.Collections.Generic;
using EasyPost._base;
using EasyPost.ApiCompatibility;
using EasyPost.Clients;
using EasyPost.Models.API;

namespace EasyPost.Parameters
{
    public static class CustomsInfo
    {
        public sealed class Create : RequestParameters
        {
            #region Request Parameters

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "customs_certify")]
            public bool? CustomsCertify { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "customs_signer")]
            public string? CustomsSigner { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "contents_type")]
            public string? ContentsType { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "restrictions_type")]
            public string? RestrictionsType { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "eel_pfc")]
            public string? EelPfc { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "customs_items")]
            public List<CustomsItem>? CustomsItems { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "contents_explanation")]
            public string? ContentsExplanation { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "restriction_type")]
            public string? RestrictionType { internal get; set; }

            [ApiCompatibility(ApiVersion.Latest)]
            [RequestParameter(Necessity.Optional, "customs_info", "non_delivery_option")]
            public string? NonDeliveryOption { internal get; set; }

            #endregion

            public Create(Dictionary<string, object?>? overrideParameters = null) : base(overrideParameters)
            {
            }
        }
    }
}
