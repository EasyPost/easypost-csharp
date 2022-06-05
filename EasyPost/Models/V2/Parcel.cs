using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Parcel : EasyPostObject
    {
        [JsonProperty("height")]
        public double? height { get; set; }

        [JsonProperty("length")]
        public double? length { get; set; }

        [JsonProperty("predefined_package")]
        public string? predefined_package { get; set; }

        [JsonProperty("weight")]
        public double weight { get; set; }
        [JsonProperty("width")]
        public double? width { get; set; }
    }
}
