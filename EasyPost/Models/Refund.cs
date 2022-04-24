using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class Refund : Resource
    {
        [JsonProperty("id")]
        public string id { get; set; }

        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }

        [JsonProperty("confirmation_number")]
        public string confirmation_number { get; set; }

        [JsonProperty("status")]
        public string status { get; set; }

        [JsonProperty("carrier")]
        public string carrier { get; set; }

        [JsonProperty("shipment_id")]
        public string shipment_id { get; set; }
    }
}
