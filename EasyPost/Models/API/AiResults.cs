using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     AiResults model. Represents AI prediction results for a shipment.
    /// </summary>
    public class AiResults
    {
        /// <summary>
        ///     The carrier for the rate.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     Indicates if the rate meets ruleset requirements.
        /// </summary>
        [JsonProperty("meets_ruleset_requirements")]
        public bool? MeetsRulesetRequirements { get; set; }

        /// <summary>
        ///     The predicted delivery by date (ISO 8601 format).
        /// </summary>
        [JsonProperty("predicted_deliver_by_date")]
        public string? PredictedDeliverByDate { get; set; }

        /// <summary>
        ///     The predicted number of delivery days.
        /// </summary>
        [JsonProperty("predicted_deliver_days")]
        public int? PredictedDeliverDays { get; set; }

        /// <summary>
        ///     The ID of the rate.
        /// </summary>
        [JsonProperty("rate_id")]
        public string? RateId { get; set; }

        /// <summary>
        ///     The rate in USD.
        /// </summary>
        [JsonProperty("rate_usd")]
        public string? RateUsd { get; set; }

        /// <summary>
        ///     The service level for the rate.
        /// </summary>
        [JsonProperty("service")]
        public string? Service { get; set; }
    }
}
