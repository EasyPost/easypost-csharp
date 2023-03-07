using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.Users
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.User.UpdateBrand"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class UpdateBrand : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "brand", "ad")]
        public string? AdBase64 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "brand", "ad_href")]
        public string? AdUrl { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "brand", "background_color")]
        public string? BackgroundColorHexCode { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "brand", "color")]
        public string? ColorHexCode { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "brand", "logo")]
        public string? LogoBase64 { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "brand", "logo_href")]
        public string? LogoUrl { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "brand", "theme")]
        public string? Theme { get; set; }

        #endregion
    }
}
