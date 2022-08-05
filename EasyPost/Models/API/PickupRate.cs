using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class PickupRate : Rate
    {
        #region JSON Properties

        [JsonProperty("pickup_id")]
        public string pickup_id { get; set; }

        #endregion

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
