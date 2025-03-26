using System.Collections.Generic;

namespace EasyPost.Exceptions.API
{
    /// <summary>
    ///     Represents a generic unknown or unexpected HTTP error that occurs when communicating with the EasyPost API.
    /// </summary>
    public class UnknownHttpError : ApiError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="UnknownHttpError" /> class.
        /// </summary>
        /// <param name="errorMessage">Error message string to print to console.</param>
        /// <param name="statusCode">Optional HTTP status code to store as a property.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        /// <param name="errors">Optional list of errors to store as a property.</param>
        internal UnknownHttpError(string errorMessage, int statusCode, string? errorType = null, List<object>? errors = null)
            : base(errorMessage, statusCode, errorType, errors)
        {
        }
    }
}
