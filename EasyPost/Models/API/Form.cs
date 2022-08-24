using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Form : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("form_type")]
        public string form_type { get; set; }
        [JsonProperty("form_url")]
        public string form_url { get; set; }
        [JsonProperty("submitted_electronically")]
        public bool submitted_electronically { get; set; }

        #endregion
    }
}
