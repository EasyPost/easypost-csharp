using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EasyPost.Http;
using EasyPost.Interfaces;
using Newtonsoft.Json;
using RestSharp;

namespace EasyPost.Models
{
    public class ApiKey : Resource
    {
        [JsonProperty("created_at")]
        public DateTime? created_at { get; set; }
        [JsonProperty("key")]
        public string key { get; set; }
        [JsonProperty("mode")]
        public string mode { get; set; }
    }
}
