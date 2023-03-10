using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.Users
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Models.API.User.Update(Update)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Update : BaseParameters
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "user", "current_password")]
        public string? CurrentPassword { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "email")]
        public string? Email { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "name")]
        public string? Name { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "password")]
        public string? Password { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "password_confirmation")]
        public string? PasswordConfirmation { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "phone_number")]
        public string? PhoneNumber { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "recharge_amount")]
        public string? RechargeAmount { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "recharge_threshold")]
        public string? RechargeThreshold { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "secondary_recharge_amount")]
        public string? SecondaryRechargeAmount { get; set; }

        #endregion
    }
}
