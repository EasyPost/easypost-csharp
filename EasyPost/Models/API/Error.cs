using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyPost.Models.API
{
#pragma warning disable CA1716
    /// <summary>
    ///     Represents an <a href="https://docs.easypost.com/docs/errors#error-object">error returned by the EasyPost API</a>.
    ///     These are typically informational about why a request failed (server-side validation issues, missing data, etc.).
    ///     This is different than the EasyPostError class, which represents exceptions in the EasyPost library,
    ///     such as bad HTTP status codes or local validation issues.
    /// </summary>
    public class Error : EasyPostObject
#pragma warning restore CA1716
    {
        #region JSON Properties

        /// <summary>
        ///     A machine-readable description of the problem encountered.
        /// </summary>
        [JsonProperty("code")]
        public string? Code { get; set; }

        /// <summary>
        ///     A breakdown of errors encountered for specific fields in the request.
        /// </summary>
        [JsonProperty("errors")]
        public List<Error>? Errors { get; set; }

        /// <summary>
        ///     The field of the request that caused the error.
        /// </summary>
        [JsonProperty("field")]
        public string? Field { get; set; }

        /// <summary>
        ///     A human-readable description of the problem encountered.
        /// </summary>
        [JsonIgnore]
        public string? Message
        {
            // This parses the message from the API response and returns it as a string regardless of what type it actually is.
            get
            {
                ICollection<string> messages = CollectErrorMessages(RawMessage, new List<string>());
                return messages.Count switch
                {
                    0 => null,
                    1 => messages.First(),
                    var _ => string.Join(", ", messages),
                };
            }
        }

        /// <summary>
        ///     A human-readable suggestion to resolve the problem encountered.
        /// </summary>
        [JsonProperty("suggestion")]
        public string? Suggestion { get; set; }

        /// <summary>
        ///     The "message" is most likely a string, but it can also be a dictionary or a list from the API.
        ///     This field is specifically private, hidden from the user, and used to deserialize the message to a string when accessed by the public Message property.
        /// </summary>
        [JsonProperty("message")]
        internal object? RawMessage { get; set; }

        #endregion

        /// <summary>
        ///     Traverse the returned element for error messages.
        ///     This will handle potential inconsistent data structures in EasyPost error messages.
        /// </summary>
        /// <param name="element">The current element to traverse.</param>
        /// <param name="collectedMessages">Previously-collected error messages.</param>
        /// <returns>A collection of error message strings.</returns>
        private static ICollection<string> CollectErrorMessages(object? element, ICollection<string> collectedMessages)
        {
            switch (element)
            {
                case null:
                    break;
                case JArray array:
                    foreach (JToken item in array)
                    {
                        collectedMessages = CollectErrorMessages(item, collectedMessages);
                    }

                    break;
                case JObject obj:
                    foreach (JProperty property in obj.Properties())
                    {
                        collectedMessages = CollectErrorMessages(property.Value, collectedMessages);
                    }

                    break;
                default:
                    string? asString = element.ToString();
                    if (!string.IsNullOrWhiteSpace(asString))
                        collectedMessages.Add(asString);
                    break;
            }

            return collectedMessages;
        }
    }
}
