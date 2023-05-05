using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost verification object.
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

        /// <summary>
        ///     Initializes a new instance of the <see cref="Verification"/> class.
        /// </summary>
        internal Verification()
        {
        }
    }
}
