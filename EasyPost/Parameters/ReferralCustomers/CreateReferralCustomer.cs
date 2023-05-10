using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.ReferralCustomers
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-referral-customer">Parameters</a> for <see cref="EasyPost.Services.ReferralCustomerService.CreateReferral(CreateReferralCustomer, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class CreateReferralCustomer : BaseParameters, IReferralCustomerParameter
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
