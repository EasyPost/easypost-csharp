using System;
using System.Collections.Generic;
using EasyPost.Models.API;

namespace EasyPost.Exceptions.API
{
    public class RetryError : ApiError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RetryError" /> class.
        /// </summary>
        /// <param name="errorMessage">Error message string to print to console.</param>
        /// <param name="statusCode">Optional HTTP status code to store as a property.</param>
        /// <param name="errorType">Optional error type string to store as a property.</param>
        /// <param name="errors">Optional list of Error objects to store as a property.</param>
        internal RetryError(string errorMessage, int statusCode, string? errorType = null, List<Error>? errors = null) : base(errorMessage, statusCode, errorType, errors)
        {
            throw new NotImplementedException();
        }
    }
}
