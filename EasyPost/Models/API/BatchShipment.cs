using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class BatchShipment : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("batch_message")]
        public string batch_message { get; set; }
        [JsonProperty("batch_status")]
        public string batch_status { get; set; }
        [JsonProperty("tracking_code")]
        public string tracking_code { get; set; }

        #endregion
    }
}
