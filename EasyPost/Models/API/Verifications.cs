using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Verifications : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("delivery")]
        public Verification? Delivery { get; set; }
        [JsonProperty("zip4")]
        public Verification? Zip4 { get; set; }

        #endregion
    }
}
