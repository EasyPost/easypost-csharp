using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.FedExRegistration
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.FedExRegistrationService.RequestPin(string, string, RequestPin, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RequestPin : BaseParameters<Models.API.FedExRequestPinResponse>, IFedExRegistrationParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Action for the FedEx registration (create/update).
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "easypost_details", "action")]
        public string? Action { get; set; }

        /// <summary>
        ///     Type of carrier account for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "easypost_details", "type")]
        public string? Type { get; set; }

        /// <summary>
        ///     Carrier account ID for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "easypost_details", "carrier_account_id")]
        public string? CarrierAccountId { get; set; }

        #endregion
    }
}
