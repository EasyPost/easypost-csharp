using Newtonsoft.Json;

namespace EasyPost
{
    public class PickupRate : Rate
    {
        [JsonProperty("pickup_id")]
        public string pickup_id { get; set; }

        /// <summary>
        ///     Convert this PickupRate object to a Rate object.
        /// </summary>
        /// <returns>An EasyPost.Rate object instance.</returns>
        internal Rate AsRate()
        {
            return (Rate)this;
        }
    }
}
