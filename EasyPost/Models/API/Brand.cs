using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    public class Brand : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("ad")]
        public string? Ad { get; internal set; }
        [JsonProperty("ad_href")]
        public string? AdHref { get; internal set; }
        [JsonProperty("background_color")]
        public string? BackgroundColor { get; internal set; }
        [JsonProperty("color")]
        public string? Color { get; internal set; }
        [JsonProperty("logo")]
        public string? Logo { get; internal set; }
        [JsonProperty("logo_href")]
        public string? LogoHref { get; internal set; }
        [JsonProperty("name")]
        public string? Name { get; internal set; }
        [JsonProperty("theme")]
        public string? Theme { get; internal set; }
        [JsonProperty("user_id")]
        public string? UserId { get; internal set; }

        #endregion

        internal Brand()
        {
        }
    }
}
