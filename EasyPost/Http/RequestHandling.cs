using System.Collections.Generic;
using RestSharp;

namespace EasyPost.Http
{
    internal static class RequestHandling
    {
        private static readonly List<int> RetryCodes = new List<int>
        {
            429, // Too Many Requests
            500, // Internal Server Error
            502, // Bad Gateway
            503, // Service Unavailable
            504 // Gateway Timeout
        };

        internal static bool ShouldRetry(this RestResponse response)
        {
            int code = (int)response.StatusCode;
            return RetryCodes.Contains(code);
        }
    }
}
