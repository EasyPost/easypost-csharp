using EasyPost.Interfaces;
using EasyPost.Utilities;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class VerificationDetails : EasyPostObject
    {
        [JsonProperty("latitude")]
        public float latitude { get; set; }
        [JsonProperty("longitude")]
        public float longitude { get; set; }
        [JsonProperty("time_zone")]
        public string? time_zone { get; set; }
    }
}
