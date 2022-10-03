using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class TimeInTransit
    {
        #region JSON Properties

        [JsonProperty("percentile_50")]
        public int? Percentile50 { get; set; }
        [JsonProperty("percentile_75")]
        public int? Percentile75 { get; set; }
        [JsonProperty("percentile_85")]
        public int? Percentile85 { get; set; }
        [JsonProperty("percentile_90")]
        public int? Percentile90 { get; set; }
        [JsonProperty("percentile_95")]
        public int? Percentile95 { get; set; }
        [JsonProperty("percentile_97")]
        public int? Percentile97 { get; set; }
        [JsonProperty("percentile_99")]
        public int? Percentile99 { get; set; }

        #endregion

        internal TimeInTransit()
        {
        }

        /// <summary>
        ///     Get the value of a specific percentile by its corresponding SmartrateAccuracy enum.
        /// </summary>
        /// <param name="accuracy">SmartrateAccuracy enum to find associated value for.</param>
        /// <returns>Corresponding percentile int value.</returns>
        public int? GetBySmartrateAccuracy(SmartrateAccuracy accuracy)
        {
            if (accuracy.Equals(SmartrateAccuracy.Percentile50))
                return Percentile50;
            if (accuracy.Equals(SmartrateAccuracy.Percentile75))
                return Percentile75;
            if (accuracy.Equals(SmartrateAccuracy.Percentile85))
                return Percentile85;
            if (accuracy.Equals(SmartrateAccuracy.Percentile90))
                return Percentile90;
            if (accuracy.Equals(SmartrateAccuracy.Percentile95))
                return Percentile95;
            if (accuracy.Equals(SmartrateAccuracy.Percentile97))
                return Percentile97;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (accuracy.Equals(SmartrateAccuracy.Percentile99))
                return Percentile99;
            return null;
        }
    }
}
