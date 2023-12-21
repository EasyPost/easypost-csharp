using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an <a href="https://www.easypost.com/docs/api#pickup-rate-object">EasyPost pickup rate</a>.
    /// </summary>
    public class PickupRate : Rate
    {
        #region JSON Properties

        /// <summary>
        ///     The ID of the <see cref="EasyPost.Models.API.Pickup"/> associated with this PickupRate.
        /// </summary>
        [JsonProperty("pickup_id")]
        public string? PickupId { get; set; }

        #endregion

    }
}
