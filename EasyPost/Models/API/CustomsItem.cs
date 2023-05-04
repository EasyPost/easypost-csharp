using EasyPost._base;
using EasyPost.BetaFeatures.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CustomsItem : EasyPostObject, ICustomsItemParameter
    {
        #region JSON Properties

        [JsonProperty("code")]
        public string? Code { get; internal set; }
        [JsonProperty("currency")]
        public string? Currency { get; internal set; }
        [JsonProperty("description")]
        public string? Description { get; internal set; }
        [JsonProperty("hs_tariff_number")]
        public string? HsTariffNumber { get; internal set; }
        [JsonProperty("origin_country")]
        public string? OriginCountry { get; internal set; }
        [JsonProperty("quantity")]
        public int? Quantity { get; internal set; }
        [JsonProperty("value")]
        public double? Value { get; internal set; }
        [JsonProperty("weight")]
        public double? Weight { get; internal set; }

        #endregion

        internal CustomsItem()
        {
        }
    }
}
