using EasyPost._base;
using EasyPost.Utilities.Internal;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#time%20in%20transit-object">EasyPost time in transit summary</a>.
    /// </summary>
    public class TimeInTransit : EphemeralEasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     Expected transit days at the 50th percentile.
        /// </summary>
        [JsonProperty("percentile_50")]
        public int? Percentile50 { get; set; }

        /// <summary>
        ///     Expected transit days at the 75th percentile.
        /// </summary>
        [JsonProperty("percentile_75")]
        public int? Percentile75 { get; set; }

        /// <summary>
        ///     Expected transit days at the 85th percentile.
        /// </summary>
        [JsonProperty("percentile_85")]
        public int? Percentile85 { get; set; }

        /// <summary>
        ///     Expected transit days at the 90th percentile.
        /// </summary>
        [JsonProperty("percentile_90")]
        public int? Percentile90 { get; set; }

        /// <summary>
        ///     Expected transit days at the 95th percentile.
        /// </summary>
        [JsonProperty("percentile_95")]
        public int? Percentile95 { get; set; }

        /// <summary>
        ///     Expected transit days at the 97th percentile.
        /// </summary>
        [JsonProperty("percentile_97")]
        public int? Percentile97 { get; set; }

        /// <summary>
        ///     Expected transit days at the 99th percentile.
        /// </summary>
        [JsonProperty("percentile_99")]
        public int? Percentile99 { get; set; }

        #endregion

        /// <summary>
        ///     Get the value of a specific percentile by its corresponding SmartRateAccuracy enum.
        /// </summary>
        /// <param name="accuracy">SmartRateAccuracy enum to find associated value for.</param>
        /// <returns>Corresponding percentile int value.</returns>
        public int? GetBySmartRateAccuracy(SmartRateAccuracy accuracy)
        {
            int? accuracyInt = null;
            SwitchCase @switch = new()
            {
                { accuracy.Equals(SmartRateAccuracy.Percentile50), () => { accuracyInt = Percentile50; } },
                { accuracy.Equals(SmartRateAccuracy.Percentile75), () => { accuracyInt = Percentile75; } },
                { accuracy.Equals(SmartRateAccuracy.Percentile85), () => { accuracyInt = Percentile85; } },
                { accuracy.Equals(SmartRateAccuracy.Percentile90), () => { accuracyInt = Percentile90; } },
                { accuracy.Equals(SmartRateAccuracy.Percentile95), () => { accuracyInt = Percentile95; } },
                { accuracy.Equals(SmartRateAccuracy.Percentile97), () => { accuracyInt = Percentile97; } },
                { accuracy.Equals(SmartRateAccuracy.Percentile99), () => { accuracyInt = Percentile99; } },
                { SwitchCaseScenario.Default, () => { accuracyInt = null; } },
            };
            @switch.MatchFirst(true); // evaluate switch case, checking which expression evaluates to "true"

            return accuracyInt;
        }
    }
}
