using System.Collections.Generic;
using EasyPost._base;
using EasyPost.Models.Shared;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Verification : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("details")]
        public VerificationDetails? Details { get; set; }
        [JsonProperty("errors")]
        // TODO: handle refactor of Error
        public List<Error>? Errors { get; set; }
        [JsonProperty("success")]
        public bool Success { get; set; }

        #endregion
    }
}
