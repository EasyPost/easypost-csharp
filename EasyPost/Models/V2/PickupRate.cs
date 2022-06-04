using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class PickupRate : Rate
    {
        [JsonProperty("pickup_id")]
        public string? pickup_id { get; set; }
    }
}
