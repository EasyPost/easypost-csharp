using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost pickup rate.
    /// </summary>
    public class PickupRate : Rate
    {
        #region JSON Properties

        [JsonProperty("pickup_id")]
        public string? PickupId { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="PickupRate"/> class.
        /// </summary>
        internal PickupRate()
        {
        }
    }
}
