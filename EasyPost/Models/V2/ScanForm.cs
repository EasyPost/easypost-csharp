using System;
using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ScanForm : EasyPostObject
    {
        [JsonProperty("address")]
        public Address? address { get; set; }
        [JsonProperty("batch_id")]
        public string? batch_id { get; set; }

        [JsonProperty("form_file_type")]
        public string? form_file_type { get; set; }
        [JsonProperty("form_url")]
        public string? form_url { get; set; }

        [JsonProperty("message")]
        public string? message { get; set; }

        [JsonProperty("status")]
        public string? status { get; set; }
        [JsonProperty("tracking_codes")]
        public List<string>? tracking_codes { get; set; }
    }
}
