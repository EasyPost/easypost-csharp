using System;
using System.Collections.Generic;
using System.Net;
using EasyPost.Exceptions.API;
using EasyPost.Utilities.Internal;

namespace EasyPost
{
    public static class Constants
    {
        private static readonly Dictionary<int, Type?> HttpExceptionsMap = new()
        {
            // 1xx status codes are usually HTTP process-related, not server-related
            // (mostly) everything else is related to the server being interacted with: https://httpwg.org/specs/rfc9110.html#rfc.section.15.5

            { 0, typeof(ConnectionError) }, // RestSharp returns status code 0 when a connection cannot be established (i.e. no internet access)
            { 100, typeof(UnknownHttpError) },
            { 101, typeof(UnknownHttpError) },
            { 102, typeof(UnknownHttpError) },
            { 103, typeof(UnknownHttpError) },
            { 300, typeof(RedirectError) },
            { 301, typeof(RedirectError) },
            { 302, typeof(RedirectError) },
            { 303, typeof(RedirectError) },
            { 304, typeof(RedirectError) },
            { 305, typeof(RedirectError) },
            { 306, typeof(RedirectError) },
            { 307, typeof(RedirectError) },
            { 308, typeof(RedirectError) },
            { 401, typeof(UnauthorizedError) },
            { 402, typeof(PaymentError) },
            { 403, typeof(UnauthorizedError) },
            { 404, typeof(NotFoundError) },
            { 405, typeof(MethodNotAllowedError) },
            { 408, typeof(TimeoutError) },
            { 422, typeof(InvalidRequestError) },
            { 429, typeof(RateLimitError) },
            { 500, typeof(InternalServerError) },
            { 503, typeof(ServiceUnavailableError) },
            { 504, typeof(GatewayTimeoutError) },
        };

        public static Type? EasyPostExceptionType(this HttpStatusCode statusCode) => GetEasyPostExceptionType(statusCode);

        public static Type? GetEasyPostExceptionType(int statusCode)
        {
            if (HttpExceptionsMap.TryGetValue(statusCode, out Type? value))
            {
                // return the exception type from the map
                return value;
            }

            // provided status code is not in the map, find fallback
            Type? exceptionType = null;
            SwitchCase @switch = new()
            {
                { Utilities.Internal.Extensions.Http.StatusCodeIs1xx(statusCode), () => { exceptionType = typeof(UnknownHttpError); } },
                { Utilities.Internal.Extensions.Http.StatusCodeIs3xx(statusCode), () => { exceptionType = typeof(UnknownHttpError); } },
                { Utilities.Internal.Extensions.Http.StatusCodeIs4xx(statusCode), () => { exceptionType = typeof(UnknownHttpError); } },
                { Utilities.Internal.Extensions.Http.StatusCodeIs5xx(statusCode), () => { exceptionType = typeof(UnknownHttpError); } },
                { SwitchCaseScenario.Default, () => { exceptionType = null; } },
            };
            @switch.MatchFirst(true); // evaluate switch case, checking which expression evaluates to "true"

            return exceptionType;
        }

        public static Type? GetEasyPostExceptionType(HttpStatusCode statusCode) => GetEasyPostExceptionType((int)statusCode);

        // public so end-users can access if need to (i.e. regex?)
        public static class ErrorMessages
        {
            public const string InvalidApiKeyType = "Invalid API key type.";
            public const string InvalidParameter = "Invalid parameter: {0}.";
            public const string InvalidWebhookSignature = "Webhook does not contain a valid HMAC signature.";
            public const string JsonDeserializationError = "Error deserializing JSON into object of type {0}.";
            public const string JsonNoDataToDeserialize = "No data to deserialize.";
            public const string JsonSerializationError = "Error serializing {0} object into JSON.";
            public const string MissingProperty = "{0} object is missing attribute: {0}.";
            public const string MissingRequiredParameter = "Missing required parameter: {0}.";
            public const string NoObjectFound = "No {0} found.";
            public const string NoPaymentMethods = "No payment methods are set up. Please add a payment method and try again.";
            public const string NullObjectComparison = "Cannot compare null objects.";
            public const string PaymentNotSetUp = "This payment method is not set up.";
            public const string UnexpectedHttpStatusCode = "Unexpected HTTP status code: {0}.";
            public const string ApiDidNotReturnErrorDetails = "API did not return error details.";
            public const string CouldNotPassClient = "Could not pass client to {0}.";
            internal const string ApiErrorDetailsParsingError = "RESPONSE.PARSE_ERROR"; // not for public consumption
            public const string NoMorePagesToRetrieve = "There are no more pages to retrieve.";
            public const string ApiRequestTimedOut = "The request to EasyPost timed out.";
        }

        public static class CarrierAccountTypes
        {
            internal static List<string> CarrierTypesWithCustomWorkflows => new()
            {
                "FedexAccount",
                "UpsAccount",
            };
        }
    }
}
