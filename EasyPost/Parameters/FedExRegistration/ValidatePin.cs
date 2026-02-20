using System;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.FedExRegistration
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.FedExRegistrationService.ValidatePin(string, ValidatePin, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class ValidatePin : BaseParameters<Models.API.FedExAccountValidationResponse>, IFedExRegistrationParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Name for the FedEx registration.
        ///     If not provided, a UUID will be auto-generated.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pin_validation", "name")]
        public string? Name { get; set; }

        /// <summary>
        ///     PIN code received for validation.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "pin_validation", "pin")]
        public string? Pin { get; set; }

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

        /// <summary>
        ///     Override the default <see cref="BaseParameters{TMatchInputType}.ToDictionary"/> method to ensure the "name" field exists.
        ///     If not present, generates a UUID (with hyphens removed) as the name.
        /// </summary>
        /// <returns>A <see cref="System.Collections.Generic.Dictionary{TKey,TValue}"/>.</returns>
        public override System.Collections.Generic.Dictionary<string, object> ToDictionary()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                Name = Guid.NewGuid().ToString().Replace("-", string.Empty);
            }

            return base.ToDictionary();
        }
    }
}
