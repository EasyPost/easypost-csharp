﻿using Newtonsoft.Json;

namespace EasyPost
{
    public class Message : Resource
    {
        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
        [JsonProperty("carrier_account_id")]
        public string carrier_account_id { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
    }
}
