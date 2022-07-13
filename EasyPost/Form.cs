using System;
using Newtonsoft.Json;

namespace EasyPost
{
    public class Form : Resource
    {
        #region JSON Properties

        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("form_type")]
        public string form_type { get; set; }
        [JsonProperty("form_url")]
        public string form_url { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("submitted_electronically")]
        public bool submitted_electronically { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }

        #endregion
    }
}
