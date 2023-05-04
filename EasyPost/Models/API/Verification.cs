using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Verification : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("details")]
        public VerificationDetails? Details { get; internal set; }
        [JsonProperty("errors")]
        public List<Error>? Errors { get; internal set; }
        [JsonProperty("success")]
        public bool? Success { get; internal set; }

        #endregion

        internal Verification()
        {
        }
    }
}
