using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.ReferralCustomer
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/users/referral-customers#create-a-referralcustomer">Parameters</a> for <see cref="EasyPost.Services.ReferralCustomerService.CreateReferral(CreateReferralCustomer, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateReferralCustomer : BaseParameters<Models.API.ReferralCustomer>, IReferralCustomerParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Email address of the new <see cref="Models.API.ReferralCustomer"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "email")]
        public string? Email { get; set; }

        /// <summary>
        ///     Name of the new <see cref="Models.API.ReferralCustomer"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Phone number of the new <see cref="Models.API.ReferralCustomer"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "user", "phone")]
        public string? PhoneNumber { get; set; }

        #endregion
    }
}
