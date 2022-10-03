using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class CustomsItem : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("code")]
        public string? Code { get; set; }
        [JsonProperty("currency")]
        public string? Currency { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("hs_tariff_number")]
        public string? HsTariffNumber { get; set; }
        [JsonProperty("origin_country")]
        public string? OriginCountry { get; set; }
        [JsonProperty("quantity")]
        public int? Quantity { get; set; }
        [JsonProperty("value")]
        public double? Value { get; set; }
        [JsonProperty("weight")]
        public double? Weight { get; set; }

        #endregion

        internal CustomsItem()
        {
        }
    }
}
