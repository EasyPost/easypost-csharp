using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#carbon%20offset-object">EasyPost carbon offset object</a>.
    /// </summary>
    public class CarbonOffset : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The currency of the price.
        ///     Currently only "USD" is supported.
        /// </summary>
        [JsonProperty("currency")]
        public string? Currency { get; set; }

        /// <summary>
        ///     The estimated amount of carbon grams emitted by the shipment.
        /// </summary>
        [JsonProperty("grams")]
        public int? Grams { get; set; }

        /// <summary>
        ///     The price to offset the <see cref="Grams"/>.
        /// </summary>
        [JsonProperty("price")]
        public string? Price { get; set; }

        #endregion
    }
}
