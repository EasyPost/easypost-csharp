using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Form : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("form_type")]
        public string? FormType { get; internal set; }
        [JsonProperty("form_url")]
        public string? FormUrl { get; internal set; }
        [JsonProperty("submitted_electronically")]
        public bool? SubmittedElectronically { get; internal set; }

        #endregion

        internal Form()
        {
        }
    }
}
