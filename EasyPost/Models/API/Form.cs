using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Form : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("form_type")]
        public string? FormType { get; set; }
        [JsonProperty("form_url")]
        public string? FormUrl { get; set; }
        [JsonProperty("submitted_electronically")]
        public bool? SubmittedElectronically { get; set; }

        #endregion
    }
}
