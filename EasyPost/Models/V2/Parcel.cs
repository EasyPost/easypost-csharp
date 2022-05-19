using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Parcel : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("height")]
        public double? height { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("length")]
        public double? length { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
        [JsonProperty("predefined_package")]
        public string predefined_package { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? updated_at { get; set; }
        [JsonProperty("weight")]
        public double weight { get; set; }
        [JsonProperty("width")]
        public double? width { get; set; }
    }
}
