using System;
using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.FedExRegistration
{
    /// <summary>
    ///     Parameters for <see cref="EasyPost.Services.FedExRegistrationService.RegisterAddress(string, RegisterAddress, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class RegisterAddress : BaseParameters<Models.API.FedExAccountValidationResponse>, IFedExRegistrationParameter
    {
        #region Request Parameters

        /// <summary>
        ///     Name for the FedEx registration.
        ///     If not provided, a UUID will be auto-generated.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "name")]
        public string? Name { get; set; }

        /// <summary>
        ///     Company name for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "company")]
        public string? Company { get; set; }

        /// <summary>
        ///     First street line for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "street1")]
        public string? Street1 { get; set; }

        /// <summary>
        ///     Second street line for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "street2")]
        public string? Street2 { get; set; }

        /// <summary>
        ///     City for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "city")]
        public string? City { get; set; }

        /// <summary>
        ///     State for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "state")]
        public string? State { get; set; }

        /// <summary>
        ///     ZIP code for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "zip")]
        public string? Zip { get; set; }

        /// <summary>
        ///     Country code for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "country")]
        public string? Country { get; set; }

        /// <summary>
        ///     Phone number for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "phone")]
        public string? Phone { get; set; }

        /// <summary>
        ///     Email address for the FedEx registration.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "address_validation", "email")]
        public string? Email { get; set; }

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
