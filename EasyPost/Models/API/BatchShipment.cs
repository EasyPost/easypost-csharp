using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class BatchShipment : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("batch_message")]
        public string? BatchMessage { get; internal set; }
        [JsonProperty("batch_status")]
        public string? BatchStatus { get; internal set; }
        [JsonProperty("tracking_code")]
        public string? TrackingCode { get; internal set; }

        #endregion

        internal BatchShipment()
        {
        }
    }
}
