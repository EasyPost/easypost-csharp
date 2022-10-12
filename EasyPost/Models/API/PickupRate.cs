using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class PickupRate : Rate
    {
        #region JSON Properties

        [JsonProperty("pickup_id")]
        public string? PickupId { get; set; }

        #endregion

        internal PickupRate()
        {
        }
    }
}
