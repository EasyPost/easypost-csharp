using EasyPost.Interfaces;
using Newtonsoft.Json;

namespace EasyPost.Models.V2
{
    public class Brand : EasyPostObject
    {
        #region JSON Properties

        [JsonProperty("ad")]
        public string? Ad { get; set; }
        [JsonProperty("ad_href")]
        public string? AdHref { get; set; }
        [JsonProperty("background_color")]
        public string? BackgroundColor { get; set; }
        [JsonProperty("color")]
        public string? Color { get; set; }
        [JsonProperty("logo")]
        public string? Logo { get; set; }
        [JsonProperty("logo_href")]
        public string? LogoHref { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("theme")]
        public string? Theme { get; set; }
        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        #endregion
    }
}
