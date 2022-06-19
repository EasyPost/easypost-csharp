using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class ApiKey : EasyPostObject
    {
        [JsonProperty("key")]
        public string? Key { get; set; }
    }
}
