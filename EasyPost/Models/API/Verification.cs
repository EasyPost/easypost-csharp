using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#verification-object">EasyPost verification object</a>.
    /// </summary>
    public class Verification : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("details")]
        public VerificationDetails? Details { get; set; }
        [JsonProperty("errors")]
        public List<Error>? Errors { get; set; }
        [JsonProperty("success")]
        public bool? Success { get; set; }

        #endregion

    }
}
