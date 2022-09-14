using System;
using System.Collections.Generic;
using System.Net;
using EasyPost.Exceptions.API;
using EasyPost.Utilities;

namespace EasyPost.Exceptions
{
    public static class Constants
    {
        private static readonly Dictionary<int, Type> HttpExceptionsMap = new Dictionary<int, Type>
        {
            { 0, typeof(VcrError) }, // EasyVCR uses 0 as the status code when a recording cannot be found
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
            { 504, typeof(GatewayTimeoutError) }
        };

        public static Type? EasyPostExceptionType(this HttpStatusCode statusCode)
        {
            return GetEasyPostExceptionType(statusCode);
        }

        public static Type? GetEasyPostExceptionType(int statusCode)
        {
            if (HttpExceptionsMap.ContainsKey(statusCode))
            {
                // return the exception type from the map
                return HttpExceptionsMap[statusCode];
            }

            // provided status code is not in the map, find fallback
            Type? exceptionType = null;
            var @switch = new SwitchCase
            {
                { Utilities.Http.StatusCodeIs4xx(statusCode), () => { exceptionType = typeof(UnknownApiError); } },
                { Utilities.Http.StatusCodeIs5xx(statusCode), () => { exceptionType = typeof(UnexpectedHttpError); } },
                { SwitchCaseScenario.Default, () => { exceptionType = null; } }
            };
            @switch.MatchFirst(true); // evaluate switch case, checking which expression evaluates to "true"

            return exceptionType;
        }

        public static Type? GetEasyPostExceptionType(HttpStatusCode statusCode)
        {
            return GetEasyPostExceptionType((int)statusCode);
        }
    }
}
