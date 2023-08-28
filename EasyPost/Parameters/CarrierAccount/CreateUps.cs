using System.Diagnostics.CodeAnalysis;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-carrier-account">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Create(Create, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateUps : Create
    {
        #region Request Parameters

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "account_number")]
        public string? AccountNumber { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "city")]
        public string? City { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "company")]
        public string? CompanyName { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "country")]
        public string? Country { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "email")]
        public string? Email { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_amount")]
        public string? InvoiceAmount { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_control_id")]
        public string? InvoiceControlId { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_currency")]
        public string? InvoiceCurrency { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_date")]
        public string? InvoiceDate { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_number")]
        public string? InvoiceNumber { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "phone")]
        public string? PhoneNumber { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "postal_code")]
        public string? PostalCode { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "title")]
        public string? RegistrarJobTitle { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "name")]
        public string? RegistrarName { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "state")]
        public string? State { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "street1")]
        public string? Street { get; set; }

        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "street2")]
        public string? Street2 { get; set; }

        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "website")]
        public string? Website { get; set; }

        #endregion

        /// <summary>
        ///     Construct a new set of <see cref="CreateUps"/> parameters.
        /// </summary>
        public CreateUps()
        {
            Type = Constants.CarrierAccountTypes.UpsAccount;
        }
    }
}
