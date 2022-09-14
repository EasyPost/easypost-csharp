using System.Collections.Generic;
using System.Linq;
using EasyPost.Models.API;
using EasyPost.Utilities;
using RestSharp;

// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class ApiError : HttpError
    {
        public readonly string? Code;

        public readonly List<Error>? Errors;

        /// <summary>
        ///     Get a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public string PrettyPrintString
        {
            get
            {
                string errorString = $@"{Code} ({StatusCode}): {Message}";
                return Errors == null
                    ? errorString
                    : Errors.Aggregate(errorString, (current, error) => current + $@"
                            Field: {error.Field}
                            Message: {error.Message}
                    ");
            }
        }

        // All constructors for API exceptions are protected, so you cannot directly initialize an instance of the exception class.
        // Instead, you must use the .FromResponse method to retrieve an instance.

        /// <summary>
        ///     Initializes a new instance of the <see cref="ApiError" /> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code to store as a property.</param>
        /// <param name="errorMessage">Error message string to print to console.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        /// <param name="errors">Optional list of Error objects to store as a property.</param>
        protected ApiError(int statusCode, string errorMessage, string? errorType = null, List<Error>? errors = null) : base(statusCode, errorMessage)
        {
            Code = errorType;
            Errors = errors;
        }

        // great minds think alike: https://github.com/stripe/stripe-dotnet/blob/6b9513d3b938d265c7607db919ad2c536ab578c3/src/Stripe.net/Infrastructure/Public/StripeClient.cs#L171

        /// <summary>
        ///     Parse a RestResponse response object and return an instance of the appropriate exception class.
        /// </summary>
        /// <param name="response">RestResponse response to parse</param>
        /// <returns>An instance of an HttpError-inherited exception.</returns>
        internal static HttpError FromResponse(RestResponse response)
        {
            int statusCode = (int)response.StatusCode;

            string? errorMessage;
            string? errorType;
            List<Error>? errors;

            try
            {
                // try to extract error details from the API response
                Dictionary<string, Dictionary<string, object>> body = JsonSerialization.ConvertJsonToObject<Dictionary<string, Dictionary<string, object>>>(response.Content);

                errorMessage = body["error"]["message"].ToString();
                errorType = body["error"]["code"].ToString();
                errors = JsonSerialization.ConvertJsonToObject<List<Error>>(response.Content, null, new List<string>
                {
                    "error",
                    "errors"
                });
            }
            catch
            {
                // could not extract error details from the API response (or API did not return data, i.e. 500) -> HttpError type rather than ApiError
                errorMessage = response.ErrorMessage // fallback to standard HTTP error message
                               ?? response.StatusDescription // fallback to standard HTTP status description
                               ?? "HTTP call did not return a standard error message"; // fallback to unknown error
                errorType = "RESPONSE.PARSE_ERROR";
                errors = null;
            }

            switch (statusCode)
            {
                case 401:
                    // ApiError with extra details
                    return new UnauthorizedError(statusCode, errorMessage, errorType, errors);
                case 402:
                    // ApiError with extra details
                    return new PaymentError(statusCode, errorMessage, errorType, errors);
                case 403:
                    // ApiError with extra details
                    return new UnauthorizedError(statusCode, errorMessage, errorType, errors);
                case 404:
                    // ApiError with extra details
                    return new NotFoundError(statusCode, errorMessage, errorType, errors);
                case 405:
                    // HttpError, no extra details
                    return new MethodNotAllowedError(statusCode, errorMessage);
                case 408:
                    // HttpError, no extra details
                    return new TimeoutError(statusCode, errorMessage);
                case 422:
                    // ApiError with extra details
                    return new InvalidRequestError(statusCode, errorMessage, errorType, errors);
                case 429:
                    // HttpError, no extra details
                    return new RateLimitError(statusCode, errorMessage);
                // todo: split these out?
                case 500:
                case 501:
                case 502:
                case 503:
                    // ApiError with extra details
                    return new InternalServerError(statusCode, errorMessage, errorType, errors);
                default:
                    // todo: or should we return an HttpError here?
                    // ApiError with extra details
                    return new UnknownApiError(statusCode, errorMessage, errorType, errors);
            }
        }
    }
}
