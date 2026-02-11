using System.Collections.Generic;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Represents a FedEx account validation response.
    /// </summary>
    public class FedExAccountValidationResponse
    {
        #region JSON Properties

        /// <summary>
        ///     Gets or sets the email address for PIN delivery.
        /// </summary>
        [JsonProperty("email_address")]
        public string? EmailAddress { get; set; }

        /// <summary>
        ///     Gets or sets the available PIN delivery options.
        /// </summary>
        [JsonProperty("options")]
        public List<string>? Options { get; set; }

        /// <summary>
        ///     Gets or sets the phone number for PIN delivery.
        /// </summary>
        [JsonProperty("phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        ///     Gets or sets the ID.
        /// </summary>
        [JsonProperty("id")]
        public string? Id { get; set; }

        /// <summary>
        ///     Gets or sets the object type.
        /// </summary>
        [JsonProperty("object")]
        public string? ObjectType { get; set; }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        /// <summary>
        ///     Gets or sets the credentials.
        /// </summary>
        [JsonProperty("credentials")]
        public Dictionary<string, string>? Credentials { get; set; }

        #endregion
    }
}
