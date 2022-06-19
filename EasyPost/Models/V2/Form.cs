using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Form : EasyPostObject
    {
        [JsonProperty("form_type")]
        public string? FormType { get; set; }
        [JsonProperty("form_url")]
        public string? FormUrl { get; set; }
        [JsonProperty("submitted_electronically")]
        public bool SubmittedElectronically { get; set; }
    }
}
