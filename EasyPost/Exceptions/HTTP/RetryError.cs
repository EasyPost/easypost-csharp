// ReSharper disable once CheckNamespace
using System;

namespace EasyPost.Exceptions
{
    public class RetryError : HttpError
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="RetryError" /> class.
        /// </summary>
        /// <param name="statusCode">HTTP status code to store as a property.</param>
        /// <param name="errorMessage">Error message string to print to console.</param>
        internal RetryError(int statusCode, string errorMessage) : base(statusCode, errorMessage)
        {
            throw new NotImplementedException();
        }
    }
}
