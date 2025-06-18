using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     LumaInfo model. Represents Luma information including AI results and selected rate.
    /// </summary>
    public class LumaInfo
    {
        /// <summary>
        ///     List of AI prediction results.
        /// </summary>
        [JsonProperty("ai_results")]
        public List<AiResults>? AiResults { get; set; }

        /// <summary>
        ///     Index of the matching rule.
        /// </summary>
        [JsonProperty("matching_rule_idx")]
        public int? MatchingRuleIdx { get; set; }

        /// <summary>
        ///     Description of the ruleset.
        /// </summary>
        [JsonProperty("ruleset_description")]
        public string? RulesetDescription { get; set; }

        /// <summary>
        ///     The Luma-selected rate.
        /// </summary>
        [JsonProperty("luma_selected_rate")]
        public Rate? LumaSelectedRate { get; set; }
    }
}
