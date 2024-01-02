using System.Diagnostics.CodeAnalysis;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal.Attributes;

namespace EasyPost.Parameters.CarrierAccount
{
    /// <summary>
    ///     <a href="https://www.easypost.com/docs/api#create-a-carrier-account">Parameters</a> for <see cref="EasyPost.Services.CarrierAccountService.Create(ACreate, System.Threading.CancellationToken)"/> API calls.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class CreateUps : ACreate
    {
        #region Request Parameters

        /// <summary>
        ///     The UPS account number.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "account_number")]
        public string? AccountNumber { get; set; }

        /// <summary>
        ///     The city of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "city")]
        public string? City { get; set; }

        /// <summary>
        ///     The company name of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "company")]
        public string? CompanyName { get; set; }

        /// <summary>
        ///     The country code of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "country")]
        public string? Country { get; set; }

        /// <summary>
        ///     The email address of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "email")]
        public string? Email { get; set; }

        /// <summary>
        ///     The amount of the invoice.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_amount")]
        public string? InvoiceAmount { get; set; }

        /// <summary>
        ///     The control ID of the invoice.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_control_id")]
        public string? InvoiceControlId { get; set; }

        /// <summary>
        ///     The currency of the invoice.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_currency")]
        public string? InvoiceCurrency { get; set; }

        /// <summary>
        ///     The date of the invoice.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_date")]
        public string? InvoiceDate { get; set; }

        /// <summary>
        ///     The invoice number.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_number")]
        public string? InvoiceNumber { get; set; }

        /// <summary>
        ///     The phone number of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "phone")]
        public string? PhoneNumber { get; set; }

        /// <summary>
        ///     The postal code of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "postal_code")]
        public string? PostalCode { get; set; }

        /// <summary>
        ///     The job title of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "title")]
        public string? RegistrarJobTitle { get; set; }

        /// <summary>
        ///     The full name of the corporate contact.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "name")]
        public string? RegistrarName { get; set; }

        /// <summary>
        ///     The state of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "state")]
        public string? State { get; set; }

        /// <summary>
        ///     The street of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "street1")]
        public string? Street { get; set; }

        /// <summary>
        ///     The second street line of the address of the shipping origin.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Optional, "carrier_account", "registration_data", "street2")]
        public string? Street2 { get; set; }

        /// <summary>
        ///     The website of the company.
        /// </summary>
        [TopLevelRequestParameter(Necessity.Required, "carrier_account", "registration_data", "website")]
        public string? Website { get; set; }

        /// <inheritdoc cref="EasyPost.Parameters.CarrierAccount.ACreate.Endpoint"/>
        internal override string Endpoint => Constants.CarrierAccounts.CustomCreateEndpoint;

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="CreateUps"/> class.
        /// </summary>
        public CreateUps()
            : base(CarrierAccountType.Ups)
        {
        }
    }
}
