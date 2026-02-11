using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Represents a FedEx request PIN response.
    /// </summary>
    public class FedExRequestPinResponse
    {
        #region JSON Properties

        /// <summary>
        ///     Gets or sets the message.
        /// </summary>
        [JsonProperty("message")]
        public string? Message { get; set; }

        #endregion
    }
}
