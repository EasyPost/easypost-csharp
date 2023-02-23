using System.Collections.Generic;
using EasyPost.Utilities.Annotations;

namespace EasyPost.Parameters.V2
{
    public static class CarrierAccounts
    {
        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.CarrierAccountService.Create"/> creation API calls.
        /// </summary>
        public sealed class Create : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "carrier_account", "credentials")]
            public Dictionary<string, object?>? Credentials { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "description")]
            public string? Description { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "reference")]
            public string? Reference { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "test_credentials")]

            // ReSharper disable once InconsistentNaming
            public Dictionary<string, object?>? TestCredentials { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "type")]
            public string? Type { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for FedEx <see cref="EasyPost.Services.CarrierAccountService.Create"/> creation API calls.
        /// </summary>
        public sealed class CreateFedEx : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "carrier_account", "type")]
            internal string Type => "FedexAccount";

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "account_number")]
            public string? AccountNumber { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_city")]
            public string? CorporateAddressCity { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_country_code")]
            public string? CorporateAddressCountryCode { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_postal_code")]
            public string? CorporateAddressPostalCode { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_state")]
            public string? CorporateAddressState { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_streets")]
            public string? CorporateAddressStreet { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_company_name")]
            public string? CorporateCompanyName { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_email_address")]
            public string? CorporateEmailAddress { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_first_name")]
            public string? CorporateFirstName { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_job_title")]
            public string? CorporateJobTitle { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_last_name")]
            public string? CorporateLastName { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "corporate_phone_number")]
            public string? CorporatePhoneNumber { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "description")]
            public string? Description { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "reference")]
            public string? Reference { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "shipping_city")]
            public string? ShippingAddressCity { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "shipping_country_code")]
            public string? ShippingAddressCountryCode { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "shipping_postal_code")]
            public string? ShippingAddressPostalCode { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "shipping_state")]
            public string? ShippingAddressState { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "shipping_streets")]
            public string? ShippingAddressStreet { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for UPS <see cref="EasyPost.Services.CarrierAccountService.Create"/> API calls.
        /// </summary>
        public sealed class CreateUps : CreateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "carrier_account", "type")]
            internal string Type => "UpsAccount";

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "account_number")]
            public string? AccountNumber { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "city")]
            public string? City { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "company")]
            public string? CompanyName { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "country")]
            public string? Country { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "description")]
            public string? Description { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "email")]
            public string? Email { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_amount")]
            public string? InvoiceAmount { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_control_id")]
            public string? InvoiceControlId { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_currency")]
            public string? InvoiceCurrency { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_date")]
            public string? InvoiceDate { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "invoice_number")]
            public string? InvoiceNumber { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "phone")]
            public string? PhoneNumber { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "postal_code")]
            public string? PostalCode { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "reference")]
            public string? Reference { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "title")]
            public string? RegistrarJobTitle { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "name")]
            public string? RegistrarName { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "state")]
            public string? State { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "street1")]
            public string? Street { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "street2")]
            public string? Street2 { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "registration_data", "website")]
            public string? Website { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Models.API.CarrierAccount"/> update API calls.
        /// </summary>
        public sealed class Update : UpdateRequestParameters
        {
            #region Request Parameters

            [RequestParameter(Necessity.Optional, "carrier_account", "credentials")]
            public Dictionary<string, object?>? Credentials { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "description")]
            public string? Description { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "reference")]
            public string? Reference { get; set; }

            [RequestParameter(Necessity.Optional, "carrier_account", "test_credentials")]

            // ReSharper disable once InconsistentNaming
            public Dictionary<string, object?>? TestCredentials { get; set; }

            #endregion
        }

        /// <summary>
        ///     Parameters for <see cref="EasyPost.Services.CarrierAccountService.All"/> API calls.
        /// </summary>
        public sealed class All : AllRequestParameters
        {
        }
    }
}
