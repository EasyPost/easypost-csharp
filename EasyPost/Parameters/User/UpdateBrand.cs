using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.User
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#update-a-brand">Parameters</a> for <see cref="EasyPost.Services.UserService.UpdateBrand(string, UpdateBrand, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class UpdateBrand : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     The base64 encoded image for the <see cref="Models.API.Brand"/>'s ad.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "brand", "ad")]
        public string? AdBase64 { get; set; }

        /// <summary>
        ///     The URL for the <see cref="Models.API.Brand"/>'s ad.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "brand", "ad_href")]
        public string? AdUrl { get; set; }

        /// <summary>
        ///     The background color for the <see cref="Models.API.Brand"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "brand", "background_color")]
        public string? BackgroundColorHexCode { get; set; }

        /// <summary>
        ///     The base64 encoded image for the <see cref="Models.API.Brand"/>'s banner.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "brand", "color")]
        public string? ColorHexCode { get; set; }

        /// <summary>
        ///    The base64 encoded image for the <see cref="Models.API.Brand"/>'s logo.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "brand", "logo")]
        public string? LogoBase64 { get; set; }

        /// <summary>
        ///     The URL for the <see cref="Models.API.Brand"/>'s logo.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "brand", "logo_href")]
        public string? LogoUrl { get; set; }

        /// <summary>
        ///     Save this configuration as a preset for future use (<c>"theme1"</c> or <c>"theme2"</c>).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "brand", "theme")]
        public string? Theme { get; set; }

        #endregion
    }
}
