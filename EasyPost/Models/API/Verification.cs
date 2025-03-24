using System.Collections.Generic;
using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://docs.easypost.com/docs/addresses#verification-object">EasyPost verification object</a>.
    /// </summary>
    public class Verification : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     Extra details related to the verification.
        /// </summary>
        [JsonProperty("details")]
        public VerificationDetails? Details { get; set; }

        /// <summary>
        ///     A list of errors encountered during verification.
        /// </summary>
        [JsonProperty("errors")]
        public List<AddressVerificationFieldError>? Errors { get; set; }

        /// <summary>
        ///     Whether the verification was successful.
        /// </summary>
        [JsonProperty("success")]
        public bool? Success { get; set; }

        #endregion

    }
}
