using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class BatchShipment : Resource
    {
        [JsonProperty("batch_message")]
        public string batch_message { get; set; }
        [JsonProperty("batch_status")]
        public string batch_status { get; set; }
        [JsonProperty("id")]
        public string id { get; set; }
        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }
    }
}
