using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Verification : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("details")]
        public VerificationDetails details { get; set; }
        [JsonProperty("errors")]
        // TODO: handle refactor of Error
        public List<Error> errors { get; set; }
        [JsonProperty("success")]
        public bool success { get; set; }

        #endregion
    }
}
