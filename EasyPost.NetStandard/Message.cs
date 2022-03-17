using Newtonsoft.Json;

namespace EasyPost
{
    public class Message : Resource
    {
        [JsonProperty("carrier")]
        public string carrier { get; set; }
        [JsonProperty("message")]
        public string message { get; set; }
        [JsonProperty("type")]
        public string type { get; set; }
    }
}
