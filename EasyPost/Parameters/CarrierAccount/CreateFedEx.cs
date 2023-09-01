using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-carrier-account">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateFedEx : Create
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "account_number")]
        public string? AccountNumber { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_city")]
        public string? CorporateAddressCity { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_country_code")]
        public string? CorporateAddressCountryCode { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_postal_code")]
        public string? CorporateAddressPostalCode { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_state")]
        public string? CorporateAddressState { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_streets")]
        public string? CorporateAddressStreet { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_company_name")]
        public string? CorporateCompanyName { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_email_address")]
        public string? CorporateEmailAddress { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_first_name")]
        public string? CorporateFirstName { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_job_title")]
        public string? CorporateJobTitle { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_last_name")]
        public string? CorporateLastName { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "corporate_phone_number")]
        public string? CorporatePhoneNumber { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_city")]
        public string? ShippingAddressCity { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_country_code")]
        public string? ShippingAddressCountryCode { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_postal_code")]
        public string? ShippingAddressPostalCode { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_state")]
        public string? ShippingAddressState { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "shipping_streets")]
        public string? ShippingAddressStreet { get; set; }

        #endregion

        /// <summary>
        ///     Construct a new set of <see cref="CreateFedEx"/> parameters.
        /// </summary>
        public CreateFedEx()
        {
            Type = Constants.CarrierAccountTypes.FedExAccount;
        }
    }
}