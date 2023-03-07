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
        public string? ContentsExplanation { get; set; }
        [JsonProperty("contents_type")]
        public string? ContentsType { get; set; }
        [JsonProperty("customs_certify")]
        public string? CustomsCertify { get; set; }
        [JsonProperty("customs_items")]
        public List<CustomsItem>? CustomsItems { get; set; }
        [JsonProperty("customs_signer")]
        public string? CustomsSigner { get; set; }
        [JsonProperty("declaration")]
        public string? Declaration { get; set; }
        [JsonProperty("eel_pfc")]
        public string? EelPfc { get; set; }
        [JsonProperty("non_delivery_option")]
        public string? NonDeliveryOption { get; set; }
        [JsonProperty("restriction_comments")]
        public string? RestrictionComments { get; set; }
        [JsonProperty("restriction_type")]
        public string? RestrictionType { get; set; }

        #endregion

        internal CustomsInfo()
        {
        }
    }
#pragma warning restore CA1724 // Naming conflicts with Parameters.CustomInfo
}
