using System.Collections.Generic;
using EasyPost.Models.API;

// ReSharper disable once CheckNamespace
namespace EasyPost.Exceptions
{
    public class InvalidRequestError : ApiError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="InvalidRequestError" /> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code to store as a property.</param>
        /// <param name="errorMessage">Error message string to print to console.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        /// <param name="errors">Optional list of Error objects to store as a property.</param>
        internal InvalidRequestError(int statusCode, string errorMessage, string? errorType = null, List<Error>? errors = null) : base(statusCode, errorMessage, errorType, errors)
        {
        }
    }
}
