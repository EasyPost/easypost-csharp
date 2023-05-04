using System.Collections.Generic;
using System.Linq;
using EasyPost._base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyPost.Models.API
{
#pragma warning disable CA1716
    /// <summary>
    ///     Represents an error returned by the EasyPost API.
    ///     These are typically informational about why a request failed (server-side validation issues, missing data, etc.).
    ///     This is different than the EasyPostError class, which represents exceptions in the EasyPost library,
    ///     such as bad HTTP status codes or local validation issues.
    /// </summary>
    public class Error : EasyPostObject
#pragma warning restore CA1716
    {
        #region JSON Properties

        [JsonProperty("code")]
        public string? Code { get; internal set; }
        [JsonProperty("errors")]
        public List<Error>? Errors { get; internal set; }
        [JsonProperty("field")]
        public string? Field { get; internal set; }

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

        [JsonProperty("suggestion")]
        public string? Suggestion { get; internal set; }

        /// <summary>
        ///     The "message" is most likely a string, but it can also be a dictionary or a list from the API.
        ///     This field is specifically private, hidden from the user, and used to deserialize the message to a string when accessed by the public Message property.
        /// </summary>
        [JsonProperty("message")]
        internal object? RawMessage { get; set; }

        #endregion

        internal Error()
        {
        }

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
