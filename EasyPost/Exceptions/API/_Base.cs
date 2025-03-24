using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using EasyPost.Models.API;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasyPost.Exceptions.API
{
#pragma warning disable SA1649
    /// <summary>
    ///     Represents an error that occurred while communicating with the EasyPost API.
    ///     This is typically due to a specific HTTP status code, such as 4xx or 5xx.
    /// </summary>
    public abstract class ApiError : EasyPostError
#pragma warning restore SA1649
    {
        /// <summary>
        ///     The machine-readable error code returned by the API.
        /// </summary>
        public readonly string? Code;

        /// <summary>
        ///     A list of errors that contain information about what triggered the failed request.
        /// </summary>
        public readonly List<object>? Errors;

        /// <summary>
        ///     The HTTP status code returned by the API.
        /// </summary>
        public readonly int? StatusCode;

        /// <summary>
        ///     Gets a formatted error string with expanded details about the error.
        /// </summary>
        /// <returns>A formatted error string.</returns>
        public override string PrettyPrint
        {
            get
            {
                string errorString = $@"{Code} ({StatusCode}): {Message}";
                if (Errors == null)
                {
                    return errorString;
                }

                foreach (var error in Errors)
                {
                    if (error is FieldError fieldError)
                    {
                        errorString += $@"
    Field: {fieldError.Field}
    Message: {fieldError.Message}";
                        if (!string.IsNullOrEmpty(fieldError.Suggestion))
                        {
                            errorString += $@"
    Suggestion: {fieldError.Suggestion}";
                        }
                    }
                    else if (error is string errorMessage)
                    {
                        errorString += $@"
    Error: {errorMessage}";
                    }
                }

                return errorString;
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
        /// <param name="errors">Optional list of errors to store as a property.</param>
        protected ApiError(string errorMessage, int? statusCode = null, string? errorType = null, List<object>? errors = null)
            : base(errorMessage)
        {
            StatusCode = statusCode;
            Code = errorType;
            Errors = errors;
        }

        /// <summary>
        ///     Traverse the returned element for error messages.
        ///     This will handle potential inconsistent data structures in EasyPost error messages.
        /// </summary>
        /// <param name="element">The current element to traverse.</param>
        /// <param name="collectedMessages">Previously-collected error messages.</param>
        private static void CollectErrorMessages(JToken element, ICollection<string> collectedMessages)
        {
            switch (element.Type)
            {
                case JTokenType.Array:
                    foreach (JToken item in element)
                    {
                        CollectErrorMessages(item, collectedMessages);
                    }

                    break;

                case JTokenType.Object:
                    foreach (JProperty property in ((JObject)element).Properties())
                    {
                        CollectErrorMessages(property.Value, collectedMessages);
                    }

                    break;

                case JTokenType.String:
                    string? asString = element.ToString();
                    if (!string.IsNullOrWhiteSpace(asString))
                    {
                        collectedMessages.Add(asString);
                    }

                    break;

                default:
                    break;
            }
        }

        // great minds think alike: https://github.com/stripe/stripe-dotnet/blob/6b9513d3b938d265c7607db919ad2c536ab578c3/src/Stripe.net/Infrastructure/Public/StripeClient.cs#L171

        /// <summary>
        ///     Parse an errored <see cref="HttpResponseMessage"/> response object and return an instance of the appropriate exception class.
        ///     Do not pass a non-error response to this method.
        /// </summary>
        /// <param name="response"><see cref="HttpResponseMessage"/> response to parse.</param>
        /// <raises>EasyPostError if an unplanned response code is found (i.e. user passed in a non-error <see cref="HttpResponseMessage"/> response object with a 2xx status code).</raises>
        /// <returns>An instance of an <see cref="ApiError"/>-inherited exception.</returns>
        internal static async Task<ApiError> FromErrorResponse(HttpResponseMessage response)
        {
            // NOTE: This method anticipates that the status code will be a non-2xx code.
            // Do not use this method to parse a successful response.

            HttpStatusCode statusCode = response.StatusCode;
            int statusCodeInt = (int)statusCode;

            string? errorMessage;
            string? errorType;
            List<object>? errors;

            try
            {
                // Parse the response JSON using Newtonsoft.Json
                string responseContent = await response.Content.ReadAsStringAsync();
                JObject root = JObject.Parse(responseContent);

                // Navigate to the "error" object if it exists
                if (root.TryGetValue("error", out JToken? errorToken) && errorToken is JObject errorObject)
                {
                    // Extract the "code" property
                    errorType = errorObject.Value<string>("code");

                    // Extract the "message" property
                    JToken? messageToken = errorObject["message"];
                    if (messageToken != null)
                    {
                        if (messageToken is JValue { Type: JTokenType.String } stringValue)
                        {
                            errorMessage = stringValue.ToString(CultureInfo.InvariantCulture);
                        }
                        else if (messageToken is JObject or JArray)
                        {
                            // Use CollectErrorMessages to traverse and collect error messages
                            List<string> messagesList = new();
                            CollectErrorMessages(messageToken, messagesList);
                            errorMessage = string.Join(", ", messagesList);
                        }
                        else
                        {
                            throw new JsonException("Invalid message format");
                        }
                    }
                    else
                    {
                        errorMessage = null;
                    }

                    // Extract the "errors" property, which can be a list of objects or strings
                    errors = errorObject["errors"] is JArray errorsArray
                        ? errorsArray
                            .Select(errorItem => errorItem.Type == JTokenType.Object
                                ? new FieldError
                                {
                                    Field = errorItem.Value<string>("field"),
                                    Message = errorItem.Value<string>("message"),
                                    Suggestion = errorItem.Value<string>("suggestion"),
                                }
                                : errorItem.ToString() as object)
                            .ToList()
                        : null;
                }
                else
                {
                    // Fallback if "error" object is not present
                    errorMessage = response.ReasonPhrase ?? Constants.ErrorMessages.ApiDidNotReturnErrorDetails;
                    errorType = "NO RESPONSE CODE";
                    errors = null;
                }
            }
            catch
            {
                // Could not extract error details from the API response
                errorMessage = response.ReasonPhrase ?? Constants.ErrorMessages.ApiDidNotReturnErrorDetails;
                errorType = Constants.ErrorMessages.ApiErrorDetailsParsingError;
                errors = null;
            }

            Type? exceptionType = statusCode.EasyPostExceptionType();
            if (exceptionType == null)
            {
                // An unaccounted-for status code was in the response.
                throw new UnknownHttpError(string.Format(CultureInfo.InvariantCulture, Constants.ErrorMessages.UnexpectedHttpStatusCode, statusCodeInt), statusCodeInt);
            }

            // Instantiate the exception class
            ConstructorInfo[] cons = exceptionType.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance);
#pragma warning disable IDE0300 // False positive
            return (ApiError)cons[0].Invoke(new object?[] { errorMessage, statusCodeInt, errorType, errors });
#pragma warning restore IDE0300 // False positive
        }
    }
}
