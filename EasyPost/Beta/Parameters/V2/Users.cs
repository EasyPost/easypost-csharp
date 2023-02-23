using EasyPost.Utilities.Annotations;

namespace EasyPost.Beta.Parameters.V2
{
    public static class Users
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.User"/> update API calls.
        /// </summary>
        public sealed class Update : UpdateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "user", "current_password")]
            public string? CurrentPassword { get; set; }

            [RequestParameter(Necessity.Optional, "user", "email")]
            public string? Email { get; set; }

            [RequestParameter(Necessity.Optional, "user", "name")]
            public string? Name { get; set; }

            [RequestParameter(Necessity.Optional, "user", "password")]
            public string? Password { get; set; }

            [RequestParameter(Necessity.Optional, "user", "password_confirmation")]
            public string? PasswordConfirmation { get; set; }

            [RequestParameter(Necessity.Optional, "user", "phone_number")]
            public string? PhoneNumber { get; set; }

            [RequestParameter(Necessity.Optional, "user", "recharge_amount")]
            public string? RechargeAmount { get; set; }

            [RequestParameter(Necessity.Optional, "user", "recharge_threshold")]
            public string? RechargeThreshold { get; set; }

            [RequestParameter(Necessity.Optional, "user", "secondary_recharge_amount")]
            public string? SecondaryRechargeAmount { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.User"/> brand update API calls.
        /// </summary>
        public sealed class UpdateBrand : RequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "brand", "ad")]
            public string? AdBase64 { get; set; }

            [RequestParameter(Necessity.Optional, "brand", "ad_href")]
            public string? AdUrl { get; set; }

            [RequestParameter(Necessity.Optional, "brand", "background_color")]
            public string? BackgroundColorHexCode { get; set; }

            [RequestParameter(Necessity.Optional, "brand", "color")]
            public string? ColorHexCode { get; set; }

            [RequestParameter(Necessity.Optional, "brand", "logo")]
            public string? LogoBase64 { get; set; }

            [RequestParameter(Necessity.Optional, "brand", "logo_href")]
            public string? LogoUrl { get; set; }

            [RequestParameter(Necessity.Optional, "brand", "theme")]
            public string? Theme { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.UserService.CreateChild"/> API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "user", "name")]
            public string? Name { get; set; }

            #endregion
        }
    }
}
