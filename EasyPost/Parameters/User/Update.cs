using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.User
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#update-a-user">Parameters</a> for <see cref="EasyPost.Services.UserService.Update(string, Update, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class Update : BaseParameters
    {
        #region Request Parameters

        /// <summary>
        ///     The current password for the <see cref="Models.API.User"/>. Required if <see cref="Email"/> or <see cref="Password"/> are being updated.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "current_password")]
        public string? CurrentPassword { get; set; }

        /// <summary>
        ///     The new email address for the <see cref="Models.API.User"/>. Requires <see cref="CurrentPassword"/> to be set.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "email")]
        public string? Email { get; set; }

        /// <summary>
        ///     The new name for the <see cref="Models.API.User"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "name")]
        public string? Name { get; set; }

        /// <summary>
        ///     The new password for the <see cref="Models.API.User"/>. Requires <see cref="CurrentPassword"/> to be set.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "password")]
        public string? Password { get; set; }

        /// <summary>
        ///     Confirmation of the new password for the <see cref="Models.API.User"/>. Required if <see cref="Password"/> is being updated.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "password_confirmation")]
        public string? PasswordConfirmation { get; set; }

        /// <summary>
        ///     The new phone number for the <see cref="Models.API.User"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "phone_number")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        ///     The new recharge amount for the <see cref="Models.API.User"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "recharge_amount")]
        public string? RechargeAmount { get; set; }

        /// <summary>
        ///     The new recharge threshold for the <see cref="Models.API.User"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "recharge_threshold")]
        public string? RechargeThreshold { get; set; }

        /// <summary>
        ///     The new secondary recharge amount for the <see cref="Models.API.User"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "secondary_recharge_amount")]
        public string? SecondaryRechargeAmount { get; set; }

        #endregion
    }
}
