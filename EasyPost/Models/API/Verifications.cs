using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Verifications : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("delivery")]
        public Verification delivery { get; set; }
        [JsonProperty("zip4")]
        public Verification zip4 { get; set; }

        #endregion
    }
}
