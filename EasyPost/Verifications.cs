using Newtonsoft.Json;

namespace EasyPost
{
    public class Verifications : Resource
    {
        #region JSON Properties

        [JsonProperty("delivery")]
        public Verification delivery { get; set; }
        [JsonProperty("zip4")]
        public Verification zip4 { get; set; }

        #endregion
    }
}
