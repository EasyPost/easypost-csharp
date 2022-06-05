using System.Collections.Generic;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.Base
{
    public class Error : EasyPostObject
    {
        [JsonProperty("code")]
        public string? code { get; set; }
        [JsonProperty("errors")]
        public List<Error>? errors { get; set; }
        [JsonProperty("field")]
        public string? field { get; set; }
        [JsonProperty("message")]
        public string? message { get; set; }
        [JsonProperty("suggestion")]
        public string? suggestion { get; set; }
    }
}
