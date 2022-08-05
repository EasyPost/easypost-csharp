using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CustomsInfo : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("contents_explanation")]
        public string contents_explanation { get; set; }
        [JsonProperty("contents_type")]
        public string contents_type { get; set; }

        [JsonProperty("customs_certify")]
        public string customs_certify { get; set; }
        [JsonProperty("customs_items")]
        public List<CustomsItem> customs_items { get; set; }
        [JsonProperty("customs_signer")]
        public string customs_signer { get; set; }
        [JsonProperty("eel_pfc")]
        public string eel_pfc { get; set; }

        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("non_delivery_option")]
        public string non_delivery_option { get; set; }
        [JsonProperty("restriction_comments")]
        public string restriction_comments { get; set; }
        [JsonProperty("restriction_type")]
        public string restriction_type { get; set; }

        [JsonProperty("declaration")]
        public string declaration { get; set; }

        #endregion
    }
}
