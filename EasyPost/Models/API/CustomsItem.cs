using EasyPost._base;
using EasyPost.Parameters;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#customs-item-object">EasyPost customs item</a>.
    /// </summary>
    public class CustomsItem : EasyPostObject, ICustomsItemParameter
    {
        #region JSON Properties

        /// <summary>
        ///     The SKU, UPC or other product identifier.
        /// </summary>
        [JsonProperty("code")]
        public string? Code { get; set; }

        /// <summary>
        ///     The three-letter ISO 4217 currency code.
        ///     Defaults to "USD".
        /// </summary>
        [JsonProperty("currency")]
        public string? Currency { get; set; }

        /// <summary>
        ///     The description of the item being shipped.
        /// </summary>
        [JsonProperty("description")]
        public string? Description { get; set; }

        /// <summary>
        ///     The Harmonized Tariff Schedule (HTS) code for the item.
        ///     See https://hts.usitc.gov/ for more information.
        /// </summary>
        [JsonProperty("hs_tariff_number")]
        public string? HsTariffNumber { get; set; }

        /// <summary>
        ///     The two-letter ISO 3166 country code where the item is being shipped from.
        /// </summary>
        [JsonProperty("origin_country")]
        public string? OriginCountry { get; set; }

        /// <summary>
        ///     The quantity of the item being shipped.
        /// </summary>
        [JsonProperty("quantity")]
        public int? Quantity { get; set; }

        /// <summary>
        ///     The value of the item being shipped, in US Dollars.
        ///     Total value should equal unit value multiplied by the <see cref="Quantity"/>.
        /// </summary>
        [JsonProperty("value")]
        public double? Value { get; set; }

        /// <summary>
        ///     The weight of the item being shipped, in ounces.
        /// </summary>
        [JsonProperty("weight")]
        public double? Weight { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="CustomsItem"/> class.
        /// </summary>
        internal CustomsItem()
        {
        }
    }
}
