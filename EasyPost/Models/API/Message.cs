using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost <see cref="Shipment"/> or <see cref="Pickup"/> <a href="https://www.easypost.com/docs/api#message-object">message</a>.
    /// </summary>
    public class Message : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     The name of the carrier producing the error.
        /// </summary>
        [JsonProperty("carrier")]
        public string? Carrier { get; set; }

        /// <summary>
        ///     The account ID of the carrier producing the error.
        ///     Useful if you have multiple accounts with the same carrier.
        /// </summary>
        [JsonProperty("carrier_account_id")]
        public string? CarrierAccountId { get; set; }

        /// <summary>
        ///     The text from the carrier explaining the problem.
        /// </summary>
        [JsonProperty("message")]
        public string? Text { get; set; } // "Message" is the enclosing class name

        /// <summary>
        ///     The category of error that occurred.
        /// </summary>
        [JsonProperty("type")]
        public string? Type { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Message"/> class.
        /// </summary>
        internal Message()
        {
        }
    }
}
