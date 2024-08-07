using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing a surcharge for a <see cref="Rate"/> object.
    /// </summary>
    public class Surcharge : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The type of surcharge.
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        ///     The amount of the surcharge.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Amount { get; set; }

        /// <summary>
        ///     The list amount of the surcharge.
        /// </summary>
        [JsonProperty("list_amount")]
        public string? ListAmount { get; set; }

        /// <summary>
        ///     The retail amount of the surcharge.
        /// </summary>
        [JsonProperty("retail_amount")]
        public string? RetailAmount { get; set; }

        /// <summary>
        ///     The currency for the surcharge.
        /// </summary>
        [JsonProperty("currency")]
        public string? Currency { get; set; }

        #endregion
    }
}
