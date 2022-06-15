using System;
using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Event : EasyPostObject
    {
        [JsonProperty("completed_urls")]
        public List<string>? completed_urls { get; set; }
        [JsonProperty("description")]
        public string? description { get; set; }
        [JsonProperty("pending_urls")]
        public List<string>? pending_urls { get; set; }
        [JsonProperty("previous_attributes")]
        public Dictionary<string, object>? previous_attributes { get; set; }
        [JsonProperty("result")]
        public Dictionary<string, object>? result { get; set; }
        [JsonProperty("status")]
        public string? status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
    }
}
