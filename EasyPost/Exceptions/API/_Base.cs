using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Models.API;
using EasyPost.Utilities.Internal;
using RestSharp;

namespace EasyPost.Exceptions.API
{
#pragma warning disable SA1649
    /// <summary>
    ///     Represents an error that occurred while communicating with the EasyPost API.
    ///     This is typically due to a specific HTTP status code, such as 4xx or 5xx.
    ///     This is different than the Error class, which represents information about what triggered the failed request.
    ///     The information from the top-level Error class is used to generate this error, and any sub-errors are stored as a list of Error objects.
    /// </summary>
    public class ApiError : EasyPostError
#pragma warning restore SA1649
    {
        public readonly string? Code;
        public readonly List<Error>? Errors; // Details about what server-side issues caused the API request to fail.
        public readonly int? StatusCode;

        /// <summary>
        ///     Gets a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public string PrettyPrint
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
        /// <param name="errorMessage">Error message string to print to console.</param>
        /// <param name="statusCode">Optional HTTP status code to store as a property.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        /// <param name="errors">Optional list of Error objects to store as a property.</param>
        protected ApiError(string errorMessage, int? statusCode = null, string? errorType = null, List<Error>? errors = null)
            : base(errorMessage)
        {
            StatusCode = statusCode;
            Code = errorType;
            Errors = errors;
        }

        // great minds think alike: https://github.com/stripe/stripe-dotnet/blob/6b9513d3b938d265c7607db919ad2c536ab578c3/src/Stripe.net/Infrastructure/Public/StripeClient.cs#L171

        /// <summary>
        ///     Parse a errored RestResponse response object and return an instance of the appropriate exception class.
        ///     Do not pass a non-error response to this method.
        /// </summary>
        /// <param name="response">RestResponse response to parse.</param>
        /// <raises>EasyPostError if an unplanned response code is found (i.e. user passed in a non-error RestResponse response object with a 2xx status code).</raises>
        /// <returns>An instance of an HttpError-inherited exception.</returns>
        internal static ApiError FromErrorResponse(RestResponse response)
        {
            // NOTE: This method anticipates that the status code will be a non-2xx code.
            // Do not use this method to parse a successful response.

            // short-circuit if the request timed out
            if (response.ErrorException?.GetType() == typeof(TaskCanceledException))
            {
                return new TimeoutError(Constants.ErrorMessages.ApiRequestTimedOut, 408);
            }

            HttpStatusCode statusCode = response.StatusCode;
            int statusCodeInt = (int)statusCode;

            string? errorMessage;
            string? errorType;
            List<Error>? errors;

            try
            {
                // try to convert the response to an Error object
                Error error = JsonSerialization.ConvertJsonToObject<Error>(response.Content, rootElementKeys: new List<string> { "error" });

                // if the above succeeded, we'll extract the error details from the Error object
                errorMessage = error.Message;
                errorType = error.Code;
                errors = error.Errors;
            }
#pragma warning disable CA1031 // Do not catch general exception types
            catch
            {
                // could not extract error details from the API response (or API did not return data, i.e. 1xx, 3xx or 5xx)
                errorMessage = response.ErrorMessage // fallback to standard HTTP error message
                               ?? response.StatusDescription // fallback to standard HTTP status description
                               ?? Constants.ErrorMessages.ApiDidNotReturnErrorDetails; // fallback to no error details
                errorType = Constants.ErrorMessages.ApiErrorDetailsParsingError;
                errors = null;
            }
#pragma warning restore CA1031 // Do not catch general exception types

            Type? exceptionType = statusCode.EasyPostExceptionType();
#pragma warning disable IDE0270 // Simplify null check
            if (exceptionType == null)
            {
                // A unaccounted-for status code was in the response.
                throw new EasyPostError(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.UnexpectedHttpStatusCode, statusCodeInt));
            }
#pragma warning restore IDE0270 // Simplify null check

            // instantiate the exception class
            ConstructorInfo[] cons = exceptionType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            return (ApiError)cons[0].Invoke(new object?[] { errorMessage, statusCodeInt, errorType, errors });
        }
    }
}
