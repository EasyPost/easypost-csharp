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
        /// </summary>
        /// <param name="fedexAccountNumber">The FedEx account number.</param>
        /// <param name="parameters"><see cref="Parameters.FedExRegistration.RegisterAddress"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="FedExAccountValidationResponse"/> object with next steps (PIN or invoice validation).</returns>
        public async Task<FedExAccountValidationResponse> RegisterAddress(string fedexAccountNumber, Parameters.FedExRegistration.RegisterAddress parameters, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> wrappedParams = parameters.ToDictionary();
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
        /// <param name="parameters"><see cref="Parameters.FedExRegistration.ValidatePin"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="FedExAccountValidationResponse"/> object.</returns>
        public async Task<FedExAccountValidationResponse> ValidatePin(string fedexAccountNumber, Parameters.FedExRegistration.ValidatePin parameters, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> wrappedParams = parameters.ToDictionary();
            string endpoint = $"fedex_registrations/{fedexAccountNumber}/pin/validate";

            return await RequestAsync<FedExAccountValidationResponse>(Method.Post, endpoint, cancellationToken, wrappedParams);
        }

        /// <summary>
        ///     Submit invoice information to complete FedEx account registration.
        /// </summary>
        /// <param name="fedexAccountNumber">The FedEx account number.</param>
        /// <param name="parameters"><see cref="Parameters.FedExRegistration.SubmitInvoice"/> parameter set.</param>
        /// <param name="cancellationToken"><see cref="CancellationToken"/> to use for the HTTP request.</param>
        /// <returns><see cref="FedExAccountValidationResponse"/> object.</returns>
        public async Task<FedExAccountValidationResponse> SubmitInvoice(string fedexAccountNumber, Parameters.FedExRegistration.SubmitInvoice parameters, CancellationToken cancellationToken = default)
        {
            Dictionary<string, object> wrappedParams = parameters.ToDictionary();
            string endpoint = $"fedex_registrations/{fedexAccountNumber}/invoice";

            return await RequestAsync<FedExAccountValidationResponse>(Method.Post, endpoint, cancellationToken, wrappedParams);
        }
    }
}
