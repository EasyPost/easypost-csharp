using Newtonsoft.Json;

namespace EasyPost.Models
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
    }
}
