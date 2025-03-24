using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing a client secret to securely collect credit card details.
    /// </summary>
    public class StripeClientSecret
    {
        #region JSON Properties

        /// <summary>
        ///     The client secret for Stripe billing.
        /// </summary>
        [JsonProperty("client_secret")]
        public string? ClientSecret { get; set; }

        #endregion
    }
}
