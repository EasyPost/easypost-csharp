using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://docs.easypost.com/docs/carrier-accounts#create-a-carrieraccount">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Create(ACreate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateFedEx : ACreate
    {
        #region Request Parameters

        /// <summary>
        ///     The FedEx account number.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "account_number")]
        public string? AccountNumber { get; set; }

        /// <summary>
        ///     The city of the address of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_city")]
        public string? CorporateAddressCity { get; set; }

        /// <summary>
        ///     The country code of the address of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_country_code")]
        public string? CorporateAddressCountryCode { get; set; }

        /// <summary>
        ///     The postal code of the address of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_postal_code")]
        public string? CorporateAddressPostalCode { get; set; }

        /// <summary>
        ///     The state of the address of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_state")]
        public string? CorporateAddressState { get; set; }

        /// <summary>
        ///     The street of the address of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_streets")]
        public string? CorporateAddressStreet { get; set; }

        /// <summary>
        ///     The company name of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_company_name")]
        public string? CorporateCompanyName { get; set; }

        /// <summary>
        ///     The email address of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_email_address")]
        public string? CorporateEmailAddress { get; set; }

        /// <summary>
        ///     The first name of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_first_name")]
        public string? CorporateFirstName { get; set; }

        /// <summary>
        ///     The job title of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_job_title")]
        public string? CorporateJobTitle { get; set; }

        /// <summary>
        ///     The last name of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_last_name")]
        public string? CorporateLastName { get; set; }

        /// <summary>
        ///     The phone number of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_phone_number")]
        public string? CorporatePhoneNumber { get; set; }

        /// <summary>
        ///     The city of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_city")]
        public string? ShippingAddressCity { get; set; }

        /// <summary>
        ///     The country code of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_country_code")]
        public string? ShippingAddressCountryCode { get; set; }

        /// <summary>
        ///     The postal code of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_postal_code")]
        public string? ShippingAddressPostalCode { get; set; }

        /// <summary>
        ///     The state of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_state")]
        public string? ShippingAddressState { get; set; }

        /// <summary>
        ///     The street of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_streets")]
        public string? ShippingAddressStreet { get; set; }

        /// <summary>
        ///     Description for the new <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "description")]
        public string? Description { get; set; }

        /// <summary>
        ///     Reference name for the new <see cref="Models.API.CarrierAccount"/>.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "reference")]
        public string? Reference { get; set; }

        /// <inheritdoc/>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "type")]
        public override string? Type { get; set; }

        #endregion

        /// <inheritdoc cref="EasyPost.Parameters.CarrierAccount.ACreate.Endpoint"/>
        internal override string Endpoint => Constants.CarrierAccounts.CustomCreateEndpoint;

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateFedEx"/> class.
        /// </summary>
        public CreateFedEx()
            : base(CarrierAccountType.FedEx)
        {
        }
    }
}
