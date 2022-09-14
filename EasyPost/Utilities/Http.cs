using RestSharp;

namespace EasyPost.Utilities
{
    internal static class Http
    {
        /// <summary>
        ///     Return whether the given response has an error status code.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <returns>True if the response code is not in the 200-399 range, false otherwise.</returns>
        internal static bool ReturnedError(this RestResponse response)
        {
            return !ReturnedNoError(response);
        }

        /// <summary>
        ///     Return whether the given response has a successful status code.
        /// </summary>
        /// <param name="response">Response to check.</param>
        /// <returns>True if the response code is in the 200-399 range, false otherwise.</returns>
        internal static bool ReturnedNoError(this RestResponse response)
        {
            return StatusCodeBetween(response, 100, 399);
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
            int statusCode = (int)response.StatusCode;
            return statusCode >= min && statusCode <= max;
        }
    }
}
