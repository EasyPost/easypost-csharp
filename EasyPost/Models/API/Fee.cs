using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/fees#fee-object">EasyPost fee</a>.
    /// </summary>
    public class Fee : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The amount of the fee, in US cents with sub-cent precision.
        /// </summary>
        [JsonProperty("amount")]
        public double? Amount { get; set; }

        /// <summary>
        ///     Whether EasyPost has successfully charged your account for the fee.
        /// </summary>
        [JsonProperty("charged")]
        public bool? Charged { get; set; }

        /// <summary>
        ///     Whether the fee has been refunded successfully.
        /// </summary>
        [JsonProperty("refunded")]
        public bool? Refunded { get; set; }

        /// <summary>
        ///     The category of the fee.
        ///     Possible types include "LabelFee", "PostageFee", "InsuranceFee" and "TrackerFee".
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

    }
}
