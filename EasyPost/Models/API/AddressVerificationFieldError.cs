using Newtonsoft.Json;

namespace EasyPost.Models.API
{
#pragma warning disable CA1716
    /// <summary>
    ///     Represents an <a href="https://docs.easypost.com/docs/errors#fielderror-object">error returned by the EasyPost API</a>.
    ///     These are typically informational about why a request failed (server-side validation issues, missing data, etc.).
    ///     This is different than the EasyPostError class, which represents exceptions in the EasyPost library,
    ///     such as bad HTTP status codes or local validation issues.
    /// </summary>
    public class AddressVerificationFieldError
#pragma warning restore CA1716
    {
        #region JSON Properties

        /// <summary>
        ///     A machine-readable description of the problem encountered.
        /// </summary>
        [JsonProperty("code")]
        public string? Code { get; set; }

        /// <summary>
        ///     The field of the request that caused the error.
        /// </summary>
        [JsonProperty("field")]
        public string? Field { get; set; }

        /// <summary>
        ///     A human-readable description of the problem encountered.
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        /// <summary>
        ///     A human-readable suggestion to resolve the problem encountered.
        /// </summary>
        [JsonProperty("suggestion")]
        public string? Suggestion { get; set; }

        #endregion
    }
}
