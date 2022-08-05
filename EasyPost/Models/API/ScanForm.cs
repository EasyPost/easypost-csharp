using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class ScanForm : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("address")]
        public Address address { get; set; }
        [JsonProperty("batch_id")]
        public string batch_id { get; set; }

        [JsonProperty("form_file_type")]
        public string form_file_type { get; set; }
        [JsonProperty("form_url")]
        public string form_url { get; set; }

        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("status")]
        public string status { get; set; }
        [JsonProperty("tracking_codes")]
        public List<string> tracking_codes { get; set; }

        #endregion
    }
}
