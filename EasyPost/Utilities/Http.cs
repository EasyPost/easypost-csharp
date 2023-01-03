using System.Net;
using RestSharp;

// ReSharper disable InconsistentNaming

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
        internal static bool HasStatusCodeBetween(this RestResponseBase response, int min, int max) => StatusCodeBetween(response, min, max);

        /// <summary>
        ///     Return whether the given status code is a 1xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 1xx error, False otherwise.</returns>
        internal static bool Is1xx(this HttpStatusCode statusCode) => StatusCodeIs1xx(statusCode);

        /// <summary>
        ///     Return whether the given status code is a 2xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 2xx error, False otherwise.</returns>
        internal static bool Is2xx(this HttpStatusCode statusCode) => StatusCodeIs2xx(statusCode);

        /// <summary>
        ///     Return whether the given status code is a 3xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
        internal static bool Is3xx(this HttpStatusCode statusCode) => StatusCodeIs3xx(statusCode);

        /// <summary>
        ///     Return whether the given status code is a 4xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
        internal static bool Is4xx(this HttpStatusCode statusCode) => StatusCodeIs4xx(statusCode);

        /// <summary>
        ///     Return whether the given status code is a 5xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
        internal static bool Is5xx(this HttpStatusCode statusCode) => StatusCodeIs5xx(statusCode);

        /// <summary>
        ///     Return whether the given status code is in the given range.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given status code is in the given range.</returns>
        internal static bool IsBetween(this HttpStatusCode statusCode, int min, int max) => StatusCodeBetween(statusCode, min, max);

        /// <summary>
        ///     Return whether the given response has an error status code.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <returns>True if the response code is not in the 200-299 range, false otherwise.</returns>
        internal static bool ReturnedError(this RestResponse response) => !ReturnedNoError(response);

        /// <summary>
        ///     Return whether the given response has a successful status code.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <returns>True if the response code is in the 200-299 range, false otherwise.</returns>
        internal static bool ReturnedNoError(this RestResponse response) => response.StatusCode.Is2xx();

        /// <summary>
        ///     Return whether the given status code is in the given range.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given status code is in the given range.</returns>
        internal static bool StatusCodeBetween(int statusCode, int min, int max) => statusCode >= min && statusCode <= max;

        /// <summary>
        ///     Return whether the given status code is in the given range.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given status code is in the given range.</returns>
        internal static bool StatusCodeBetween(HttpStatusCode statusCode, int min, int max) => StatusCodeBetween((int)statusCode, min, max);

        /// <summary>
        ///     Return whether the given response has a status code in the given range.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given response has a status code in the given range.</returns>
        internal static bool StatusCodeBetween(RestResponseBase response, int min, int max) => StatusCodeBetween(response.StatusCode, min, max);

        /// <summary>
        ///     Return whether the given status code is a 1xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 1xx error, False otherwise.</returns>
        internal static bool StatusCodeIs1xx(int statusCode) => StatusCodeBetween(statusCode, 100, 199);

        /// <summary>
        ///     Return whether the given status code is a 1xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 1xx error, False otherwise.</returns>
        internal static bool StatusCodeIs1xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 100, 199);

        /// <summary>
        ///     Return whether the given status code is a 2xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 2xx error, False otherwise.</returns>
        internal static bool StatusCodeIs2xx(int statusCode) => StatusCodeBetween(statusCode, 200, 299);

        /// <summary>
        ///     Return whether the given status code is a 2xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 2xx error, False otherwise.</returns>
        internal static bool StatusCodeIs2xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 200, 299);

        /// <summary>
        ///     Return whether the given status code is a 3xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
        internal static bool StatusCodeIs3xx(int statusCode) => StatusCodeBetween(statusCode, 300, 399);

        /// <summary>
        ///     Return whether the given status code is a 3xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 3xx error, False otherwise.</returns>
        internal static bool StatusCodeIs3xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 300, 399);

        /// <summary>
        ///     Return whether the given status code is a 4xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
        internal static bool StatusCodeIs4xx(int statusCode) => StatusCodeBetween(statusCode, 400, 499);

        /// <summary>
        ///     Return whether the given status code is a 4xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 4xx error, False otherwise.</returns>
        internal static bool StatusCodeIs4xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 400, 499);

        /// <summary>
        ///     Return whether the given status code is a 5xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
        internal static bool StatusCodeIs5xx(int statusCode) => StatusCodeBetween(statusCode, 500, 599);

        /// <summary>
        ///     Return whether the given status code is a 5xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns>True if the status code is a 5xx error, False otherwise.</returns>
        internal static bool StatusCodeIs5xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 500, 599);
    }
}
