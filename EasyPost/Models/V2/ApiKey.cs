using System;
using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ApiKey : EasyPostObject
    {
        [JsonProperty("key")]
        public string? key { get; set; }

    }
}
