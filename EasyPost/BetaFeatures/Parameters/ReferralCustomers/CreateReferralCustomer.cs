using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Annotations;

namespace EasyPost.BetaFeatures.Parameters.ReferralCustomers
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.PartnerService.CreateReferral(CreateReferralCustomer)"/> API calls.
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
