using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost.Models
{
    public class Verification : Resource
    {
        [JsonProperty("details")]
        public VerificationDetails details { get; set; }
        [JsonProperty("errors")]
        // TODO: handle refactor of Error
        public List<Error> errors { get; set; }
        [JsonProperty("success")]
        public bool success { get; set; }
    }
}
