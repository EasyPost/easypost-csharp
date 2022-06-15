using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Form : EasyPostObject
    {
        [JsonProperty("form_type")]
        public string? form_type { get; set; }
        [JsonProperty("form_url")]
        public string? form_url { get; set; }
        [JsonProperty("submitted_electronically")]
        public bool submitted_electronically { get; set; }
    }
}
