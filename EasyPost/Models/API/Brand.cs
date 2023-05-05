using EasyPost._base;
using Newtonsoft.Json;

namespace EasyPost.Models.API
{
    /// <summary>
    ///     Class representing an EasyPost brand.
    /// </summary>
    public class Brand : EasyPostObject
    {
        #region JSON Properties

        /// <summary>
        ///     A base64-encoded string for a PNG, JPEG, GIF or SVG image.
        /// </summary>
        [JsonProperty("ad")]
        public string? Ad { get; set; }

        /// <summary>
        ///     A URL to link to when the ad is clicked.
        ///     Maximum length is 255 characters.
        /// </summary>
        [JsonProperty("ad_href")]
        public string? AdHref { get; set; }

        /// <summary>
        ///    A hex code for the background color of the brand.
        /// </summary>
        [JsonProperty("background_color")]
        public string? BackgroundColor { get; set; }

        /// <summary>
        ///     A hex code for the color of the brand.
        /// </summary>
        [JsonProperty("color")]
        public string? Color { get; set; }

        /// <summary>
        ///     A base64-encoded string for a PNG, JPEG, GIF or SVG image.
        /// </summary>
        [JsonProperty("logo")]
        public string? Logo { get; set; }

        /// <summary>
        ///     A URL to link to when the logo is clicked.
        /// </summary>
        [JsonProperty("logo_href")]
        public string? LogoHref { get; set; }

        /// <summary>
        ///     The name of the brand associated with the <see cref="User"/>.
        /// </summary>
        [JsonProperty("name")]
        public string? Name { get; set; }

        /// <summary>
        ///     A name to store this brand preset as.
        ///     Either "theme1" or "theme2".
        /// </summary>
        [JsonProperty("theme")]
        public string? Theme { get; set; }

        /// <summary>
        ///     The ID of the <see cref="User"/> associated with this brand.
        /// </summary>
        [JsonProperty("user_id")]
        public string? UserId { get; set; }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="Brand"/> class.
        /// </summary>
        internal Brand()
        {
        }
    }
}
