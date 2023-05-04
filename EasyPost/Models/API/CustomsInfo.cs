using System.Collections.Generic;
using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1724 // Naming conflicts with Parameters.CustomInfo
    public class CustomsInfo : EasyPostObject, ICustomsInfoParameter
    {
        #region JSON Properties

        [JsonProperty("contents_explanation")]
        public string? ContentsExplanation { get; internal set; }
        [JsonProperty("contents_type")]
        public string? ContentsType { get; internal set; }
        [JsonProperty("customs_certify")]
        public string? CustomsCertify { get; internal set; }
        [JsonProperty("customs_items")]
        public List<CustomsItem>? CustomsItems { get; internal set; }
        [JsonProperty("customs_signer")]
        public string? CustomsSigner { get; internal set; }
        [JsonProperty("declaration")]
        public string? Declaration { get; internal set; }
        [JsonProperty("eel_pfc")]
        public string? EelPfc { get; internal set; }
        [JsonProperty("non_delivery_option")]
        public string? NonDeliveryOption { get; internal set; }
        [JsonProperty("restriction_comments")]
        public string? RestrictionComments { get; internal set; }
        [JsonProperty("restriction_type")]
        public string? RestrictionType { get; internal set; }

        #endregion

        internal CustomsInfo()
        {
        }
    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.CustomInfo
}
