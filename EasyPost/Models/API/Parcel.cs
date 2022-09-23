﻿using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Parcel : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("height")]
        public double? Height { get; set; }
        [JsonProperty("length")]
        public double? Length { get; set; }
        [JsonProperty("predefined_package")]
        public string? PredefinedPackage { get; set; }
        [JsonProperty("weight")]
        public double? Weight { get; set; }
        [JsonProperty("width")]
        public double? Width { get; set; }

        #endregion

        internal Parcel()
        {
        }
    }
}
