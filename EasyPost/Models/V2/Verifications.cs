using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Verifications : EasyPostObject
    {
        [JsonProperty("delivery")]
        public Verification? delivery { get; set; }
        [JsonProperty("zip4")]
        public Verification? zip4 { get; set; }
    }
}
