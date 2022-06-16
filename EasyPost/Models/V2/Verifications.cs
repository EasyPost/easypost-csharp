using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Verifications : EasyPostObject
    {
        [JsonProperty("delivery")]
        public Verification? Delivery { get; set; }
        [JsonProperty("zip4")]
        public Verification? Zip4 { get; set; }
    }
}
