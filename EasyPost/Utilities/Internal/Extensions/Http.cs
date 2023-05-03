using System.Net;
using System.Net.Http;

// ReSharper disable InconsistentNaming
namespace EasyPost.Utilities.Internal.Extensions
{
    /// <summary>
    ///     Extension utility methods for HTTP-related functionality.
    /// </summary>
    internal static class Http
    {
        /// <summary>
        ///     Return whether the given <see cref="HttpResponseMessage"/> has a status code in the given range.
        /// </summary>
        /// <param name="response"><see cref="HttpResponseMessage"/> to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given <see cref="HttpResponseMessage"/> has a status code in the given range.</returns>
        internal static bool HasStatusCodeBetween(this HttpResponseMessage response, int min, int max) => StatusCodeBetween(response, min, max);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 1xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 1xx error, <c>false</c> otherwise.</returns>
        internal static bool Is1xx(this HttpStatusCode statusCode) => StatusCodeIs1xx(statusCode);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 2xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 2xx error, <c>false</c> otherwise.</returns>
        internal static bool Is2xx(this HttpStatusCode statusCode) => StatusCodeIs2xx(statusCode);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 3xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 3xx error, <c>false</c> otherwise.</returns>
        internal static bool Is3xx(this HttpStatusCode statusCode) => StatusCodeIs3xx(statusCode);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 4xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 4xx error, <c>false</c> otherwise.</returns>
        internal static bool Is4xx(this HttpStatusCode statusCode) => StatusCodeIs4xx(statusCode);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 5xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 5xx error, <c>false</c> otherwise.</returns>
        internal static bool Is5xx(this HttpStatusCode statusCode) => StatusCodeIs5xx(statusCode);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is in the given range.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given <see cref="HttpStatusCode"/> is in the given range.</returns>
        internal static bool IsBetween(this HttpStatusCode statusCode, int min, int max) => StatusCodeBetween(statusCode, min, max);

        /// <summary>
        ///     Return whether the given <see cref="HttpResponseMessage"/> has an error status code.
        /// </summary>
        /// <param name="response"><see cref="HttpResponseMessage"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpResponseMessage"/> has a status code not in the 200-299 range, <c>false</c> otherwise.</returns>
        internal static bool ReturnedError(this HttpResponseMessage response) => !ReturnedNoError(response);

        /// <summary>
        ///     Return whether the given <see cref="HttpResponseMessage"/> has a successful status code.
        /// </summary>
        /// <param name="response"><see cref="HttpResponseMessage"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpResponseMessage"/> has a status code in the 200-299 range, <c>false</c> otherwise.</returns>
        internal static bool ReturnedNoError(this HttpResponseMessage response) => response.StatusCode.Is2xx();

        /// <summary>
        ///     Return whether the given status code is in the given range.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given status code is in the given range.</returns>
        internal static bool StatusCodeBetween(int statusCode, int min, int max) => statusCode >= min && statusCode <= max;

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is in the given range.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given <see cref="HttpStatusCode"/> is in the given range.</returns>
        internal static bool StatusCodeBetween(HttpStatusCode statusCode, int min, int max) => StatusCodeBetween((int)statusCode, min, max);

        /// <summary>
        ///     Return whether the given <see cref="HttpResponseMessage"/> has a status code in the given range.
        /// </summary>
        /// <param name="response"><see cref="HttpResponseMessage"/> to check.</param>
        /// <param name="min">Minimum valid status code.</param>
        /// <param name="max">Maximum valid status code.</param>
        /// <returns>Whether the given <see cref="HttpResponseMessage"/> has a status code in the given range.</returns>
        internal static bool StatusCodeBetween(HttpResponseMessage response, int min, int max) => StatusCodeBetween(response.StatusCode, min, max);

        /// <summary>
        ///     Return whether the given status code is a 1xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns><c>true</c> if the status code is a 1xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs1xx(int statusCode) => StatusCodeBetween(statusCode, 100, 199);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 1xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 1xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs1xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 100, 199);

        /// <summary>
        ///     Return whether the given status code is a 2xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns><c>true</c> if the status code is a 2xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs2xx(int statusCode) => StatusCodeBetween(statusCode, 200, 299);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 2xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 2xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs2xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 200, 299);

        /// <summary>
        ///     Return whether the given status code is a 3xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns><c>true</c> if the status code is a 3xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs3xx(int statusCode) => StatusCodeBetween(statusCode, 300, 399);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 3xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 3xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs3xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 300, 399);

        /// <summary>
        ///     Return whether the given status code is a 4xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns><c>true</c> if the status code is a 4xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs4xx(int statusCode) => StatusCodeBetween(statusCode, 400, 499);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 4xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 4xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs4xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 400, 499);

        /// <summary>
        ///     Return whether the given status code is a 5xx error.
        /// </summary>
        /// <param name="statusCode">Status code to check.</param>
        /// <returns><c>true</c> if the status code is a 5xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs5xx(int statusCode) => StatusCodeBetween(statusCode, 500, 599);

        /// <summary>
        ///     Return whether the given <see cref="HttpStatusCode"/> is a 5xx error.
        /// </summary>
        /// <param name="statusCode"><see cref="HttpStatusCode"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="HttpStatusCode"/> is a 5xx error, <c>false</c> otherwise.</returns>
        internal static bool StatusCodeIs5xx(HttpStatusCode statusCode) => StatusCodeBetween(statusCode, 500, 599);
    }
}
