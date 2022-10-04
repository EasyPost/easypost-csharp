using System.Net;
using RestSharp;

namespace EasyPost.Utilities
{
    internal static class Http
    {
        /// <summary>
        ///     Return whether the given response has a status code in the given range.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given response has a status code in the given range.</returns>
        internal static bool HasStatusCodeBetween(this RestResponseBase response, int min, int max)
        {
            return StatusCodeBetween(response, min, max);
        }

        /// <summary>
        ///     Return whether the given status code is a 3xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
        internal static bool Is3xx(this HttpStatusCode statusCode)
        {
            return StatusCodeIs3xx(statusCode);
        }

        /// <summary>
        ///     Return whether the given status code is a 4xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
        internal static bool Is4xx(this HttpStatusCode statusCode)
        {
            return StatusCodeIs4xx(statusCode);
        }

        /// <summary>
        ///     Return whether the given status code is a 5xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
        internal static bool Is5xx(this HttpStatusCode statusCode)
        {
            return StatusCodeIs5xx(statusCode);
        }

        /// <summary>
        ///     Return whether the given status code is in the given range.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given status code is in the given range.</returns>
        internal static bool IsBetween(this HttpStatusCode statusCode, int min, int max)
        {
            return StatusCodeBetween(statusCode, min, max);
        }

        /// <summary>
        ///     Return whether the given response has an error status code.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <returns>True if the response code is not in the 100-299 range, false otherwise.</returns>
        internal static bool ReturnedError(this RestResponse response)
        {
            return !ReturnedNoError(response);
        }

        /// <summary>
        ///     Return whether the given response has a successful status code.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <returns>True if the response code is in the 100-299 range, false otherwise.</returns>
        internal static bool ReturnedNoError(this RestResponse response)
        {
            return StatusCodeBetween(response, 100, 299);
        }

        /// <summary>
        ///     Return whether the given status code is in the given range.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given status code is in the given range.</returns>
        internal static bool StatusCodeBetween(int statusCode, int min, int max)
        {
            return statusCode >= min && statusCode <= max;
        }

        /// <summary>
        ///     Return whether the given status code is in the given range.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given status code is in the given range.</returns>
        internal static bool StatusCodeBetween(HttpStatusCode statusCode, int min, int max)
        {
            return StatusCodeBetween((int)statusCode, min, max);
        }

        /// <summary>
        ///     Return whether the given response has a status code in the given range.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given response has a status code in the given range.</returns>
        internal static bool StatusCodeBetween(RestResponseBase response, int min, int max)
        {
            return StatusCodeBetween(response.StatusCode, min, max);
        }

        /// <summary>
        ///     Return whether the given status code is a 3xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
        internal static bool StatusCodeIs3xx(int statusCode)
        {
            return StatusCodeBetween(statusCode, 300, 399);
        }

        /// <summary>
        ///     Return whether the given status code is a 3xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
        internal static bool StatusCodeIs3xx(HttpStatusCode statusCode)
        {
            return StatusCodeBetween(statusCode, 300, 399);
        }

        /// <summary>
        ///     Return whether the given status code is a 4xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
        internal static bool StatusCodeIs4xx(int statusCode)
        {
            return StatusCodeBetween(statusCode, 400, 499);
        }

        /// <summary>
        ///     Return whether the given status code is a 4xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
        internal static bool StatusCodeIs4xx(HttpStatusCode statusCode)
        {
            return StatusCodeBetween(statusCode, 400, 499);
        }

        /// <summary>
        ///     Return whether the given status code is a 5xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
        internal static bool StatusCodeIs5xx(int statusCode)
        {
            return StatusCodeBetween(statusCode, 500, 599);
        }

        /// <summary>
        ///     Return whether the given status code is a 5xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
        internal static bool StatusCodeIs5xx(HttpStatusCode statusCode)
        {
            return StatusCodeBetween(statusCode, 500, 599);
        }
    }
}
