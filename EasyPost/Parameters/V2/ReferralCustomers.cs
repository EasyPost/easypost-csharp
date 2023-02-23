using EasyPost.Utilities.Annotations;

namespace EasyPost.Parameters.V2
{
    public static class ReferralCustomers
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.PartnerService.CreateReferral"/> API calls.
        /// </summary>
        public class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "user", "email")]
            public string? Email { get; set; }

            [RequestParameter(Necessity.Optional, "user", "name")]
            public string? Name { get; set; }

            [RequestParameter(Necessity.Optional, "user", "phone")]
            public string? PhoneNumber { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.PartnerService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }
    }
}
