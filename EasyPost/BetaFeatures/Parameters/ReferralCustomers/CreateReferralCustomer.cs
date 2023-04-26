using System.Diagnostics.CodeAnalysis;
using EasyPost.Services;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.BetaFeatures.Parameters.ReferralCustomers
{
    /// <summary>
    ///     Parameters for <see cref="ReferralCustomerService.CreateReferral(CreateReferralCustomer)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class CreateReferralCustomer : BaseParameters, IReferralCustomerParameter
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Optional, "user", "email")]
        public string? Email { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "name")]
        public string? Name { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "user", "phone")]
        public string? PhoneNumber { get; set; }

        #endregion
    }
}
