using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyPost._base;
using EasyPost.Http;
using EasyPost.Models.API;

namespace EasyPost.Services
{
    /// <summary>
    ///     Class representing a set of FedEx registration-related functionality.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class FedExRegistrationService : EasyPostService
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="FedExRegistrationService"/> class.
        /// </summary>
        /// <param name="client">The <see cref="EasyPostClient"/> to tie to this service and use for API calls.</param>
        internal FedExRegistrationService(EasyPostClient client)
            : base(client)
        {
        }

        /// <summary>
        ///     Register the billing address for a FedEx account.
        ///     Advanced method for custom parameter structures.
        /// </summary>
        /// <param name="fedexAccountNumber">The FedEx account number.</param>
        /// <param name="parameters">Dictionary of parameters.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="FedExAccountValidationResponse"/> object with next steps (PIN or invoice validation).</returns>
        public async Task<FedExAccountValidationResponse> RegisterAddress(string fedexAccountNumber, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> wrappedParams = WrapAddressValidation(parameters);
            string endpoint = $"fedex_registrations/{fedexAccountNumber}/address";

            return await RequestAsync<FedExAccountValidationResponse>(Method.Post, endpoint, cancellationToken, wrappedParams);
        }

        /// <summary>
        ///     Request a PIN for FedEx account verification.
        /// </summary>
        /// <param name="fedexAccountNumber">The FedEx account number.</param>
        /// <param name="pinMethodOption">The PIN delivery method: "SMS", "CALL", or "EMAIL".</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="FedExRequestPinResponse"/> object confirming PIN was sent.</returns>
        public async Task<FedExRequestPinResponse> RequestPin(string fedexAccountNumber, string pinMethodOption, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> wrappedParams = new Dictionary<string, object>
            {
                {
                    "pin_method", new Dictionary<string, object>
                    {
                        { "option", pinMethodOption },
                    }
                },
            };
            string endpoint = $"fedex_registrations/{fedexAccountNumber}/pin";

            return await RequestAsync<FedExRequestPinResponse>(Method.Post, endpoint, cancellationToken, wrappedParams);
        }

        /// <summary>
        ///     Validate the PIN entered by the user for FedEx account verification.
        /// </summary>
        /// <param name="fedexAccountNumber">The FedEx account number.</param>
        /// <param name="parameters">Dictionary of parameters.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="FedExAccountValidationResponse"/> object.</returns>
        public async Task<FedExAccountValidationResponse> ValidatePin(string fedexAccountNumber, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> wrappedParams = WrapPinValidation(parameters);
            string endpoint = $"fedex_registrations/{fedexAccountNumber}/pin/validate";

            return await RequestAsync<FedExAccountValidationResponse>(Method.Post, endpoint, cancellationToken, wrappedParams);
        }

        /// <summary>
        ///     Submit invoice information to complete FedEx account registration.
        /// </summary>
        /// <param name="fedexAccountNumber">The FedEx account number.</param>
        /// <param name="parameters">Dictionary of parameters.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="FedExAccountValidationResponse"/> object.</returns>
        public async Task<FedExAccountValidationResponse> SubmitInvoice(string fedexAccountNumber, Dictionary<string, object> parameters, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> wrappedParams = WrapInvoiceValidation(parameters);
            string endpoint = $"fedex_registrations/{fedexAccountNumber}/invoice";

            return await RequestAsync<FedExAccountValidationResponse>(Method.Post, endpoint, cancellationToken, wrappedParams);
        }

        /// <summary>
        ///     Wraps address validation parameters and ensures the "name" field exists.
        ///     If not present, generates a UUID (with hyphens removed) as the name.
        /// </summary>
        /// <param name="parameters">The original parameters dictionary.</param>
        /// <returns>A new dictionary with properly wrapped address_validation and easypost_details.</returns>
        private static Dictionary<string, object> WrapAddressValidation(Dictionary<string, object> parameters)
        {
            Dictionary<string, object> wrappedParams = new Dictionary<string, object>();

            if (parameters.TryGetValue("address_validation", out object? addressValidationValue))
            {
                Dictionary<string, object> addressValidation = new Dictionary<string, object>((Dictionary<string, object>)addressValidationValue);
                EnsureNameField(addressValidation);
                wrappedParams["address_validation"] = addressValidation;
            }

            if (parameters.TryGetValue("easypost_details", out object? easypostDetailsValue))
            {
                wrappedParams["easypost_details"] = easypostDetailsValue;
            }

            return wrappedParams;
        }

        /// <summary>
        ///     Wraps PIN validation parameters and ensures the "name" field exists.
        ///     If not present, generates a UUID (with hyphens removed) as the name.
        /// </summary>
        /// <param name="parameters">The original parameters dictionary.</param>
        /// <returns>A new dictionary with properly wrapped pin_validation and easypost_details.</returns>
        private static Dictionary<string, object> WrapPinValidation(Dictionary<string, object> parameters)
        {
            Dictionary<string, object> wrappedParams = new Dictionary<string, object>();

            if (parameters.TryGetValue("pin_validation", out object? pinValidationValue))
            {
                Dictionary<string, object> pinValidation = new Dictionary<string, object>((Dictionary<string, object>)pinValidationValue);
                EnsureNameField(pinValidation);
                wrappedParams["pin_validation"] = pinValidation;
            }

            if (parameters.TryGetValue("easypost_details", out object? easypostDetailsValue))
            {
                wrappedParams["easypost_details"] = easypostDetailsValue;
            }

            return wrappedParams;
        }

        /// <summary>
        ///     Wraps invoice validation parameters and ensures the "name" field exists.
        ///     If not present, generates a UUID (with hyphens removed) as the name.
        /// </summary>
        /// <param name="parameters">The original parameters dictionary.</param>
        /// <returns>A new dictionary with properly wrapped invoice_validation and easypost_details.</returns>
        private static Dictionary<string, object> WrapInvoiceValidation(Dictionary<string, object> parameters)
        {
            Dictionary<string, object> wrappedParams = new Dictionary<string, object>();

            if (parameters.TryGetValue("invoice_validation", out object? invoiceValidationValue))
            {
                Dictionary<string, object> invoiceValidation = new Dictionary<string, object>((Dictionary<string, object>)invoiceValidationValue);
                EnsureNameField(invoiceValidation);
                wrappedParams["invoice_validation"] = invoiceValidation;
            }

            if (parameters.TryGetValue("easypost_details", out object? easypostDetailsValue))
            {
                wrappedParams["easypost_details"] = easypostDetailsValue;
            }

            return wrappedParams;
        }

        /// <summary>
        ///     Ensures the "name" field exists in the provided dictionary.
        ///     If not present, generates a UUID (with hyphens removed) as the name.
        /// </summary>
        /// <param name="dictionary">The dictionary to ensure the "name" field in.</param>
        private static void EnsureNameField(Dictionary<string, object> dictionary)
        {
            if (!dictionary.TryGetValue("name", out object? nameValue) || nameValue == null)
            {
                string uuid = Guid.NewGuid().ToString().Replace("-", string.Empty);
                dictionary["name"] = uuid;
            }
        }
    }
}
