using System.Collections.Generic;
using EasyPost.Interfaces;
using EasyPost.Models.Base;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Verification : EasyPostObject
    {
        [JsonProperty("details")]
        public VerificationDetails? Details { get; set; }
        [JsonProperty("errors")]
        // TODO: handle refactor of Error
        public List<Error>? Errors { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }
    }
}
