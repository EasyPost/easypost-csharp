using System;
using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Event : EasyPostObject
    {
        [JsonProperty("completed_urls")]
        public List<string>? CompletedUrls { get; set; }
        [JsonProperty("description")]
        public string? Description { get; set; }
        [JsonProperty("pending_urls")]
        public List<string>? PendingUrls { get; set; }
        [JsonProperty("previous_attributes")]
        public Dictionary<string, object>? PreviousAttributes { get; set; }
        [JsonProperty("result")]
        public Dictionary<string, object>? Result { get; set; }
        [JsonProperty("status")]
        public string? Status { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
