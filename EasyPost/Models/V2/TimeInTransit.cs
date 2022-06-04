using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class TimeInTransit
    {
        [JsonProperty("percentile_50")]
        public int? percentile_50 { get; set; }
        [JsonProperty("percentile_75")]
        public int? percentile_75 { get; set; }
        [JsonProperty("percentile_85")]
        public int? percentile_85 { get; set; }
        [JsonProperty("percentile_90")]
        public int? percentile_90 { get; set; }
        [JsonProperty("percentile_95")]
        public int? percentile_95 { get; set; }
        [JsonProperty("percentile_97")]
        public int? percentile_97 { get; set; }
        [JsonProperty("percentile_99")]
        public int? percentile_99 { get; set; }

        /// <summary>
        ///     Get the value of a specific percentile by its corresponding SmartrateAccuracy enum.
        /// </summary>
        /// <param name="accuracy">SmartrateAccuracy enum to find associated value for.</param>
        /// <returns>Corresponding percentile int value.</returns>
        public int? GetBySmartrateAccuracy(SmartrateAccuracy accuracy)
        {
            if (accuracy.Equals(SmartrateAccuracy.Percentile50))
                return percentile_50;
            if (accuracy.Equals(SmartrateAccuracy.Percentile75))
                return percentile_75;
            if (accuracy.Equals(SmartrateAccuracy.Percentile85))
                return percentile_85;
            if (accuracy.Equals(SmartrateAccuracy.Percentile90))
                return percentile_90;
            if (accuracy.Equals(SmartrateAccuracy.Percentile95))
                return percentile_95;
            if (accuracy.Equals(SmartrateAccuracy.Percentile97))
                return percentile_97;
            // ReSharper disable once ConvertIfStatementToReturnStatement
            if (accuracy.Equals(SmartrateAccuracy.Percentile99))
                return percentile_99;
            return null;
        }
    }
}
