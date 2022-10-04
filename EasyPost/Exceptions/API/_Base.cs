﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using EasyPost.Models.API;
using EasyPost.Utilities;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace EasyPost.Exceptions.API
{
    public class ApiError : EasyPostError
    {
        public readonly string? Code;
        public readonly List<Error>? Errors;
        public readonly int? StatusCode;

        /// <summary>
        ///     Get a formatted error string with expanded details about the error.
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
        protected ApiError(string errorMessage, int? statusCode = null, string? errorType = null, List<Error>? errors = null) : base(errorMessage)
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
        /// <param name="response">RestResponse response to parse</param>
        /// <raises>EasyPostError if a non-400/500 error code is extracted from the response.</raises>
        /// <returns>An instance of an HttpError-inherited exception.</returns>
        internal static ApiError FromErrorResponse(RestResponse response)
        {
            // NOTE: This method anticipates that the status code will be a 4xx or 5xx error code.
            // Do not use this method to parse a successful response.
            HttpStatusCode statusCode = response.StatusCode;
            int statusCodeInt = (int)statusCode;

            string? errorMessage;
            string? errorType;
            List<Error>? errors;

            try
            {
                // try to extract error details from the API response
                Dictionary<string, Dictionary<string, object>> body = JsonSerialization.ConvertJsonToObject<Dictionary<string, Dictionary<string, object>>>(response.Content);

                // Errors may be an array improperly assigned to the `message` field instead of the `errors` field, concatenate those here
                object parsedBodyMessage = body["error"]["message"];
                errorMessage = parsedBodyMessage switch
                {
                    string bodyMessage => bodyMessage,
                    JArray bodyMessage => string.Join(", ", bodyMessage),
                    var _ => throw new Exception() // this will trigger the catch block below
                };
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
                               ?? Constants.ErrorMessages.ApiDidNotReturnErrorDetails; // fallback to no error details
                errorType = Constants.ErrorMessages.ApiErrorDetailsParsingError;
                errors = null;
            }

            Type? exceptionType = statusCode.EasyPostExceptionType();
            if (exceptionType == null)
            {
                // A non-400/500 error code in the response, which is not expected.
                throw new EasyPostError(string.Format(Constants.ErrorMessages.UnexpectedHttpStatusCode, statusCodeInt));
            }

            // instantiate the exception class
            var cons = exceptionType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
            return (ApiError)cons[0].Invoke(new object?[] { errorMessage, statusCodeInt, errorType, errors });
        }
    }
}
