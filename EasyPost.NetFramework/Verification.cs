using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost
{
    public class Verification : Resource
    {
        [JsonProperty("details")]
        public List<VerificationDetails> details { get; set; }
        [JsonProperty("errors")]
        public List<Error> errors { get; set; }
        [JsonProperty("success")]
        public bool success { get; set; }
    }
}
