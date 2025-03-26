using System.Collections.Generic;

namespace EasyPost.Exceptions.API
{
    /// <summary>
    ///     Represents an error that occurs when a rate limit error occurs on the EasyPost API.
    /// </summary>
    public class RateLimitError : ApiError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RateLimitError" /> class.
        /// </summary>
        /// <param name="errorMessage">The error message to print to console.</param>
        /// <param name="statusCode">Optional HTTP status code to store as a property.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        /// <param name="errors">Optional list of errors to store as a property.</param>
        internal RateLimitError(string errorMessage, int statusCode, string? errorType = null, List<object>? errors = null)
            : base(errorMessage, statusCode, errorType, errors)
        {
        }
    }
}
