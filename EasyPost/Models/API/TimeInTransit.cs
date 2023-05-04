using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TimeInTransit : EphemeralEasyPostObject
    {
        #region JSON Properties

        [JsonProperty("percentile_50")]
        public int? Percentile50 { get; internal set; }
        [JsonProperty("percentile_75")]
        public int? Percentile75 { get; internal set; }
        [JsonProperty("percentile_85")]
        public int? Percentile85 { get; internal set; }
        [JsonProperty("percentile_90")]
        public int? Percentile90 { get; internal set; }
        [JsonProperty("percentile_95")]
        public int? Percentile95 { get; internal set; }
        [JsonProperty("percentile_97")]
        public int? Percentile97 { get; internal set; }
        [JsonProperty("percentile_99")]
        public int? Percentile99 { get; internal set; }

        #endregion

        internal TimeInTransit()
        {
        }

        /// <summary>
        ///     Get the value of a specific percentile by its corresponding SmartRateAccuracy enum.
        /// </summary>
        /// <param name="accuracy">SmartRateAccuracy enum to find associated value for.</param>
        /// <returns>Corresponding percentile int value.</returns>
        public int? GetBySmartRateAccuracy(SmartRateAccuracy accuracy)
        {
            if (accuracy.Equals(SmartRateAccuracy.Percentile50))
            {
                return Percentile50;
            }

            if (accuracy.Equals(SmartRateAccuracy.Percentile75))
            {
                return Percentile75;
            }

            if (accuracy.Equals(SmartRateAccuracy.Percentile85))
            {
                return Percentile85;
            }

            if (accuracy.Equals(SmartRateAccuracy.Percentile90))
            {
                return Percentile90;
            }

            if (accuracy.Equals(SmartRateAccuracy.Percentile95))
            {
                return Percentile95;
            }

            if (accuracy.Equals(SmartRateAccuracy.Percentile97))
            {
                return Percentile97;
            }

            // ReSharper disable once ConvertIfStatementToReturnStatement
#pragma warning disable IDE0046
            if (accuracy.Equals(SmartRateAccuracy.Percentile99))
#pragma warning restore IDE0046
            {
                return Percentile99;
            }

            return null;
        }
    }
}
